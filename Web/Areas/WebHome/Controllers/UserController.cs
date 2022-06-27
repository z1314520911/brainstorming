using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Net;
using System.IO;
//using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System.Configuration;
using Engine;
using Commons;
using BLL;
using Model;
using Newtonsoft.Json;
using System.Numerics;
using Senparc.Weixin.MP.Helpers;
using My.Engine;
using System.Security.Cryptography;
using System.Dynamic;
using System.Transactions;

namespace Web.Areas.WebHome
{
    public class UserController : XcxControllerBase
    {
        //private int _unitId = 0;

        ////
        //// GET: /User/

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetByOpenId(string openId)
        {
            T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("OpenId"), Dict.T_User_OpenId(openId));

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        #region 检测是否存在用户，不存在则自动注册用户

        /// <summary>
        /// 检测是否存在用户，不存在则自动注册用户
        /// 使用小程序用户表，需增加公众号表维护，小程序表维护
        /// </summary>
        /// <returns>返回用户信息</returns>
        public JsonResult AutoReg()
        {
            string openId = Request["openId"];
            string name = Request["name"];
            string avatarUrl = Request["avatarUrl"];
            string province = Request["province"];
            string city = Request["city"];
            string wechatshareid = Request["wechatshareid"];


            Result result = new Result();

            //string openId = Utils.StrToString(Session["MAuthAccessOpenIDForBind"]);
            //LogUtility.WriteLog("", this.GetType().ToString() + "-openID:", openId);

            if (string.IsNullOrEmpty(openId) || openId.ToLower() == "undefined")
            {
                HttpContext.Response.Write("非法访问或登录失败，请用微信打开服务号");
            }
            else
            {
                try
                {
                    T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("OpenId"), Dict.T_User_OpenId(openId));
                    if (user.Id == 0)
                    {
                        user = new T_User();
                        user.Name = name;
                        user.LoginId = openId;
                        user.Password = MD5Utility.SALTMD5("123456");
                        user.OpenId = openId;
                        user.NickName = name;
                        user.Avatar = avatarUrl;
                        user.Phone = "";
                        //user.LevelId = 4;

                        user.Id = ModelBLL.InsertOne(user);

                        //4.添加会员关系
                        string insertt_UserRelationship = string.Empty;
                        if (string.IsNullOrEmpty(wechatshareid))
                            insertt_UserRelationship = string.Format("INSERT INTO [t_UserRelationship]([OpenId],[S1],[S2]) VALUES ('{0}',NULL,NULL)", openId);
                        else
                            insertt_UserRelationship = string.Format("INSERT INTO [t_UserRelationship]([OpenId],[S1],[S2]) SELECT '{0}',[OpenId],S1 FROM [tb_UserRelationship] WHERE [OpenId]='{1}'", openId, wechatshareid);

                        int n = ModelBLL.ExecuteSql(insertt_UserRelationship);

                    }
                    result.Code = user.Id;
                    result.Detail = JsonConvert.SerializeObject(user);
                }
                catch (Exception ex)
                {
                    HttpContext.Response.Write(ex.Message);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        
        #region 首页
        public void Index()
        {
            T_User user = GetUser();
            if (user.Id > 0)
            {
                int userId = RequestEx("userId", 0);

                Dictionary<string, object> dictionary = new Dictionary<string, object>();

                int[] Total = { 0, 0, 0 };
                //Total[0] = ModelBLL.AllPage("S_Goods", Common.IsEqual("UserId"), Dict.S_Goods_UserId(userId));
                //Total[1] = ModelBLL.AllPage("T_JoinRecord", Common.IsEqual("UserId"), Dict.T_JoinRecord_UserId(userId));
                Total[2] = 0;
                dictionary.Add("Total", Total);

                ReJson(dictionary, 2);
            }
        }
        #endregion

        #region 自动获取手机号码
        public void GetPhone()
        {
            Result result = new Result();
            result.Code = -1;

            string text = HttpUtility.UrlDecode(Request["encryptedData"], System.Text.Encoding.UTF8);
            string IV = Request["iv"];

            //小程序appid和appsecret配置
            string appId = ConfigurationManager.AppSettings["XcxAppId"];
            string secret = ConfigurationManager.AppSettings["XcxSecret"];

            string code = Request["code"];//微信获取登录的口令

            Stream s_re = WebRequest.Create("https://api.weixin.qq.com/sns/jscode2session?appid=" + appId + "&secret=" + secret + "&js_code=" + code + "&grant_type=authorization_code").GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(s_re, Encoding.UTF8);
            string strLine = sr.ReadToEnd();
            sr.Close();
            try
            {
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(strLine);
                //WriteLog.CreateLog(IV);
                //WriteLog.CreateLog(strLine);
                //WriteLog.CreateLog(Convert.ToString(data.session_key));
                //string weixinID = jo["openid"].ToString();
                dynamic data2 = Newtonsoft.Json.JsonConvert.DeserializeObject(AesDecode(text, Convert.ToString(data.session_key), IV));
                result.Message = data2.phoneNumber;
                result.Code = 1;
            }
            catch (Exception ex)
            {
                result.Detail = ex.ToString();
                //WriteLog.CreateLog(ex);

            }
            ReJson(result);

        }
        #region Aes解密

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <param name="key">密钥,长度不够时空格补齐,超过时从左截取</param>
        /// <param name="iv">偏移量,长度不够时空格补齐,超过时从左截取</param>
        /// <param name="keyLenth">秘钥长度,16 24 32</param>
        /// <param name="aesMode">解密模式</param>
        /// <param name="aesPadding">填充方式</param>
        /// <returns></returns>
        public static string AesDecode(string str, string key, string iv, int keyLenth = 16, CipherMode aesMode = CipherMode.CBC, PaddingMode aesPadding = PaddingMode.PKCS7)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = Convert.FromBase64String(key);
            aes.IV = Convert.FromBase64String(iv);
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(str);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = decode2(ms.ToArray());
            }
            return Encoding.UTF8.GetString(xBuff);
        }
        private static byte[] decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
        #endregion
        #endregion
        

        #region 小程序二维码

        /// <summary>
        /// 根据参数和跳转页面路径生产B类型二维码
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public string CreateWxCodeBySceneAndPage2(string scene, string page, string imgName = "", string path = "", int color1 = -1, int color2 = -1, int color3 = -1)
        {
            string ret = string.Empty;
            try
            {
                //WxInfo obj = WxConfig.WxRequest();
                //LogUtility.WriteLog("", "PostMoths-WxInfo", JsonUtility.Serialize(obj));
                string AppId = Config.GetKeyValue("XcxAppId");
                string Secret = Config.GetKeyValue("XcxSecret");

                string access_token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(AppId, Secret, true);
                //WriteLog.CreateLog("access_token：" + access_token);
                //string access_token = Senparc.Weixin.MP.CommonAPIs.AccessTokenContainer.TryGetAccessToken(AppId, Secret);
                string DataJson = string.Empty;
                string url = "https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + access_token;
                DataJson = "{";
                //DataJson += string.Format("\"scene\":\"{0},{1}\",", "123", "456");//所要传的参数用,分看
                DataJson += string.Format("\"scene\":\"{0}\",", scene);//所要传的参数用
                DataJson += string.Format("\"page\":\"{0}\",", page);//扫码所要跳转的地址，根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面
                DataJson += string.Format("\"width\":{0},", 430);  //二维码的宽度 最高430

                DataJson += "\"line_color\":{";
                if (color1 == -1)
                {
                    DataJson += string.Format("\"r\":\"{0}\",", "0");
                    DataJson += string.Format("\"g\":\"{0}\",", "0");
                    DataJson += string.Format("\"b\":\"{0}\"", "0");
                }
                else
                {
                    DataJson += string.Format("\"r\":\"{0}\",", color1);
                    DataJson += string.Format("\"g\":\"{0}\",", color2);
                    DataJson += string.Format("\"b\":\"{0}\"", color3);
                }
                DataJson += "},";
                DataJson += string.Format("\"is_hyaline\":true");
                DataJson += "}";
                //DataJson的配置见小程序开发文档，B接口：https://mp.weixin.qq.com/debug/wxadoc/dev/api/qrcode.html

                WriteLog.CreateLog("DataJson" + DataJson);
                ret = PostMoths(url, DataJson, imgName, path);
                if (ret.Length > 0)
                {
                    //对图片进行存储操作，下次可直接调用你存储的图片，不用再调用接口
                }
            }
            catch (Exception e)
            {
                ret = e.Message;
                WriteLog.CreateLog("CreateWxCodeBySceneAndPage2-Ex：" + ret);
            }
            return ret;//返回图片地址
        }

        public string PostMoths(string url, string param, string imgName = "", string path = "/upload/qrcode/")
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();//返回图片数据流
            byte[] tt = StreamToBytes(s);//将数据流转为byte[]

            //在文件名前面加上时间，以防重名
            if (string.IsNullOrEmpty(imgName))
            {
                imgName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
            }
            //文件存储相对于当前应用目录的虚拟目录

            //获取相对于应用的基目录,创建目录
            string imgPath = System.Web.HttpContext.Current.Server.MapPath(path);     //通过此对象获取文件名
            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }

            //WriteLog.CreateLog(path + imgName);
            System.IO.File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath(path + imgName), tt);//讲byte[]存储为图片

            //需判断图片大小
            //if(tt.Length )

            return path + imgName;
        }
        ///将数据流转为byte[]
        public static byte[] StreamToBytes(Stream stream)
        {
            List<byte> bytes = new List<byte>();
            int temp = stream.ReadByte();
            while (temp != -1)
            {
                bytes.Add((byte)temp);
                temp = stream.ReadByte();
            }
            return bytes.ToArray();
        }


        //public ContentResult UserCouponCode()
        //{
        //    int uid = RequestEx("id", 0);
        //    string imgName = "";
        //    T_User user = GetUser();
        //    if (user.Id > 0)
        //    {
        //        T_UserCoupon uc = ModelBLL.QueryBySelectWhere<T_UserCoupon>(Common.IsEqualAnd("UserId", "CouponId")
        //            , Dict.T_UserAccount_UserId(user.Id)
        //            , Dict.T_UserCoupon_CouponId(uid)
        //        );
        //        if (uc.Id > 0)
        //        {
        //            imgName = MD5Utility.Encrypt(user.Id + "_" + uid) + ".jpg";
        //            string path = "/upload/code/";
        //            string scene = "";
        //            string page = "";
        //            if (!System.IO.File.Exists(Server.MapPath("~" + imgName)))
        //            {
        //                CreateWxCodeBySceneAndPage2(scene, page, imgName, path);
        //            }
        //            imgName = path + imgName;
        //        }
        //    }
        //    return Content(imgName);
        //}
        #endregion
    }
}

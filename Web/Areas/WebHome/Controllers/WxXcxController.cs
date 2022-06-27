using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using System.Dynamic;

using Commons;
using BLL;
using Model;
using Newtonsoft.Json;
using My.Engine;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.MP;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Transactions;
using System.Data;

using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace Web.Areas.WebHome
{
    public class WxXcxController : XcxControllerBase
    {
        #region 微信参数
        private static TenPayV3Info _tenPayV3Info;
        private static string _webUrl;

        public static string WebUrl
        {
            get
            {
                if (_webUrl == null)
                {
                    _webUrl = ConfigurationManager.AppSettings["WebUrl"];
                }
                return _webUrl;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[ConfigurationManager.AppSettings["TenPayV3_MchId"]];
                }
                return _tenPayV3Info;
            }
        }
        #endregion

        /// <summary>
        /// 验证微信配置
        /// </summary>
        /// <returns></returns>
        public ActionResult Auth(string signature, string timestamp, string nonce, string echostr)
        {
            try
            {
                string token = ConfigurationManager.AppSettings["WxMicroProgramToken"];

                SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
                dict.Add("timestamp", timestamp);
                dict.Add("nonce", nonce);
                dict.Add("token", token);


                StringBuilder sbParam = new StringBuilder();
                List<string> lstParam = new List<string>();

                foreach (string item in dict.Keys)
                {
                    lstParam.Add(dict[item]);
                }

                lstParam.Sort();

                foreach (var item in lstParam)
                {
                    sbParam.Append(item);
                }

                string signResult = SHA1(sbParam.ToString(), Encoding.Default).ToLower();


                #region log
                StringBuilder sbLog = new StringBuilder();
                sbLog.AppendFormat("signature:{0}\r\n", signature);
                sbLog.AppendFormat("timestamp:{0}\r\n", timestamp);
                sbLog.AppendFormat("nonce:{0}\r\n", nonce);
                sbLog.AppendFormat("echostr:{0}\r\n", echostr);
                sbLog.AppendFormat("token:{0}\r\n", token);
                sbLog.AppendFormat("signResult:{0}\r\n", signResult);
                WriteLog.CreateLog(sbLog.ToString());
                #endregion log

                if (signResult == signature)
                {
                    return Content(echostr);
                }
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
            }

            return Content("false");
        }

        #region 加密签名
        private string Sign4WXMGAuth(SortedDictionary<string, string> dict)
        {
            StringBuilder sbParam = new StringBuilder();
            List<string> lstParam = new List<string>();

            foreach (string item in dict.Keys)
            {
                lstParam.Add(dict[item]);
            }

            lstParam.Sort();

            foreach (var item in lstParam)
            {
                sbParam.Append(item);
            }

            string strResult = SHA1(sbParam.ToString(), Encoding.Default);

            return strResult.ToLower();
        }

        private string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public void TestRedirect()
        {
            string token = RequestEx("token");
            Response.Redirect("/Order/List?token=" + token);
            return;
        }

        /// <summary>
        /// 获取openId,自动注册用户
        /// </summary>
        /// <param name="jscode">临时code</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetOpenId(string jscode, string name, string avatarUrl, string province, string city, string encryptedData = "", string iv = "", string wechatshareid = "")
        {
            string appId = ConfigurationManager.AppSettings["XcxAppId"];
            string secret = ConfigurationManager.AppSettings["XcxSecret"];
            string wxUrl = ConfigurationManager.AppSettings["XcxOpenUrl"];

            string url = string.Format("{0}?appid={1}&secret={2}&js_code={3}&grant_type=authorization_code", wxUrl, appId, secret, jscode);

            //WriteLog.CreateLog("GetOpenId-url:" + url);
            string strContent = RequestUtility.Get(url, null);
            WriteLog.CreateLog("GetOpenId-result:" + strContent);
            Commons.Result result = new Commons.Result();
            if (strContent.IndexOf("\"openid\":\"") != -1)
            {
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(strContent);
                string openId = obj.openid;
                string session_key = obj.session_key;
                string unionId = "";


                if (!string.IsNullOrEmpty(encryptedData))
                {
                    #region 用户数据解密
                    //用户数据解密
                    AesIV = iv;
                    AesKey = session_key;

                    string resultData = AESDecrypt(encryptedData);

                    //存储用户数据
                    dynamic _usrInfo = JsonConvert.DeserializeObject(resultData);

                    try //部分验证返回值中没有unionId
                    {
                        unionId = _usrInfo["unionId"].ToString();
                    }
                    catch (Exception)
                    {
                        unionId = "unionId";
                    }

                    #endregion
                }

                return AutoReg(openId, name, avatarUrl, province, city, unionId, wechatshareid);
            }
            else
            {
                if (ConfigurationManager.AppSettings["IsWeiXin"] == "no")
                {
                    string openId = "";
                    T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("NickName"), Dict.T_User_NickName(name));
                    if (string.IsNullOrEmpty(user.OpenId))
                    {
                        openId = Guid.NewGuid().ToString("N");
                    }
                    else
                    {
                        openId = user.OpenId;
                    }
                    return AutoReg(openId, name, avatarUrl, province, city, "", wechatshareid);
                }
                else
                {
                    result.Code = -1;
                    result.Message = "";
                    if (Request.UrlReferrer != null)
                    {
                        result.Message = Request.UrlReferrer.ToString();
                        WriteLog.CreateLog("UrlReferrer:" + Request.UrlReferrer.ToString());
                    }
                    result.Detail = "获取openid失败";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region 微信小程序用户数据解密

        public static string AesKey;
        public static string AesIV;

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="inputdata">输入的数据encryptedData</param>
        /// <param name="AesKey">key</param>
        /// <param name="AesIV">向量128</param>
        /// <returns name="result">解密后的字符串</returns>
        public string AESDecrypt(string inputdata)
        {
            try
            {
                AesIV = AesIV.Replace(" ", "+");
                AesKey = AesKey.Replace(" ", "+");
                inputdata = inputdata.Replace(" ", "+");
                byte[] encryptedData = Convert.FromBase64String(inputdata);

                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = Convert.FromBase64String(AesKey); // Encoding.UTF8.GetBytes(AesKey);
                rijndaelCipher.IV = Convert.FromBase64String(AesIV);// Encoding.UTF8.GetBytes(AesIV);
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);

                return result;
            }
            catch (Exception)
            {
                return null;

            }
        }
        #endregion

        #region 检测是否存在用户，不存在则自动注册用户

        /// <summary>
        /// 检测是否存在用户，不存在则自动注册用户
        /// 使用小程序用户表，需增加公众号表维护，小程序表维护
        /// </summary>
        /// <returns>返回用户信息</returns>
        public JsonResult AutoReg(string openId, string name = "", string avatarUrl = "", string province = "", string city = "", string unionId = "", string wechatshareid = "")
        {
            //string openId = Request["openId"];
            //string name = Request["name"];
            //string avatarUrl = Request["avatarUrl"];
            //string province = Request["province"];
            //string city = Request["city"];

            Commons.Result result = new Commons.Result();

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
                    T_User shareuser = ModelBLL.QueryByID<T_User>(wechatshareid);
                    T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("OpenId"), Dict.T_User_OpenId(openId));
                    WriteLog.CreateLog("AutoReg-User:" + JsonConvert.SerializeObject(user));
                    if (user.Id == 0)
                    {
                        user = new T_User();
                        user.Name = name;
                        user.LoginId = openId;
                        user.Password = MD5Utility.SALTMD5("123456");
                        user.OpenId = openId;
                        user.Guid = GetGuid();
                        user.NickName = name;
                        user.Avatar = avatarUrl;
                        user.Phone = "";
                        user.UnionID = unionId;
                        user.UserId = shareuser.Id;

                        user.Id = ModelBLL.InsertOne(user);

                        //4.添加会员关系
                        string insertt_UserRelationship = string.Empty;
                        if (shareuser.Id == 0)//没有上级
                            insertt_UserRelationship = string.Format("INSERT INTO [t_UserRelationship]([OpenId],[S1],[S2],UnionId) VALUES ('{0}','','','{1}')", openId, unionId);
                        else
                        {
                            if (shareuser.UserId == 0)//只有1级
                                insertt_UserRelationship = string.Format("INSERT INTO [t_UserRelationship]([OpenId],[S1],[S2],UnionId) VALUES ('{0}','{1}','','{2}')", openId, shareuser.OpenId, unionId);
                            else  //有1~2级
                                insertt_UserRelationship = string.Format("INSERT INTO [t_UserRelationship]([OpenId],[S1],[S2],UnionId) SELECT '{0}',[OpenId],S1,'{1}' FROM [T_UserRelationship] WHERE [OpenId]='{2}'", openId, unionId, shareuser.OpenId);
                        }

                        int n = ModelBLL.ExecuteSql(insertt_UserRelationship);
                    }
                    else
                    {
                        user.Guid = GetGuid();
                        user.UnionID = unionId;

                        int n = ModelBLL.UpdateInfoBySelectWhere("T_User", "Guid=@Guid, UnionID=@UnionID", "Id=@Id", Dict.T_User_Id(user.Id), Dict.T_User_Guid(user.Guid), Dict.T_User_UnionID(user.UnionID));
                    }
                    UserLogin login = new UserLogin();
                    //login.Id = user.Guid;
                    login.NickName = user.Name;
                    login.Avatar = user.Avatar;
                    login.OpenId = user.OpenId;
                    login.Token = user.Guid;

                    result.Code = user.Id;
                    result.Detail = JsonConvert.SerializeObject(login);
                    result.Message = "";
                }
                catch (Exception ex)
                {
                    result.Code = -1;
                    result.Message = ex.Message;
                    WriteLog.CreateLog("AutoReg-Ex:" + ex.Message);
                    //HttpContext.Response.Write(ex.Message);
                }
                finally
                {
                    if (Request.UrlReferrer != null)
                    {
                        WriteLog.CreateLog("UrlReferrer:" + Request.UrlReferrer.ToString());
                        result.Message = Request.UrlReferrer.ToString();
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string GetGuid()
        {
            string strGuid = Guid.NewGuid().ToString("N");
            if (ModelBLL.CheckInFo("T_User", Common.IsEqual("Guid"), Dict.T_User_Guid(strGuid)))
            {
                strGuid = GetGuid();
            }
            return strGuid;
        }

        #endregion

        #region 检测用户是否存在，不存在返回信息，客户端要求重新登录

        public void CheckGuExist()
        {
            ReJson(base.CheckUser());
        }

        #endregion

    }
}

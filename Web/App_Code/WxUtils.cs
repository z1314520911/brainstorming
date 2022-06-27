
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Commons;

namespace Web
{
    public class WxUtils
    {


        #region 小程序二维码

        /// <summary>
        /// 根据参数和跳转页面路径生产B类型二维码
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string CreateWxCodeBySceneAndPage2(string scene, string page, string imgName = "", string path = "", int color1 = -1, int color2 = -1, int color3 = -1)
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

        public static string PostMoths(string url, string param, string imgName = "", string path = "/upload/qrcode/")
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


        #endregion

    }
}
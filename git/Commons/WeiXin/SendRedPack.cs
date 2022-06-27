using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;

namespace Commons
{
    public class SendRedPack
    {

        /// <summary>
        /// 调用微信支付接口
        /// </summary>
        /// <param name="payForWeiXin"></param>
        /// <returns></returns>
        public string SendRedPackUrl(string postData)
        {
            string result = string.Empty;
            try
            {
                result = PostPage("https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack", postData);
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
                result = "err";
            }
            return result;
        }


        /// <summary>
        /// post微信请求
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string PostPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...  
            try
            {
                string certPath = System.Web.HttpContext.Current.Server.MapPath(@"\App_Data\apiclient_cert.p12");

                X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(certPath, Config.GetKeyValue("TenPayV3_MchId"), X509KeyStorageFlags.MachineKeySet);
                // 设置参数  
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.ContentLength = data.Length;
                //request.ClientCertificates = "";

                request.ClientCertificates.Add(cert);
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据  
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求  
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码  
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;

            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
                return "err";
            }
        }


        public string RandomStr(string str, int Length)
        {
            string result = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += str[rd.Next(str.Length)];
            }
            return result;
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String Encrypt(String s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }

        public static string GetIP()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageDate = webClient.DownloadData("http://pv.sohu.com/cityjson?ie=utf-8");
                    String ip = Encoding.UTF8.GetString(pageDate);
                    webClient.Dispose();

                    Match rebool = Regex.Match(ip, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    return rebool.Value;
                }
                catch (Exception e)
                {
                    return "";
                }

            }
        }
    }
}

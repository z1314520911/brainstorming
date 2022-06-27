using System;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Commons
{
    /// <summary>
    /// 辅助工具类
    /// </summary>
    public class ExpressUtils
    {
        public static string Get(string code)
        {
            string url = "https://sp0.baidu.com/9_Q4sjW91Qh3otqbppnN2DJv/pae/channel/data/asyncqury?appid=4001&com=&nu=" + code;

            string result = string.Empty;
            string strCookie = string.Empty;
            result = DeUnicode(WebClientUtils.Get(url, Encoding.UTF8, ref strCookie));

            return result;
        }

        #region Post数据 非模仿浏览器 不可用

        public static string GetPost(string url, string content)
        {
            int ctLen = Encoding.UTF8.GetBytes(content).Length;
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();

            return ret;
        }

        #endregion

        #region Unicode

        /// <summary>
        /// Unicode解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeUnicode(string str)
        {
            //最直接的方法Regex.Unescape(str);
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            return reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        }

        /// <summary>
        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        public static string StringToUnicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>    
        /// 字符串转为UniCode码字符串    
        /// </summary>    
        /// <param name="s"></param>    
        /// <returns></returns>    
        public static string StringToUnicode2(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format(@"\u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        #endregion

    }
}

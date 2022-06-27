using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;

namespace Commons
{
    public class CookieUtils
    {
        /// <summary>
        /// 全局变量Cookie名称
        /// </summary>
        public static string CookieName { get { return ConfigurationManager.AppSettings["CookieName"]; } }

        private static string _CookieDomain = ConfigurationManager.AppSettings["CookieDomain"];
        /// <summary>
        /// 设置Cookie,有效期一天
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="cookieName"></param>
        /// <returns>HttpCookie对象</returns>
        public static System.Web.HttpCookie SetCookie<T>(T data, string cookieName) where T : class
        {
            //声明一个序列化类
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            MemoryStream ms = new MemoryStream();  //声明一个内存流
            bf.Serialize(ms, data);  //执行序列化操作
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string cookieValue = System.Convert.ToBase64String(result);
            /*此处为关键步骤，将得到的字节数组按照一定的编码格式转换为字符串，不然当对象包含中文时，进行反序列化操作时会产生编码错误*/
            ms.Flush();
            ms.Close();
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName);  //声明一个Key为person的Cookie对象
            cookie.Expires = DateTime.Now.AddDays(1);  //设置Cookie的有效期到明天为止,此处时间可以根据需要设置
            if (!string.IsNullOrEmpty(_CookieDomain))
                cookie.Domain = _CookieDomain;
            cookie.Value = cookieValue;
            System.Web.HttpContext.Current.Response.SetCookie(cookie);
            return cookie;
        }
        /// <summary>
        /// Cookie或Session转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static T GetCookie<T>(string cookieName) where T : class
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(cookieName);
            if (cookie != null)
            {
                string result = cookie.Value;
                try
                {
                    byte[] b = System.Convert.FromBase64String(result);  //将得到的字符串根据相同的编码格式分成字节数组
                    MemoryStream ms = new MemoryStream(b, 0, b.Length);  //从字节数组中得到内存流
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)bf.Deserialize(ms);  //反序列化得到Person类对象
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 清除客户端cookie值
        /// </summary>
        /// <param name="CookieName"></param>
        static public void ClearCookie(string CookieName)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                if (!string.IsNullOrEmpty(_CookieDomain))
                    cookie.Domain = _CookieDomain;
                ///cookie.Values.Clear();
                System.Web.HttpContext.Current.Response.SetCookie(cookie);
                //System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

    }


    [Serializable]
    public class WxInfo
    {
        public int Id { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> WxType { get; set; }
        public string WxName { get; set; }
        public string WxId { get; set; }
        //public string WeiXinCode { get; set; }
        //public string WxPwd { get; set; }
        //public string HeaderPic { get; set; }
        public string WebUrl { get; set; }
        public string Token { get; set; }
        public string EncodingAESKey { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        //public Nullable<int> States { get; set; }
    }
}


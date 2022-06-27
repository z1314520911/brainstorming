using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Commons
{
    public class RequestUtility
    {
        private static string strUserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko";

        public static string IP
        {
            get
            {
                string result;
                try
                {
                    if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                    {
                        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToLower();
                    }
                    else
                    {
                        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToLower();
                    }
                }
                catch
                {
                    result = "0.0.0.0";
                }
                return result;
            }
        }

        public static string UserAgent
        {
            get
            {
                string result;
                try
                {
                    result = HttpContext.Current.Request.Headers["User-Agent"].Trim();
                }
                catch
                {
                    result = string.Empty;
                }
                return result;
            }
        }

        private static string CreateUrl(string apiPath)
        {
            if (apiPath.ToLower().StartsWith("http://") || apiPath.ToLower().StartsWith("https://"))
            {
                return apiPath;
            }
            return string.Format("{0}/{1}", Config.ApiDomain, apiPath);
        }

        public static string Get(string apiPath, IDictionary<string, string> parameters = null)
        {
            string result;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (KeyValuePair<string, string> current in parameters)
                    {
                        try
                        {
                            stringBuilder.AppendFormat("{0}={1}&", current.Key.Trim(), HttpUtility.UrlEncode(Common.IsString(current.Value).Trim(), Encoding.UTF8));
                        }
                        catch
                        {
                        }
                    }
                }
                stringBuilder.AppendFormat("r={0}", Guid.NewGuid().ToString());
                HttpWebRequest httpWebRequest = WebRequest.Create(string.Format("{0}?{1}", RequestUtility.CreateUrl(apiPath), stringBuilder)) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = RequestUtility.strUserAgent;
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream expr_E5 = httpWebResponse.GetResponseStream();
                    StreamReader expr_F0 = new StreamReader(expr_E5, Encoding.UTF8);
                    string text = expr_F0.ReadToEnd();
                    expr_F0.Close();
                    expr_E5.Close();
                    httpWebRequest.Abort();
                    httpWebResponse.Close();
                    result = text.Trim();
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
                result = string.Empty;
            }
            return result;
        }

        public static string Get(string apiPath)
        {
            string result;
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(apiPath) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = RequestUtility.strUserAgent;
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream expr_E5 = httpWebResponse.GetResponseStream();
                    StreamReader expr_F0 = new StreamReader(expr_E5, Encoding.UTF8);
                    string text = expr_F0.ReadToEnd();
                    expr_F0.Close();
                    expr_E5.Close();
                    httpWebRequest.Abort();
                    httpWebResponse.Close();
                    result = text.Trim();
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
                result = string.Empty;
            }
            return result;
        }

        public static string Post(string apiPath, IDictionary<string, string> parameters = null)
        {
            string result;
            try
            {
                byte[] array = null;
                StringBuilder stringBuilder = new StringBuilder();
                new CookieContainer();
                HttpWebRequest httpWebRequest = WebRequest.Create(RequestUtility.CreateUrl(apiPath)) as HttpWebRequest;
                httpWebRequest.Method = "POST";
                httpWebRequest.UserAgent = RequestUtility.strUserAgent;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.AllowAutoRedirect = false;
                if (parameters != null && parameters.Keys.Count > 0)
                {
                    foreach (KeyValuePair<string, string> current in parameters)
                    {
                        stringBuilder.AppendFormat("{0}={1}&", current.Key.Trim(), Common.IsString(current.Value).Trim());
                    }
                    stringBuilder.AppendFormat("r={0}", Guid.NewGuid().ToString());
                    array = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                    httpWebRequest.ContentLength = (long)array.Length;
                }
                Stream stream = httpWebRequest.GetRequestStream();
                if (array != null && array.Length != 0)
                {
                    stream.Write(array, 0, array.Length);
                }
                stream.Close();
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    stream = httpWebResponse.GetResponseStream();
                    StreamReader expr_13E = new StreamReader(stream, Encoding.UTF8);
                    string text = expr_13E.ReadToEnd();
                    expr_13E.Close();
                    stream.Close();
                    httpWebRequest.Abort();
                    httpWebResponse.Close();
                    result = text.Trim();
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
                result = string.Empty;
            }
            return result;
        }

        public static string PostJson(string url, string param)
        {
            string result = string.Empty;
            try
            {
                string strURL = url;
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
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
                s = response.GetResponseStream();
                string StrDate = "";
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    result += StrDate + "\r\n";
                }
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog(ex);
                result = string.Empty;
            }
            return result;
        }
    }
}

using System;
using System.Text;
using System.Net;
using System.IO;

namespace Commons
{
    public class Sms106
    {
        private static string _user = "";
        private static string _pwd = "";

        public static Result sendSms(string mobile, string content)
        {
            Result result = new Result();

            //发送验证码
            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, _user, _pwd, mobile, content));

            string url = "http://sms.jiekou.com/sms";
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            myRequest.ContentLength = postData.Length;

            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string strResult = reader.ReadToEnd();  //100为成功
                //反序列化upfileMmsMsg.Text
                //实现自己的逻辑

                result.Code = 1;
                switch (strResult)
                {
                    case "100":
                        {
                            result.Message = "发送成功，请查阅短信"; 
                            break;
                        }
                    case "108":
                        {
                            result.Code = -1;
                            result.Message = "手机号码发送太频繁，请换号或隔天再发"; 
                            break;
                        }
                    default:
                        {
                            result.Code = -1;
                            result.Message = "发送失败，请咨询技术";
                            break;
                        }
                }
                result.OprTime = DateTime.Now;
            }
            else
            {
                //访问失败
                result.Code = -1;
                result.Message = "发送失败";
                result.OprTime = DateTime.Now;
            }

            return result;
        }
    }
}

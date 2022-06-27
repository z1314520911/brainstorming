using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Text;

namespace Commons
{
    public class WriteLog
    {
        /// <summary> 
        /// 创建日志文件 
        /// </summary> 
        /// <param name="ex">异常类</param> 
        public static void CreateLog(Exception ex)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/log/");
            if (!Directory.Exists(path))
            {
                //创建日志文件夹 
                Directory.CreateDirectory(path);
            }
            //发生异常每天都创建一个单独的日子文件[*.log],每天的错误信息都在这一个文件里。方便查找 
            path += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            WriteLogInfo(ex, path);
        }
        public static void CreateLog(string name, string value)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/log/");
            if (!Directory.Exists(path))
            {
                //创建日志文件夹 
                Directory.CreateDirectory(path);
            }
            path += "\\" + name + ".log";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(value);
            }
        }
        public static string ReadLog(string fp = "bin", string name = "appConfig")
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + fp + "/");
            path += "\\" + name + ".log";
            string res = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                res = sr.ReadToEnd();
            }
            return res;
        }
        /// <summary> 
        /// 写日志信息 
        /// </summary> 
        /// <param name="ex">异常类</param> 
        /// <param name="path">日志文件存放路径</param> 
        private static void WriteLogInfo(Exception ex, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                               + DateTime.Now.ToLongTimeString()
                               + "】*****************************************");
                if (ex != null)
                {
                    sw.WriteLine("【ErrorType】" + ex.GetType());
                    sw.WriteLine("【TargetSite】" + ex.TargetSite);
                    sw.WriteLine("【Message】" + ex.Message);
                    sw.WriteLine("【Source】" + ex.Source);
                    sw.WriteLine("【StackTrace】" + ex.StackTrace);
                }
                else
                {
                    sw.WriteLine("Exception is NULL");
                }
                sw.WriteLine();
            }
        }
        /// <summary> 
        /// 写日志信息 
        /// </summary> 
        /// <param name="value">异常类</param> 
        public static void CreateLog(string value)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/log/");
            path += "\\" + DateTime.Now.ToString("yyyy");
            if (!Directory.Exists(path))
            {
                //创建日志文件夹 
                Directory.CreateDirectory(path);
            }
            //发生异常每天都创建一个单独的日子文件[*.log],每天的错误信息都在这一个文件里。方便查找 
            path += "\\" + DateTime.Now.ToString("MMdd") + ".log";
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                               + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                               + "】*****************************************");
                sw.WriteLine(value);
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine();
            }
        }

        /// <summary> 
        /// 写日志信息 
        /// </summary> 
        /// <param name="value">异常类</param> 
        public static void Log(string value, string fileName = "error")
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/log/");
            path += "\\" + DateTime.Now.ToString("yyyy");
            if (!Directory.Exists(path))
            {
                //创建日志文件夹 
                Directory.CreateDirectory(path);
            }
            //发生异常每天都创建一个单独的日子文件[*.log],每天的错误信息都在这一个文件里。方便查找 
            path += "\\" + DateTime.Now.ToString("MMdd") + "_" + fileName + ".log";
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                               + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                               + "】*****************************************");
                sw.WriteLine(value);
                //sw.WriteLine("********************************************************************************************************");
                //sw.WriteLine();
            }
        }
    }
}
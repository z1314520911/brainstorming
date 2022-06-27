using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Commons;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.TenPayLibV3;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            AreaRegistrationOrder.RegisterAllAreasOrder();

            var tenPayV3_MchId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"];
            var tenPayV3_Key = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_Key"];
            var tenPayV3_AppId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppId"];
            var tenPayV3_AppSecret = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppSecret"];
            var tenPayV3_TenpayNotify = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_TenpayNotify"];

            //var weixinPayInfo = new TenPayInfo(weixinPay_PartnerId, weixinPay_Key, weixinPay_AppId, weixinPay_AppKey, weixinPay_TenpayNotify);
            //TenPayInfoCollection.Register(weixinPayInfo);
            var tenPayV3Info = new TenPayV3Info(tenPayV3_AppId, tenPayV3_AppSecret, tenPayV3_MchId, tenPayV3_Key, tenPayV3_TenpayNotify);
            TenPayV3InfoCollection.Register(tenPayV3Info);

            //全局注册Token
            AccessTokenContainer.Register(tenPayV3_AppId, tenPayV3_AppSecret);
        }

        /// <summary>
        /// 请求的时候做一些处理。(每一个应用都会触发这里)       
        /// </summary>
        /// <param name="sender"></param>
        /// 查看当前请求的URL，通过这个（HttpContext.Current.Request.URL）在
        /// 快速监听里面查看总共请求了哪些URL
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //string absoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;//实际访问的url，包含参数

            //string guid = Request["token"];
            //T_User user = new T_User();
            //if (!string.IsNullOrEmpty(guid))
            //{
            //    user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("Guid"), Dict.T_User_Guid(guid));
            //}

            //防盗链
            //if ((HttpContext.Current.Request.Url.AbsolutePath.IndexOf(".jpg") != -1 
            //    || HttpContext.Current.Request.Url.AbsolutePath.IndexOf(".png") != -1)
            //    && (HttpContext.Current.Request.UrlReferrer.Host != "localhost"
            //    || HttpContext.Current.Request.UrlReferrer.Host != "localhost") //小程序api端？
            //    )
            //{
            //    HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/imgs/forbid.png"));
            //    HttpContext.Current.Response.End();
            //}

            //遍历Post参数，隐藏域除外 
            foreach (string i in this.Request.Form)
            {
                if (i == "__VIEWSTATE") continue;
                goErr(this.Request.Form[i].ToString());
            }
            //遍历Get参数。 
            foreach (string i in this.Request.QueryString)
            {
                goErr(this.Request.QueryString[i].ToString());
            }

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            double time = (DateTime.Now - HttpContext.Current.Timestamp).TotalMilliseconds;
            string log = string.Format("Ip：{0}。响应：{1}毫秒。请求：{2}", Ip(), time, HttpContext.Current.Request.RawUrl);
            if (Config.GetKeyValue("IsLogRequestTime").Equals("true"))
            {
                WriteLog.Log(log, "time");
            }
            if (time > 3000)
            {
                WriteLog.Log(log, "error");
            }
        }


        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();

            #region 错误日志

            Exception ex = lastError.GetBaseException();
            StringBuilder strErr = new StringBuilder();
            //strErr.Append("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            strErr.Append("客户信息：");
            strErr.Append("\t Ip:" + Ip());
            strErr.Append("\t 浏览器:" + Request.Browser.Browser.ToString());
            strErr.Append("\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
            strErr.Append("\t操作系统:" + Request.Browser.Platform.ToString());
            strErr.Append("\r\n错误信息：");

            int httpCode = 0;
            string strExceptionMessage = string.Empty;
            if (lastError != null)
            {
                HttpException httpError = lastError as HttpException;
                if (httpError != null)
                {
                    //获取错误代码
                    httpCode = httpError.GetHttpCode();
                    strExceptionMessage = httpError.Message;
                    //if (httpCode == 400 || httpCode == 404)
                    //{
                    //    Response.StatusCode = 404;
                    //    /*
                    //     * 跳转到指定的http 500错误信息页面
                    //     * 跳转到静态页面一定要用Response.WriteFile方法                 
                    //     */
                    //    //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
                    //    Server.ClearError();
                    //    Response.WriteFile("~/HttpError/404.html");
                    //    return;
                    //}
                }

            }

            strErr.Append("\r\n\t 页面：" + Request.Url.ToString());
            if (httpCode > 0)
            {
                strErr.Append("\r\n\t 状态：" + httpCode + "\t" + strExceptionMessage);
            }
            strErr.Append("\r\n\t 错误源：" + ex.Source);
            strErr.Append("\r\n\t 异常方法：" + ex.TargetSite);
            strErr.Append("\r\n\t 错误信息：" + ex.Message);
            //strErr.Append("\r\n\t 堆栈信息：" + ex.StackTrace);
            List<string> list = Zz.SubstringMultiple(ex.StackTrace, "位置", "在");
            StringBuilder strHh = new StringBuilder();
            foreach (string error in list)
            {
                strHh.Append("\t " + error);
            }
            strErr.Append("\r\n\t 错误行号：\r\n" + strHh.ToString());

            WriteLog.Log(strErr.ToString());

            #endregion
            //处理完及时清理异常
            //Server.ClearError();
            //跳转至出错页面
            //Response.Redirect("http://error.html");
            //Response.Redirect("~/HttpError/error.html");

        }

        #region 过滤sql注入

        private void goErr(string tm)
        {
            if (SqlFilter(tm))
            {
                string log = string.Format("Ip：{0}。请求：{1}。请求含有非法字符串：{2}", Ip(), HttpContext.Current.Request.RawUrl, tm);
                WriteLog.Log(log, "error");
                Response.Write("请求含有非法字符串，已记录");
                Response.End();
            }
            //%SystemRoot%\system32;C:\Users\Administrator\AppData\Local\Programs\Microsoft VS Code\bin;C:\Users\Administrator\.dotnet\tools;C:\Users\Administrator\AppData\Roaming\npm
            //%CATALINA_HOME%\bin;%CATALINA_HOME%\lib;%Android_Home%;%SystemRoot%\system32;%SystemRoot%;%SystemRoot%\system32\wbem;D:\Program Files\nodejs\
        }
        private bool SqlFilter(string InText)
        {
            string word = "and★exec★insert★select★delete★update★chr★mid★master★truncate★char★declare★join★net user★xp_cmdshell";
            if (InText == null)
                return false;
            foreach (string i in word.Split('★'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            if ((InText.ToLower().IndexOf(" or ") > -1))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region 获取客户IP

        private string Ip()
        {
            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            return ip;
        }

        #endregion
    }

    #region 根据顺序注册Areas
    /// <summary>
    /// 根据顺序注册Areas
    /// </summary>
    abstract public class AreaRegistrationOrder : AreaRegistration
    {
        /// <summary>
        /// 存放AreaContent
        /// </summary>
        protected static List<AreaRegistrationContext> areaContent = new List<AreaRegistrationContext>();

        /// <summary>
        /// 存放AreaRegistration
        /// </summary>
        protected static List<AreaRegistrationOrder> areaRegistration = new List<AreaRegistrationOrder>();

        /// <summary>
        /// 劫持
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            areaContent.Add(context);
            areaRegistration.Add(this);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="context"></param>
        public abstract void RegisterAreaOrder(AreaRegistrationContext context);

        /// <summary>
        /// 顺序
        /// </summary>
        public abstract int Order { get; }

        /// <summary>
        /// 按照顺序注册Areas
        /// </summary>
        public static void RegisterAllAreasOrder()
        {
            RegisterAllAreas();
            Register();
        }

        /// <summary>
        /// 注册
        /// </summary>
        private static void Register()
        {
            List<int[]> order = new List<int[]>();
            for (int k = 0; k < areaRegistration.Count; k++)
            {
                order.Add(new int[] { areaRegistration[k].Order, k });
            }
            order = order.OrderBy(o => o[0]).ToList();
            foreach (var o in order)
            {
                areaRegistration[o[1]].RegisterAreaOrder(areaContent[o[1]]);
            }
        }
    }
    #endregion
}
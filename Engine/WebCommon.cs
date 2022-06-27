
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;

using Commons;
using Model;


namespace Engine
{
    /// <summary>
    /// 基类公用类，提供公用的属性、方法和上下文信息。
    /// </summary>
    internal class WebCommon
    {
        #region 私有变量
        private static readonly int _intMinutes;
        #endregion 私有变量

        #region 静态构造函数
        static WebCommon()
        {
            _intMinutes = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LoginTimeOutMinutes"]) ? 1140 : Convert.ToInt32(ConfigurationManager.AppSettings["LoginTimeOutMinutes"]);
        }
        #endregion 静态构造函数

        #region 公用属性
        internal static int LoginOutMinites
        {
            get
            {
                return _intMinutes;
            }
        }
        #endregion 公用属性

        #region 获取登录用户信息
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        internal static UserLogin GetLoginUserInfo()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                return BinaryUtility.DeserializeFromByte<UserLogin>(StringUtility.FromBase64ToByte(((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData));
            }
            else
            {
                return null;
            }
        }
        #endregion 获取登录用户信息

        #region 设置登录状态
        /// <summary>
        /// 设置登录状态
        /// </summary>
        /// <param name="loginID">会员登录帐号</param>
        /// <param name="userData">会员数据</param>
        internal static void SetLoginState(string loginID, string userData)
        {
            SetLoginState(loginID, userData, new TimeSpan(0, _intMinutes, 0));
        }

        /// <summary>
        /// 设置登录状态
        /// </summary>
        /// <param name="loginID">会员登录帐号</param>
        /// <param name="userData">会员数据</param>
        /// <param name="loginSpan">过期时长</param>
        internal static void SetLoginState(string loginID, string userData, TimeSpan loginSpan)
        {
            try
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, loginID, DateTime.Now, DateTime.Now.Add(loginSpan), true, userData);
                string strTicket = FormsAuthentication.Encrypt(ticket);

                HttpCookie ckiLogin = new HttpCookie(FormsAuthentication.FormsCookieName, strTicket);

                // 如果过期时间间隔不等于默认过期时间间隔，则设置 Cookie 的过期时间。
                if (!double.Equals(_intMinutes, Convert.ToInt32(loginSpan.TotalMinutes)))
                {
                    ckiLogin.Expires = DateTime.Now.Add(loginSpan);
                }
                //if (ticket.IsPersistent)
                //{
                //    ckiLogin.Expires = ticket.Expiration;
                //}

                HttpContext.Current.Response.Cookies.Add(ckiLogin);
            }
            catch (Exception ex)
            {
                WriteLog.CreateLog("SetLoginState:" + ex.Message);
            }

            //WebAPIControllerBase.SetLoginState(loginID, userData, loginSpan);
        }
        #endregion 设置登录状态

        #region 用户退出
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        internal static Result Logout()
        {
            Result result = null;

            try
            {
                FormsAuthentication.SignOut();

                result = new Result()
                {
                    Code = 1,
                    Message = "退出成功！",
                    Detail = string.Empty,
                    OprTime = DateTime.Now
                };
            }
            catch (Exception exp)
            {
                result = new Result()
                {
                    Code = -1,
                    Message = exp.Message,
                    Detail = exp.ToString(),
                    OprTime = DateTime.Now
                };
            }

            return result;
        }
        #endregion 用户退出
    }
}

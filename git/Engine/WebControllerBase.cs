using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

using Commons;
using Model;
using BLL;

namespace Engine
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    [WebAuthorize]
    public class WebControllerBase : Controller
    {
        string _pageIdentity = string.Empty;

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

        #region 获取当前操作员信息
        public T_User GetT_User
        {
            get
            {
                try
                {
                    T_User user = BLL.ModelBLL.QueryByOpenId<T_User>("");
                    return user;
                }
                catch (Exception ex)
                {
                    return new T_User();
                }
            }
        }
        #endregion

        #region 前端用户登录
        /// <summary>
        /// 前端用户登录
        /// </summary>
        /// <param name="loginID">登录帐号</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        protected Result Login(string loginNo, string password, string remember)
        {
            TimeSpan tsRemember = new TimeSpan(0, WebCommon.LoginOutMinites, 0);
            #region 判断是否需要设置登录 Cookie 失效时间间隔。
            switch (remember)
            {
                case "1":       // 保存一周。
                    {
                        tsRemember = new TimeSpan(0, 10080, 0);

                        break;
                    }

                case "2":       // 保存一个月。
                    {
                        tsRemember = new TimeSpan(0, 43200, 0);

                        break;
                    }
            }
            #endregion 判断是否需要设置登录 Cookie 失效时间间隔。
            return this.Login(loginNo, password, tsRemember);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="openId">微信openId</param>
        /// <returns></returns>
        protected Result Login(string openId, string remember)
        {
            TimeSpan tsRemember = new TimeSpan(0, WebCommon.LoginOutMinites, 0);
            #region 判断是否需要设置登录 Cookie 失效时间间隔。
            switch (remember)
            {
                case "1":       // 保存一周。
                    {
                        tsRemember = new TimeSpan(0, 10080, 0);

                        break;
                    }

                case "2":       // 保存一个月。
                    {
                        tsRemember = new TimeSpan(0, 43200, 0);

                        break;
                    }
            }
            #endregion 判断是否需要设置登录 Cookie 失效时间间隔。
            return this.Login(openId, (new TimeSpan(0, WebCommon.LoginOutMinites, 0)));
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="openId">微信openId</param>
        /// <returns></returns>
        protected Result Login(string openId, TimeSpan loginSpan)
        {
            Result result = new Result(-1, "登录失败，用户不存在");
            T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("Phone"), Dict.T_User_OpenId(openId));

            if (user.Id > 0)
            {
                int intId = user.Id;

                UserLogin userInfo = new UserLogin()
                {
                    Id = intId,
                    NickName = user.NickName,
                    Avatar = user.Phone,
                    OpenId = user.OpenId
                };

                result.Code = user.Id;
                result.Detail = JsonUtility.Serialize(user);


                // 转换用户数据为字符串，将保存到登录 Cookie。
                byte[] byteBasUser = BinaryUtility.SerializeToByte<UserLogin>(userInfo);
                string strBasUser = StringUtility.FromByteToBase64(byteBasUser);

                Session["WXLogin"] = openId;
                // 设置登录状态。
                WebCommon.SetLoginState(openId, strBasUser, loginSpan);
            }
            return result;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginID">登录帐号</param>
        /// <param name="password">登录密码</param>
        /// <param name="loginSpan">过期时间间隔</param>
        /// <returns></returns>
        protected Result Login(string loginNo, string password, TimeSpan loginSpan)
        {
            Result result = new Result(-1, "登录失败，用户不存在");
            T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqualAnd("LoginId", "Password"), Dict.T_User_LoginId(loginNo), Dict.T_User_Password(password));

            if (user.Id > 0)
            {
                int intId = user.Id;

                UserLogin userInfo = new UserLogin()
                {
                    Id = intId,
                    NickName = user.NickName,
                    Avatar = user.Phone,
                    OpenId = user.OpenId
                };

                result.Code = user.Id;
                result.Detail = JsonUtility.Serialize(user);


                // 转换用户数据为字符串，将保存到登录 Cookie。
                byte[] byteBasUser = BinaryUtility.SerializeToByte<UserLogin>(userInfo);
                string strBasUser = StringUtility.FromByteToBase64(byteBasUser);

                Session["WXLogin"] = user.OpenId;
                // 设置登录状态。
                WebCommon.SetLoginState(loginNo, strBasUser, loginSpan);
            }
            return result;
        }

        #endregion 用户登录

        #region 用户退出
        protected Result Logout()
        {
            return WebCommon.Logout();
        }
        #endregion 用户退出

        #region 清理缓存

        public void RemoveCache(string cacheName)
        {
            if (cacheName.Length > 0)
            {
                HttpRuntime.Cache.Remove(cacheName.ToString());
            }
            else
            {
                var cache = HttpRuntime.Cache.GetEnumerator();
                while (cache.MoveNext())
                {
                    HttpRuntime.Cache.Remove(cache.Key.ToString());
                }
            }
        }

        #endregion

        #region 时间编号 yyyyMMddHHmmssfff
        public string NewOrderNo()
        {
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), string.Format("{0:000}", new Random().Next(0, 999)));
        }
        #endregion 预约回收基础方法

        #region 获取微信配置信息

        #region 从缓存获取微信配置信息

        protected WxInfo GetWxConfig
        {
            get
            {
                return WxConfig.WxRequest();
            }
        }

        #endregion

        #region 微信服务号编号 AppId AppSecret
        /// <summary>
        /// 微信服务号编号
        /// </summary>
        protected int WxcId
        {
            get
            {
                return GetWxConfig.Id.Int();
            }
        }

        /// <summary>
        /// 微信服务号编号
        /// </summary>
        protected string AppId
        {
            get
            {
                return GetWxConfig.AppId.String();
                //"未查询到AppId"
            }
        }

        /// <summary>
        /// 微信服务号编号
        /// </summary>
        protected string AppSecret
        {
            get
            {
                return GetWxConfig.AppSecret.String();
                //"未查询到AppSecret"
            }
        }

        #endregion

        #endregion
    }
}

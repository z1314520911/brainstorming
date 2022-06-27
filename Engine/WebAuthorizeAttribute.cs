using System.Configuration;
using System.Web;
using System.Web.Mvc;

using Model;
using BLL;
using Commons;

namespace Engine
{
    public class WebAuthorizeAttribute : AuthorizeAttribute
    {
        private string _popedom = string.Empty;
        private UserLogin _currentUser = null;
        private string IsWeiXin = new XmlConfig("Cn").IsWeiXin;
        private string reUrl = string.Empty;

        /// <summary>
        /// 重写基类方法，校验用户权限。
        /// </summary>
        /// <param name="httpContext">当前请求上下文</param>
        /// <returns>有访问权限：true；无访问权限：false。</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                if (httpContext == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(IsWeiXin) && IsWeiXin.Equals("yes"))
                {
                    string controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
                    string action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
                    reUrl = "/" + controller + "/" + action;
                    if (HttpContext.Current.Session["OAuthAccessToken"] == null)
                    {
                        return false;
                    }
                }
                else
                {
                    // 用户未登录。
                    if (!httpContext.Request.IsAuthenticated)
                    {
                        return false;
                    }
                }

                _currentUser = WebCommon.GetLoginUserInfo();

                // 获取用户登录信息失败。
                if (_currentUser == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(_popedom))
                {
                    // 判断是否有使用权限。
                    return HasPopedom(_currentUser.Id, _popedom);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #region 公用属性
        /// <summary>
        /// 用户权限。
        /// </summary>
        public string Popedom
        {
            get
            {
                return _popedom ?? string.Empty;
            }

            set
            {
                _popedom = value;
            }
        }
        #endregion 公用属性

        #region 权限/用户基础数据处理
        /// <summary>
        /// 判断用户是否有使用权限。
        /// </summary>
        /// <param name="userID">用户 ID</param>
        /// <param name="popedomCode">权限编码</param>
        /// <returns></returns>
        private bool HasPopedom(int userID, string popedomCode)
        {
            return false;
            //return (new BasUserRoleService()).HasPopedom(userID, popedomCode);
        }
        #endregion 权限/用户基础数据处理
    }
}

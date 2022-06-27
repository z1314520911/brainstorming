using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Areas.Member
{
    public class MemberAreaRegistration : AreaRegistrationOrder
    {
        public override string AreaName
        {
            get
            {
                return "Member";
            }
        }

        public override void RegisterAreaOrder(AreaRegistrationContext context)
        {
            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            context.MapRoute(
                name: "Member1",
                url: "Member/{controller}/{action}.aspx",
                defaults: new { controller = "Home", action = "Index", id = 0 }
            );
            context.MapRoute(
                name: "Member2",
                url: "Member/{controller}/{action}/{id}.aspx",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
        public override int Order
        {
            get
            {
                return 2;
            }
        }
    }
}


namespace Web.Areas.WebHome
{
    public class WebHomeAreaRegistration : AreaRegistrationOrder
    {
        public override string AreaName
        {
            get
            {
                return "WebHome";
            }
        }

        public override void RegisterAreaOrder(AreaRegistrationContext context)
        {
            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //context.MapRoute(
            //    name: "Default2",
            //    url: "{action}.aspx",
            //    defaults: new { controller = "Home", action = "Index", id = 0 }
            //);
            context.MapRoute(
                "Wx", // 路由名称
                "weixin/{action}", // 带有参数的 URL
                new { controller = "WeiXin", action = "Index", type = UrlParameter.Optional } // 参数默认值
            );
            context.MapRoute(
                name: "Web_default2",
                url: "{action}/{id}.aspx",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", id = 0 }
            );
        }
        public override int Order
        {
            get
            {
                return 3;
            }
        }
    }
}

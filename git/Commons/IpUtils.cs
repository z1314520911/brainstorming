
using System.Web;

namespace Commons
{
    public class IpUtils
    {
        public static string IP
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                    {
                        return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToLower();
                    }
                    else
                    {
                        return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToLower();
                    }
                }
                catch
                {
                    return "0.0.0.0";
                }
            }
        }
    }
}

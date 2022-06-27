using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Net;
using System.IO;
//using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System.Configuration;
using Engine;
using Commons;
using BLL;
using Model;
using Newtonsoft.Json;
using System.Numerics;
using Senparc.Weixin.MP.Helpers;
using My.Engine;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace Web.Areas.WebHome
{
    public class HomeController : XcxControllerBase
    {
        //
        // GET: /Index/

        public void Index()
        {
            DataTable dtBannerList = ModelBLL.getPage(0, "Name,Pic,Url,OpenType", "T_Advert", "", "");

            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            DataTable dtCoupon = ModelBLL.getPage(0, "Id,Name,Pic,StartDate,EndDate,NumberGet,case when Number=0 then 0 else round(cast(NumberSent as float)*100/Number,2) end as [Percent]", "T_Coupon", "Status = 1 and StartDate <= getdate() and EndDate >= getdate() ", "Id desc");

            dtCoupon = DefaultImg(dtCoupon);

            dictionary.Add("CouponList", dtCoupon);
            dictionary.Add("BannerList", dtBannerList);

            ReJson(dictionary, "MM月dd日");
        }
        

        #region 获取并返回小程序环境

        public string GetXcxInfo()
        {
            string strReferrer = string.Empty;

            if (Request.UrlReferrer != null)
            {
                strReferrer = Request.UrlReferrer.ToString();
            }

            return strReferrer;
        }

        #endregion

        

        #region 默认图片
        public static string DefaultImg(object img)
        {
            return string.IsNullOrEmpty(Convert.ToString(img)) ? "/img/yhj.png" : img.ToString();
        }
        public static DataTable DefaultImg(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["Pic"] = DefaultImg(row["Pic"]);
            }
            return dt;
        }
        public static string DefaultShop(object img)
        {
            return string.IsNullOrEmpty(Convert.ToString(img)) ? "/img/md.png" : img.ToString();
        }
        public static DataTable DefaultShop(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["Logo"] = DefaultShop(row["Logo"]);
            }
            return dt;
        }
        #endregion
    }
}

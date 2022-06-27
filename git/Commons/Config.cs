using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Web.Mvc;

namespace Commons
{
    public class Config
    {
        #region 公共信息
        /// <summary>
        /// 获取config值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetKeyValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }        
        /// <summary>
        /// 网站网址
        /// </summary>
        public static readonly string HomeUrl = "";
        /// <summary>
        /// 数据库前缀
        /// </summary>
        public static string DatabasePrefix(object table)
        {
            //return table.ToString();
            return "[" + "" + Convert.ToString(table) + "]";
        }
        /// <summary>
        /// 1代表关闭多国语言选择,2开启多国语言选择
        /// </summary>
        public static readonly string Language = "1";
        /// <summary>
        /// 动态加载网站语言
        /// </summary>
        public static readonly string[,] Languages = new string[,] { { "中文版", "Cn" }, { "英文版", "En" } };
        //public static readonly string[,] Languages = new string[,] { { "英文版", "En" } };
        /// <summary>
        /// FLASH显示条数
        /// </summary>
        public static readonly int FlashNum = 6;
        /// <summary>
        /// Config配置文件所在目录
        /// </summary>
        public static readonly string ConfigPath = IsWeb("~/App_Data/Config.xml");
        /// <summary>
        /// Email配置文件所在目录
        /// </summary>
        public static readonly string EmailPath = IsWeb("~/App_Data/Email.xml");
        /// <summary>
        /// 网站板块配置文件所在目录
        /// </summary>
        public static readonly string ClassPath = IsWeb("~/App_Data/class.xml");
        /// <summary>
        /// 上传文件所在目录
        /// </summary>
        public static readonly string UploadPath = IsWeb("~/upload/");
        /// <summary>
        /// 上传图片Url
        /// </summary>
        public static readonly string UploadUrl = "/upload/";
        /// <summary>
        /// Model字段
        /// </summary>
        public static readonly string DataCollectionPath = IsWeb("~/App_Data/DataCollection.config");
        /// <summary>
        /// 后台分页条数
        /// </summary>
        public static readonly int CurrentRow = GetKeyValue("rows").Int() > 0 ? GetKeyValue("rows").Int() : 15;

        //public static readonly string DataCollectionPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/DataCollection.config");

        public static readonly string CommondllPath = IsWeb("~/bin/Commons.dll");
        
        public static string ApiDomain
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("ApiDomain") && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ApiDomain"]))
                {
                    return ConfigurationManager.AppSettings["ApiDomain"].ToLower();
                }
                return string.Format("/api", new object[0]);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetSex(string SelectValue)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "男", Value = "0" });
            items.Add(new SelectListItem { Text = "女", Value = "1" });
            Common.SetSelectListItem(ref items, SelectValue);
            return items;
        }
        /// <summary>
        /// 支付类型
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetPayType(string SelectValue = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "手工充值", Value = "1" });
            items.Add(new SelectListItem { Text = "支付宝充值", Value = "2" });
            items.Add(new SelectListItem { Text = "财付通充值", Value = "3" });
            items.Add(new SelectListItem { Text = "手Q充值", Value = "4" });
            items.Add(new SelectListItem { Text = "微信充值", Value = "5" });
            Common.SetSelectListItem(ref items, SelectValue);
            return items;
        }
        /// <summary>
        /// 支付类型
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetGoodsParentId(string SelectValue = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "按时间自动开奖", Value = "1" });
            items.Add(new SelectListItem { Text = "按人数自动开奖", Value = "2" });
            items.Add(new SelectListItem { Text = "手动开奖", Value = "3" });
            Common.SetSelectListItem(ref items, SelectValue);
            return items;
        }

        public static string IsWeb(string MapPath)
        {
            try
            {
                return System.Web.HttpContext.Current.Server.MapPath(MapPath);
            }
            catch
            {
                //WindowsService使用 添加
                return AppDomain.CurrentDomain.BaseDirectory + MapPath.Replace("~/bin/", "");
            }
        }
        #endregion

        #region 通用 Js

        /// <summary>
        /// 弹窗选择返回 /Content/admin/js/iframe.js
        /// </summary>
        public static readonly string S_Iframe = "<script type=\"text/javascript\" src=\"/Content/admin/js/iframe.js?v=" + GetKeyValue("version") + "\"></script>";

        /// <summary>
        /// 弹窗外键列表页 /Content/admin/js/JScript.js
        /// </summary>
        public static readonly string S_JScript = "<script type=\"text/javascript\" src=\"/Content/admin/js/JScript.js?v=" + GetKeyValue("version") + "\"></script>";

        /// <summary>
        /// 城市联动选择 /Content/admin/js/city.js
        /// </summary>
        public static readonly string S_City = "<script type=\"text/javascript\" src=\"/Content/admin/js/city.js?v=" + GetKeyValue("version") + "\"></script>";

        #endregion
    }
}
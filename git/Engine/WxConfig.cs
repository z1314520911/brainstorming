using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Configuration;

using Commons;
using Model;
using BLL;

public static class WxConfig
{
    //暂时不写sql配置

    public static WxInfo WxRequest()
    {
        WxInfo wxConfig = new WxInfo();

        wxConfig.AppId              = Config.GetKeyValue("GZH_AppId");
        wxConfig.AppSecret          = Config.GetKeyValue("GZH_Secret");
        wxConfig.Token              = Config.GetKeyValue("GZH_Token");
        wxConfig.EncodingAESKey     = Config.GetKeyValue("GZH_EncodingAESKey");
        wxConfig.WebUrl             = Config.GetKeyValue("GZH_WebUrl");

        return wxConfig;

        //WxInfo wxConfig = new WxInfo();
        //T_DomainUnitService duService = new T_DomainUnitService();
        //Sys_WxConfigService wxService = new Sys_WxConfigService();
        //try
        //{
        //    string strUrl = HttpContext.Current.Request.Url.DnsSafeHost;
        //    //LogUtility.WriteLog("", "strUrl", strUrl);
        //    //CookieUtils.ClearCookie("WxConfig");
        //    //WxInfo wxInfo2 = CookieUtils.GetCookie<WxInfo>("WxConfig");
        //    if (CookieUtils.GetCookie<WxInfo>("WxConfig") == null)
        //    {
        //        dynamic obj = duService.List(0, 0, 0, strUrl);
        //        //LogUtility.WriteLog("", "obj.rows", JsonUtility.Serialize(obj.rows));
        //        if (obj.total > 0)
        //        {
        //            List<T_DomainUnit> list = JsonUtility.Deserialize<List<T_DomainUnit>>(JsonUtility.Serialize(obj.rows));
        //            int WxcId = list[0].WxcId.Value;

        //            string json = string.Empty;
        //            dynamic tmpObj = wxService.List(0, 0, WxcId, "", "");
        //            List<Sys_WxConfig> listWx = JsonUtility.Deserialize<List<Sys_WxConfig>>(JsonUtility.Serialize(tmpObj.rows));
        //            if (listWx != null)
        //            {
        //                Sys_WxConfig cacheWxConfig = listWx[0];
        //                WxInfo wxInfo = JsonUtility.Deserialize<WxInfo>(JsonUtility.Serialize(cacheWxConfig));
        //                CookieUtils.SetCookie<WxInfo>(wxInfo, "WxConfig");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        wxConfig = CookieUtils.GetCookie<WxInfo>("WxConfig");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    LogUtility.WriteLog("", "ex", ex.Message);
        //}
        //finally
        //{

        //}
        //return wxConfig;
    }

    //public static WxInfo WxAdminRequest(int UnitId)
    //{
    //    WxInfo wxConfig = new WxInfo();
    //    Sys_WxConfigService wxService = new Sys_WxConfigService();
    //    try
    //    {
    //        string strUrl = HttpContext.Current.Request.Url.DnsSafeHost;
    //        if (CookieUtils.GetCookie<WxInfo>("Sys_WxConfig") == null)
    //        {
    //            string json = string.Empty;
    //            dynamic tmpObj = wxService.GetByUnitId(0, UnitId);
    //            Sys_WxConfig cacheWxConfig = JsonUtility.Deserialize<Sys_WxConfig>(JsonUtility.Serialize(tmpObj));
    //            if (cacheWxConfig != null && cacheWxConfig.Id > 0)
    //            {
    //                WxInfo wxInfo = JsonUtility.Deserialize<WxInfo>(JsonUtility.Serialize(cacheWxConfig));
    //                CookieUtils.SetCookie<WxInfo>(wxInfo, "Sys_WxConfig");
    //            }
    //        }
    //        else
    //        {
    //            wxConfig = CookieUtils.GetCookie<WxInfo>("Sys_WxConfig");
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {

    //    }
    //    return wxConfig;
    //}

    public static void RemoveCache(string cacheName)
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
}
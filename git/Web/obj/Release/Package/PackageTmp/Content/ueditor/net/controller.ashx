<%@ WebHandler Language="C#" Class="UEditorHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using Newtonsoft.Json;


public class UEditorHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (HttpContext.Current.Request.IsAuthenticated)
        {
            Handler action = null;
            switch (context.Request["action"])
            {
                case "config":
                    action = new ConfigHandler(context);
                    break;
                case "uploadimage":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = ueditorConfig.GetStringList("imageAllowFiles"),
                        PathFormat = ueditorConfig.GetString("imagePathFormat"),
                        SizeLimit = ueditorConfig.GetInt("imageMaxSize"),
                        UploadFieldName = ueditorConfig.GetString("imageFieldName")
                    });
                    break;
                case "uploadscrawl":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = ueditorConfig.GetString("scrawlPathFormat"),
                        SizeLimit = ueditorConfig.GetInt("scrawlMaxSize"),
                        UploadFieldName = ueditorConfig.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "uploadvideo":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = ueditorConfig.GetStringList("videoAllowFiles"),
                        PathFormat = ueditorConfig.GetString("videoPathFormat"),
                        SizeLimit = ueditorConfig.GetInt("videoMaxSize"),
                        UploadFieldName = ueditorConfig.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = ueditorConfig.GetStringList("fileAllowFiles"),
                        PathFormat = ueditorConfig.GetString("filePathFormat"),
                        SizeLimit = ueditorConfig.GetInt("fileMaxSize"),
                        UploadFieldName = ueditorConfig.GetString("fileFieldName")
                    });
                    break;
                case "listimage":
                    action = new ListFileManager(context, ueditorConfig.GetString("imageManagerListPath"), ueditorConfig.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    action = new ListFileManager(context, ueditorConfig.GetString("fileManagerListPath"), ueditorConfig.GetStringList("fileManagerAllowFiles"));
                    break;
                case "catchimage":
                    action = new CrawlerHandler(context);
                    break;
                case "delUrl":
                    action = new DeleteHandler(context);
                    break;
                default:
                    action = new NotSupportedHandler(context);
                    break;
            }
            action.Process();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
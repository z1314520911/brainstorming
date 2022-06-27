using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.Agent;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.Weixin.MP.CommonAPIs;
using System.Web.Configuration;
using Model;
using BLL;
using Commons;
using System.IO;
using System.Data;

namespace Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        private string GetWelcomeInfo()
        {
            T_WxReply reply = ModelBLL.QueryBySelectWhereAndOrderBy<T_WxReply>("ParentType=3 and Status=1", "Id desc");
            return reply.Content;
            //获取Senparc.Weixin.MP.dll版本信息
            //var fileVersionInfo = FileVersionInfo.GetVersionInfo(HttpContext.Current.Server.MapPath("~/bin/Senparc.Weixin.MP.dll"));
            //var version = string.Format("{0}.{1}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart);
            //+ "帮助指南 <a href='https://mp.weixin.qq.com/s?__biz=MzU0MzI5MjkyOA==&mid=100000027&idx=1&sn=de195dec5ce35a6bb9d60742d37f313a&chksm=7b0cea744c7b6362a78de9e5a74192d89ada83375ba78a9e17aeb2c12af184754019697cd650&mpshare=1&scene=1&srcid=0315q3jqE1j4ZCUhYSasqUJ7#rd'>点击这里</a>\r\n"
            //+ "新闻公告 <a href='https://mp.weixin.qq.com/mp/profile_ext?action=home&__biz=MzU0MzI5MjkyOA==&scene=123#wechat_redirect'>点击这里</a>\r\n"
            //return "你好，欢迎关注ityizu！\r\n"
            //                    + "爱投资，爱生活！\r\n"
            //                    + "链克供求 <a href='http://wkb.ityizu.com'>点击这里</a>\r\n"
            //                    + "更多帮助，请直接回复留言\r\n"
            //                    + "或咨询客服QQ：80018225\r\n"
            //                    + "免费QQ群：555507 \r\n"
            //                    + "VIP QQ群：555220";
        }

        public override IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            // 预处理文字或事件类型请求。
            // 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
            // 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
            // 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
            // 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
            // 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey

            //if (requestMessage.Content == "OneClick")
            //{
            //    var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
            //    strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
            //    return strongResponseMessage;
            //}
            return null;//返回null，则继续执行OnTextRequest或OnEventRequest
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            //菜单点击，需要跟创建菜单时的Key匹配
            switch (requestMessage.EventKey)
            {
                //case "OneClick":
                //    {
                //        //这个过程实际已经在OnTextOrEventRequest中完成，这里不会执行到。
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
                //    }
                //    break;
                //case "SubClickRoot_Text":
                //    {
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Content = "您点击了子菜单按钮。";
                //    }
                //    break;
                //case "SubClickRoot_News":
                //    {
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Articles.Add(new Article()
                //        {
                //            Title = "您点击了子菜单图文按钮",
                //            Description = "您点击了子菜单图文按钮，这是一条图文信息。",
                //            PicUrl = WebUrl + "/Images/qrcode.jpg",
                //            Url = WebUrl
                //        });
                //    }
                //    break;
                //case "SubClickRoot_Music":
                //    {
                //        //上传缩略图
                //        var accessToken = CommonAPIs.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                //        var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.thumb,
                //                                                     Server.GetMapPath("~/Images/Logo.jpg"));
                //        //设置音乐信息
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageMusic>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Music.Title = "天籁之音";
                //        strongResponseMessage.Music.Description = "真的是天籁之音";
                //        strongResponseMessage.Music.MusicUrl = WebUrl + "/Content/music1.mp3";
                //        strongResponseMessage.Music.HQMusicUrl = WebUrl + "/Content/music1.mp3";
                //        strongResponseMessage.Music.ThumbMediaId = uploadResult.thumb_media_id;
                //    }
                //    break;
                //case "SubClickRoot_Image":
                //    {
                //        //上传图片
                //        var accessToken = CommonAPIs.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                //        var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image,
                //                                                     Server.GetMapPath("~/Images/Logo.jpg"));
                //        //设置图片信息
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageImage>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Image.MediaId = uploadResult.media_id;
                //    }
                //    break;
                //case "OAuth"://OAuth授权测试
                //    {
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();
                //        strongResponseMessage.Articles.Add(new Article()
                //        {
                //            Title = "OAuth2.0测试",
                //            Description = "点击【查看全文】进入授权页面。\r\n注意：此页面仅供测试（是专门的一个临时测试账号的授权，并非Senparc.Weixin.MP SDK官方账号，所以如果授权后出现错误页面数正常情况），测试号随时可能过期。请将此DEMO部署到您自己的服务器上，并使用自己的appid和secret。",
                //            Url = WebUrl + "/oauth2",
                //            PicUrl = WebUrl + "/Images/qrcode.jpg"
                //        });
                //        reponseMessage = strongResponseMessage;
                //    }
                //    break;
                //case "Description":
                //    {
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                //        strongResponseMessage.Content = GetWelcomeInfo();
                //        reponseMessage = strongResponseMessage;
                //    }
                //    break;
                //case "SubClickRoot_PicPhotoOrAlbum":
                //    {
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Content = "您点击了【微信拍照】按钮。系统将会弹出拍照或者相册发图。";
                //    }
                //    break;
                //case "SubClickRoot_ScancodePush":
                //    {
                //        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                //        reponseMessage = strongResponseMessage;
                //        strongResponseMessage.Content = "您点击了【微信扫码】按钮。";
                //    }
                //    break;
                default:
                    {
                        if (requestMessage.EventKey.IndexOf("海报") > -1)
                        {
                            T_Poster poster = ModelBLL.QueryBySelectWhere<T_Poster>(Common.IsEqual("Keywords"), Dict.T_Poster_Keywords(requestMessage.EventKey));
                            return MessagePoster(poster);
                        }
                        //var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        //if (requestMessage.EventKey == "menu_joke")
                        //{
                        //    strongResponseMessage.Content = "开心时刻";
                        //}
                        //else
                        //{
                        //    if (requestMessage.EventKey == "12")
                        //    {
                        //        strongResponseMessage.Content = "您的当前余额：" + requestMessage.FromUserName + " 信币" + requestMessage.ToUserName;
                        //    }
                        //    else
                        //    {
                        //        strongResponseMessage.Content = "您点击了按钮，EventKey：" + requestMessage.EventKey;
                        //    }
                        //}
                        //reponseMessage = strongResponseMessage;
                    }
                    break;
            }

            return reponseMessage;
        }

        public IResponseMessageBase MessagePoster(T_Poster poster)
        {

            var strongResponseMessage = CreateResponseMessage<ResponseMessageImage>();

            try
            {
                if (poster.Id > 0)
                {
                    string appId = WebConfigurationManager.AppSettings["AppId"];
                    string appSecret = WebConfigurationManager.AppSettings["AppSecret"];
                    string _urlFormat = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
                    //var accessToken = CommonAPIs.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                    string text = poster.WaitText;

                    var data = new
                    {
                        touser = WeixinOpenId,
                        msgtype = "text",
                        text = new { content = text }
                    };
                    //Weixin.Entities.WxJsonResult result = CommonJsonSend.Send(accessToken, _urlFormat, data);
                    ApiHandlerWapper.TryCommonApi(accessToken =>
                    {
                        return CommonJsonSend.Send(accessToken, _urlFormat, data);

                    }, appId);


                    string path = System.Web.HttpContext.Current.Server.MapPath("~/" + poster.Pic.Trim('/'));
                    if (!string.IsNullOrEmpty(poster.Pic) && !File.Exists(path))
                    {
                        string dirPath = Path.GetDirectoryName(path);
                        if (!Directory.Exists(dirPath))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                            directoryInfo.Create();
                        }
                        System.Net.WebClient web = new System.Net.WebClient();
                        web.DownloadFile("http://i.wkebi.com/" + poster.Pic.Replace("/upload/", ""), path);
                    }
                    T_User user = ModelBLL.QueryBySelectWhere<T_User>(Common.IsEqual("OpenId"), Dict.T_User_OpenId(WeixinOpenId));
                    if (user.Id > 0)
                    {
                        string pic = "/upload/poster/" + Commons.StringUtility.RSAEncrypt(user.Id.ToString()) + ".jpg";
                        Poster.MakePoster(pic, poster, user);

                        //accessToken = CommonAPIs.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                        var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(appId, UploadMediaFileType.image, System.Web.HttpContext.Current.Server.MapPath(pic));
                        strongResponseMessage.Image.MediaId = uploadResult.media_id;
                    }
                    else
                    {
                        var strongResponseMessage2 = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage2.Content = poster.FailText;
                        return strongResponseMessage2;
                    }
                }
            }
            catch (Exception e)
            {
                Commons.WriteLog.CreateLog(e);
            }

            return strongResponseMessage;
        }

        public IResponseMessageBase WxReply(T_WxReply reply)
        {
            try
            {
                if (!string.IsNullOrEmpty(reply.Url) || !string.IsNullOrEmpty(reply.Pic))
                {
                    Article article = new Article();
                    article.Description = reply.Content;
                    article.Title = reply.Name;
                    if (!string.IsNullOrEmpty(reply.Pic))
                    {
                        article.PicUrl = "http://i.wkebi.com/" + reply.Pic.Replace("/upload/", "");
                    }
                    article.Url = reply.Url;
                    var openResponseMessage = CreateResponseMessage<ResponseMessageNews>();
                    openResponseMessage.Articles.Add(article);
                    return openResponseMessage;
                }
                else
                {
                    var responseMessage = CreateResponseMessage<ResponseMessageText>();
                    responseMessage.Content = reply.Content;
                    return responseMessage;
                }
            }
            catch (Exception e)
            {
                Commons.WriteLog.CreateLog(e);
            }
            return null;
        }

        //public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        //{
        //    var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
        //    responseMessage.Content = "您刚才发送了ENTER事件请求。";
        //    return responseMessage;
        //}

        //public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        //{
        //    //这里是微信客户端（通过微信服务器）自动发送过来的位置信息
        //    var responseMessage = CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = requestMessage.Latitude + "," + requestMessage.Longitude;
        //    return null;//这里也可以返回null（需要注意写日志时候null的问题）
        //}

        //public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        //{
        //    //通过扫描关注
        //    var responseMessage = CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "通过扫描关注。";
        //    return responseMessage;
        //}

        //public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        //{
        //    //说明：这条消息只作为接收，下面的responseMessage到达不了客户端，类似OnEvent_UnsubscribeRequest
        //    var responseMessage = CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "您点击了view按钮，将打开网页：" + requestMessage.EventKey;
        //    return responseMessage;
        //}

        //public override IResponseMessageBase OnEvent_MassSendJobFinishRequest(RequestMessageEvent_MassSendJobFinish requestMessage)
        //{
        //    var responseMessage = CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "接收到了群发完成的信息。";
        //    return responseMessage;
        //}

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = GetWelcomeInfo();
            return responseMessage;
        }

        ///// <summary>
        ///// 退订
        ///// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        ///// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        ///// </summary>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "有空再来";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 事件之扫码推事件(scancode_push)
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "事件之扫码推事件";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "事件之扫码推事件且弹出“消息接收中”提示框";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 事件之弹出拍照或者相册发图（pic_photo_or_album）
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "事件之弹出拍照或者相册发图";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 事件之弹出系统拍照发图(pic_sysphoto)
        ///// 实际测试时发现微信并没有推送RequestMessageEvent_Pic_Sysphoto消息，只能接收到用户在微信中发送的图片消息。
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "事件之弹出系统拍照发图";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 事件之弹出微信相册发图器(pic_weixin)
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "事件之弹出微信相册发图器";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 事件之弹出地理位置选择器（location_select）
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        //{
        //    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "事件之弹出地理位置选择器";
        //    return responseMessage;
        //}
    }
}
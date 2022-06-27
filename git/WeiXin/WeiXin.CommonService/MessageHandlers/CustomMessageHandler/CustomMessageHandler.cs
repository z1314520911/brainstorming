using System;
using System.IO;
using System.Web.Configuration;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Model;
using BLL;
using Commons;
using System.Data;

namespace Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        /*
         * 重要提示：v1.5起，MessageHandler提供了一个DefaultResponseMessage的抽象方法，
         * DefaultResponseMessage必须在子类中重写，用于返回没有处理过的消息类型（也可以用于默认消息，如帮助信息等）；
         * 其中所有原OnXX的抽象方法已经都改为虚方法，可以不必每个都重写。若不重写，默认返回DefaultResponseMessage方法中的结果。
         */


        private string appId = WebConfigurationManager.AppSettings["AppId"];
        private string appSecret = WebConfigurationManager.AppSettings["Secret"];
        private string WebUrl = WebConfigurationManager.AppSettings["WebUrl"];

        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();


            WriteLog.CreateLog("requestMessage.Content: " + requestMessage.Content); 

            if (requestMessage.Content == null)
            {

            }
            else
            {
                if (requestMessage.Content.IndexOf("海报") > -1)
                {
                    T_Poster poster = ModelBLL.QueryBySelectWhere<T_Poster>(Common.IsEqual("Keywords"), Dict.T_Poster_Keywords(requestMessage.Content));
                    return MessagePoster(poster);
                }
                else
                {
                    DataTable dt = ModelBLL.getPage(0, "*", "T_WxReply", "Status=1 and ParentType!=3", "Id desc");
                    DataRow[] row = dt.Select("Keywords='" + requestMessage.Content + "'");
                    if (row.Length > 0)
                    {
                        return WxReply(DataTableExtensions.ToModel<T_WxReply>(row[0]));
                    }
                    else
                    {
                        //try
                        //{
                        foreach (DataRow dr in dt.Rows)
                        {
                            string Keywords = Convert.ToString(dr["Keywords"]);
                            if (!string.IsNullOrEmpty(Keywords))
                            {
                                string[] txt = Keywords.Split(',');
                                foreach (string a in txt)
                                {
                                    if (!string.IsNullOrEmpty(a) && requestMessage.Content.IndexOf(a) > -1)
                                    {
                                        return WxReply(DataTableExtensions.ToModel<T_WxReply>(dr));
                                    }
                                }
                            }
                        }
                        if (CurrentMessageContext.RequestMessages.Count == 1)
                        {
                            DataRow[] row2 = dt.Select("ParentType=2");
                            if (row2.Length > 0)
                            {
                                return WxReply(DataTableExtensions.ToModel<T_WxReply>(row2[0]));
                            }
                        }
                        //}
                        //catch (Exception e)
                        //{
                        //    Commons.WriteLog.CreateLog(e);
                        //}
                    }
                }
                return null;
            }
            //else if (requestMessage.Content == "AsyncTest")
            //{
            //    //异步并发测试（提供给单元测试使用）
            //    DateTime begin = DateTime.Now;
            //    int t1, t2, t3;
            //    System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
            //    System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            //    DateTime end = DateTime.Now;
            //    var thread = System.Threading.Thread.CurrentThread;
            //    responseMessage.Content = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
            //            thread.ManagedThreadId,
            //            HttpContext.Current != null ? HttpContext.Current.ApplicationInstance.GetHashCode() : -1,
            //            begin,
            //            end,
            //            t2 - t1
            //            );
            //}
            //else if(requestMessage.Content=="open")
            //{
            //    var openResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageNews>();
            //    openResponseMessage.Articles.Add(new Article()
            //    {
            //        Title = "开放平台微信授权测试",
            //        Description = @"点击进入Open授权页面。",
            //        Url = WebUrl + "/OAuth2"
            //    });
            //    return openResponseMessage;
            //}
            //else
            //{
            //    var result = new StringBuilder();
            //    result.AppendFormat("您刚才发送了文字信息：{0}", requestMessage.Content);

            //    if (CurrentMessageContext.RequestMessages.Count > 1)
            //    {
            //        result.AppendFormat("\r\n您刚才还发送了如下消息（{0}/{1}）：\r\n", CurrentMessageContext.RequestMessages.Count,
            //            CurrentMessageContext.StorageData);
            //        for (int i = CurrentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
            //        {
            //            var historyMessage = CurrentMessageContext.RequestMessages[i];
            //            result.AppendFormat("{0} 【{1}】{2}",
            //                historyMessage.CreateTime.ToShortTimeString(),
            //                historyMessage.MsgType.ToString(),
            //                (historyMessage is RequestMessageText)
            //                    ? (historyMessage as RequestMessageText).Content
            //                    : "[非文字类型]"
            //                );
            //        }
            //    }

            //    responseMessage.Content = result.ToString();
            //}
            return responseMessage;
        }

        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var locationService = new LocationService();
            var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            return responseMessage;
        }

        //public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        //{
        //    var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "您刚才发送的是小视频";
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 处理图片请求
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        //{
        //    var responseMessage = CreateResponseMessage<ResponseMessageNews>();
        //    responseMessage.Articles.Add(new Article()
        //    {
        //        Title = "您刚才发送了图片信息",
        //        Description = "您发送的图片将会显示在边上",
        //        PicUrl = requestMessage.PicUrl,
        //        Url = WebUrl
        //    });
        //    responseMessage.Articles.Add(new Article()
        //    {
        //        Title = "第二条",
        //        Description = "第二条带连接的内容",
        //        PicUrl = requestMessage.PicUrl,
        //        Url = WebUrl
        //    });

        //    return responseMessage;
        //}

        ///// <summary>
        ///// 处理语音请求
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        //{
        //    var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
        //    //上传缩略图
        //    var accessToken = CommonAPIs.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
        //    var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image,
        //                                                 Server.GetMapPath("~/Images/Logo.jpg"));

        //    //设置音乐信息
        //    responseMessage.Music.Title = "天籁之音";
        //    responseMessage.Music.Description = "播放您上传的语音";
        //    responseMessage.Music.MusicUrl = WebUrl+"/Media/GetVoice?mediaId=" + requestMessage.MediaId;
        //    responseMessage.Music.HQMusicUrl = WebUrl + "/Media/GetVoice?mediaId=" + requestMessage.MediaId;
        //    responseMessage.Music.ThumbMediaId = uploadResult.media_id;
        //    return responseMessage;
        //}
        ///// <summary>
        ///// 处理视频请求
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        //{
        //    var responseMessage = CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "您发送了一条视频信息，ID：" + requestMessage.MediaId;
        //    return responseMessage;
        //}

        ///// <summary>
        ///// 处理链接消息请求
        ///// </summary>
        ///// <param name="requestMessage"></param>
        ///// <returns></returns>
        //public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        //{
        //    var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
        //    responseMessage.Content = string.Format(@"您发送了一条连接信息：
        //    Title：{0}
        //    Description:{1}
        //    Url:{2}", 
        //    requestMessage.Title, requestMessage.Description, requestMessage.Url);
        //    return responseMessage;
        //}

        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = base.OnEventRequest(requestMessage);
            //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {


            return null;
        }
    }
}

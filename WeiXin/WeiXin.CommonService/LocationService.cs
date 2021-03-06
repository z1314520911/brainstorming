/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：LocationService.cs
    文件功能描述：地理位置信息处理
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.BaiduMap;
using Senparc.Weixin.MP.Entities.GoogleMap;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Sample.CommonService
{
    public class LocationService
    {
        public ResponseMessageNews GetResponseMessage(RequestMessageLocation requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);

            var markersList = new List<BaiduMarkers>();
            markersList.Add(new BaiduMarkers()
            {
                Size = BaiduMarkerSize.m,
                Color = "red",
                Label = "A",
                Latitude = requestMessage.Location_X,
                Longitude = requestMessage.Location_Y,
            });
            var mapUrl = BaiduMapHelper.GetBaiduStaticMap(requestMessage.Location_Y, requestMessage.Location_X, 1, 13, markersList);
            responseMessage.Articles.Add(new Article()
            {
                Description = string.Format("您刚才发送了地理位置信息。Location_X:{0},Location_Y:{1},Scale:{2},标签：{3}", requestMessage.Location_X, requestMessage.Location_Y, requestMessage.Scale, requestMessage.Label),
                PicUrl = mapUrl,
                Title = "定位地点周边地图",
                Url = mapUrl
            });
            

            //var markersList = new List<GoogleMapMarkers>();
            //markersList.Add(new GoogleMapMarkers()
            //{
            //    X = requestMessage.Location_X,
            //    Y = requestMessage.Location_Y,
            //    Color = "red",
            //    Label = "S",
            //    Size = GoogleMapMarkerSize.Default,
            //});
            //var mapSize = "480x600";
            //var mapUrl = GoogleMapHelper.GetGoogleStaticMap(19 /*requestMessage.Scale*//*微信和GoogleMap的Scale不一致，这里建议使用固定值*/,
            //                                                markersList, mapSize);
            //responseMessage.Articles.Add(new Article() 
            //{
            //    Description = string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
            //                  requestMessage.Location_X, requestMessage.Location_Y,
            //                  requestMessage.Scale, requestMessage.Label),
            //    PicUrl = mapUrl,
            //    Title = "定位地点周边地图",
            //    Url = mapUrl
            //});

            return responseMessage;
        }
    }
}
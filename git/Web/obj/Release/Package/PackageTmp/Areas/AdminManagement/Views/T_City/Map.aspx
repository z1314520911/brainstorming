<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
	<style type="text/css">
		body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
    <script type="text/javascript" src="/Content/ueditor/third-party/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=mKhq6orhzfw37BIz1IsijEgH"></script>
	<title>单击获取点击的经纬度</title>
</head>
<body style="background:#CBE1FF">
    <div style="width:730px;">   
        地址：<input id="txtSearch" type="text" value="海口市 " />
        <input type="button" value="查询" onclick="searchByStationName();"/>
        查询结果：<input id="result_point" type="text" readonly="readonly" />
        <input type="button" value="此处点击直接保存左侧坐标" onclick="saveResultPoint();"/>
        <div id="container" 
            style="position: absolute;
                margin-top:30px; 
                width: 98%; 
                height: 500px; 
                top: 0px; 
                left: 6px;
                right:6px;
                border: 1px solid gray;
                overflow:hidden;">
        </div>
    </div>
<script type="text/javascript">
    var map = new BMap.Map("container");
    map.centerAndZoom("海口市 龙华路", 15);
    map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用

    //map.addControl(new BMap.NavigationControl());  //添加默认缩放平移控件
    //map.addControl(new BMap.OverviewMapControl()); //添加默认缩略地图控件
    //map.addControl(new BMap.OverviewMapControl({ isOpen: true, anchor: BMAP_ANCHOR_BOTTOM_RIGHT }));   //右下角，打开

    var localSearch = new BMap.LocalSearch(map);
    localSearch.enableAutoViewport(); //允许自动调节窗体大小
    function searchByStationName() {
        map.clearOverlays();//清空原来的标注
        var keyword = document.getElementById("txtSearch").value;
        localSearch.setSearchCompleteCallback(function (searchResult) {
            var poi = searchResult.getPoi(0);
            document.getElementById("result_point").value = poi.point.lng + "," + poi.point.lat;
            map.centerAndZoom(poi.point, 15);
            var marker = new BMap.Marker(new BMap.Point(poi.point.lng, poi.point.lat));  // 创建标注，为要查询的地方对应的经纬度
            map.addOverlay(marker);
            var content = document.getElementById("txtSearch").value + "<br/><br/>经度：" + poi.point.lng + "<br/>纬度：" + poi.point.lat;
            var infoWindow = new BMap.InfoWindow("<p style='font-size:14px;'>" + content + "</p>");
            marker.addEventListener("click", function () { this.openInfoWindow(infoWindow); });
            // marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
        });
        localSearch.search(keyword);
    }


    // 百度地图API功能
    function G(id) {
        return document.getElementById(id);
    }
    var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
		{
		    "input": "txtSearch"
		, "location": map
		});

    ac.addEventListener("onhighlight", function (e) {  //鼠标放在下拉列表上的事件
        var str = "";
        var _value = e.fromitem.value;
        var value = "";
        if (e.fromitem.index > -1) {
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

        value = "";
        if (e.toitem.index > -1) {
            _value = e.toitem.value;
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
        G("searchResultPanel").innerHTML = str;
    });

    var myValue;
    ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
        var _value = e.item.value;
        myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        G("searchResultPanel").innerHTML = "onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;

        setPlace();
    });

    //单击获取点击的经纬度
    map.addEventListener("click", function (e) {
        savePoint(e.point.lat, e.point.lng);
    });

    function saveResultPoint() {
        var point = document.getElementById("result_point").value;
        if (point.length == 0) {
            alert("请确认左侧有坐标地址");
            return false;
        }
        savePoint(point.split(',')[1], point.split(',')[0]);
    }

    function savePoint(lat, lng) {        
        $("body", parent.document).find('#Latitude').val(lat);
        $("body", parent.document).find('#LatitudeButton').html(lat);
        
        $("body", parent.document).find('#Longitude').val(lng);
        $("body", parent.document).find('#LongitudeButton').html(lng);

        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    }
    
</script>
</body>
</html>
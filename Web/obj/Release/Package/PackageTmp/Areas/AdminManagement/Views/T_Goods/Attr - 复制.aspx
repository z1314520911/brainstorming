<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>jQuery仿淘宝SKU选择商品属性代码</title>
    <script type="text/javascript" src="/content/sku/js/jquery.js"></script>
    <script type="text/javascript" src="/content/sku/js/liandong.js"></script>
    <link type="text/css" rel="stylesheet" href="/content/sku/css/liandong.css" />
    <script type="text/javascript">
        
        function HtmlTableToJson(tableid) {
            // table need set thead and tbody
            // talbe head need set name attr
            // value control need set josnval attr or will get null string
            var jsondata = [];
            // table head
            var heads = [];
            $("#" + tableid + ' thead th').each(function (index, item) {
                heads.push($(item).attr('name'));
            });
            // tbody
            $("#" + tableid + ' tbody tr').each(function (index, item) {
                var rowdata = {};
                $(item).find('td').each(function (index, item) {
                    if ($(item).find('[josnval]').size() > 0) {
                        console.log("have json val");
                        rowdata[heads[index]] = $(item).find('[josnval]').val();
                    } else {
                        console.log("no jsonval");
                        rowdata[heads[index]] = "";
                    }
                });
                jsondata.push(rowdata);
            });
            return jsondata;
        }
        function tj() {
            alert(JSON.stringify(HtmlTableToJson("process")));
        }
    </script>
</head>
<body class="">
<div class="kePublic">
<!--效果html开始-->
<div id="navtab1" style="width: 960px; margin:0 auto; padding:20px; border: 1px solid #A3C0E8;">
    <div title="扩展信息" tabid="tabItem3">
        <div id="Div1">
            <div position="center">
                <div style="padding: 5px 8px;" class="div_content">
                    <div class="div_title"><span>产品规格</span></div>
                    <div class="div_contentlist">
                <%
                    int index = 0;
                    DataTable dt = ViewBag.dt as DataTable;
                    foreach(DataRow row in dt.Select("num=1"))
                    {
                %>
                        <ul class="Father_Title"><li><%= row["Name"] %></li></ul>
                        <ul class="Father_Item<%= index %>">
                    <%
                        foreach(DataRow row2 in dt.Select("num=2 and PId=" + row["Id"]))
                        {
                    %>
                            <li class="li_width"><label><input id="Checkbox<%= row2["Id"] %>" type="checkbox" class="chcBox_Width" value="<%= row2["Name"] %>" /><%= row2["Name"] %><span class="li_empty"> </span></label></li>
                    <%
                        }
                    %>
                        </ul>
                        <br/>
                <%
                        index++;
                    }
                %>
                    </div>
                    <div class="div_contentlist2">
                        <ul>
                            <li><div id="createTable"></div></li>
                        </ul>
                        <ul><li><input type="button" id="Button1" class="l-button" value="提交" onclick="tj();" /></li></ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!--效果html结束-->
<div class="clear"></div>
</div>

</body>
</html>
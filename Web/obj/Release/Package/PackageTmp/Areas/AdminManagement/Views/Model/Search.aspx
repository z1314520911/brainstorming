<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %><!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title><%= Commons.Config.GetKeyValue("WebTitle") %></title>
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <link rel="stylesheet" href="/content/layui/css/layui.css" media="all">
    <link rel="stylesheet" href="/content/layui/build/css/app.css" media="all">
    <script src="/content/layui/layui.js"></script>
    <script type="text/javascript" src="/Content/ueditor/third-party/jquery-1.10.2.min.js"></script>
</head>

<body>
    <div class="site-text site-block">
        <% using (Html.BeginForm(ViewContext.RouteData.Values["Action"].ToString(), ViewContext.RouteData.Values["controller"].ToString(), Commons.Common.routeValues(ViewData["page"], ViewData["KeyWord"], ViewData["list"], ViewData["Table"], ViewData["Languages"], ViewData["DivId"], null), FormMethod.Post, new { enctype = "multipart/form-data", @class = "layui-form" }))
           {%>
        <%if (ViewData["showSearch"]!="1")
          { %>
        <div class="layui-elem-quote layui-quote-nm layui-elem-quote-admin" style="border:0;margin:0;padding:15px 15px 5px">
            请输入关键字：<input type="text" class="layui-input" id="keyword" name="keyword" value="<%=ViewData["KeyWord"] %>" />
            <input type="submit" class="layui-btn" value="查询" onclick='this.form.action="<%=Url.Action("Search",Commons.Common.routeValues(ViewData["page"], ViewData["KeyWord"], ViewData["list"], ViewData["Table"], ViewData["Languages"], ViewData["DivId"], "Search")) %>";' />
        </div>
        <%} %>
    <table class="layui-table layui-table-admin" lay-even lay-size="sm"><thead>
        <tr><%=ViewData["TableTh"] %></tr></thead>
            <mvc:mvcrepeater id="MvcRepeater2" key="TableTd" runat="server">
                <ItemTemplate>
                    <tr><%#((string)Container.DataItem)%></tr>
                </ItemTemplate>
            </mvc:mvcrepeater> </table>
    <%=Html.Pager("keyword", Convert.ToString(ViewData["KeyWord"]), true, 9, Model)%>
        <%} %>
        <%=ViewData["msg"] %>
    </div>
<script type="text/javascript">
    function Choose(value,text) {
        $("body", parent.document).find('#<%=ViewData["DivId"] %>').val(value);
        <%if(!string.IsNullOrEmpty(Convert.ToString(ViewData["BindJquery"]))){%>
        window.parent.window.<%=ViewData["BindJquery"]%>('<%=ViewData["DivId"] %>',value,text); 
        <%}else{%>
        $("body", parent.document).find('#<%=ViewData["DivId"] %>Button').html(text);
        <%}%>

        var Logistics = $("body", parent.document).find('#Logistics');
        if (Logistics) {
            Logistics.val(text);
        }

        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    }
</script> 
</body>
</html>
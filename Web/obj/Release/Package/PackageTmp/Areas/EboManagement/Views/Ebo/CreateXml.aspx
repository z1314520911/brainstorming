<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Commons.Config.GetKeyValue("WebTitle") %> 配置</title>
    <style>body{font-family:新宋体;font-size:12px;margin:0px auto;}a{color:#36163a;text-decoration:none}a:hover{color: #bd0a01;text-decoration:underline}a.lcblue{color:#0f479c}a.lchot{color: #bd0a01}input{vertical-align:middle;}</style>
</head>
<body>
    <style>
        input[type="text"],textarea
        {
            width: 400px;
            border: 1px solid #CCC;
            margin-bottom: 5px;
        }
        input.w1{width: 100px;}
        input.w2{width: 60px;}
        textarea{width: 960px; height:60px;}
        .a1{ color:Blue;}
    </style>
    <div style=" width:960px; margin:30px auto 30px auto;">
    <%=ViewData["TopNav"]%>
        <% 
        using (Html.BeginForm())
        {%>
        <font class="a1">模型：</font><input name="table" type="text" value="<%=ViewData["table"]%>"/><%=ViewData["myText"]%><br />
        <font class="a1">导航：</font><input name="NavText" type="text" value="<%=ViewData["NavText"]%>" /><%=ViewData["myNavText"]%><br />
        <font class="a1">容器：</font><input name="NavBox" type="text" value="<%=ViewData["myNavBox"]%>" /><br />
        <font class="a1">图片：</font><input name="PicPath" type="text" value="<%=ViewData["myPicPath"]%>" /><br /><br />
        <font class="a1">字段：</font><textarea name="Field" type="text"><%=ViewData["Descrip"]%></textarea><%=ViewData["myDescrip"]%><br />
        <input type="submit" value="保存" name="type" /><br />
        <%} %><%=ViewData["ReturnAlert"]%>
    </div>
</body>
</html>
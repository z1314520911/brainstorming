<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Model.AdminUser>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312">
    <meta name="viewport" content="initial-scale=1.0, minimum-scale=1.0, maximum-scale=2.0, user-scalable=no, width=device-width">
    <title>管理员登录</title>
    <link href="/content/admin/login/login.css" rel="stylesheet" type="text/css">
    <script type="text/javaScript">
        function FormSubmit() {
            var fm = window.document.frm_login;
            if (fm.username.value == "" || fm.username.value == "请输入用户名") {
                window.alert("请输入您的用户名!");
                fm.username.focus();
                return false;
            }
            if (fm.password.value.length == "") {
                window.alert("请输入您的密码!");
                fm.password.focus();
                return false;
            }
            if (fm.code.value == "" || fm.code.value == "请输入验证码") {
                window.alert("请输入验证码!");
                fm.code.focus();
                return false;
            }
            fm.submit();
            return true;
        }
</script>
</head>
<body>
    <div class="header">
        <div class="toplinks">
            <a href="/">返回首页</a> | <a onclick="window.external.AddFavorite(location.href, document.title);" style="cursor: pointer" title="加入收藏">加入收藏</a>
        </div>
    </div>
    <div class="login-main">
        <div class="login-logo">企业管理后台</div>
        <div class="login-form">
            <% using (Html.BeginForm("login", "main", FormMethod.Post, new { id = "frm_login", name = "frm_login", onsubmit = "javascript:return FormSubmit();" }))
                       { %>
            <div class="login-txtbox" id="login-txtbox">
                <i class="ico-usename"></i>
                <input class="txt-input text-email" type="text" id="username" name="username" placeholder="请输入用户名" value="<%=string.IsNullOrEmpty(Model.UserName) ? "请输入用户名" : Model.UserName%>" tabindex="1"
                    onmousedown="if(this.value=='请输入用户名'){this.value='';}"
                    onblur="if(this.value==''){this.value='请输入用户名';}" onmouseover="document.getElementById('login-txtbox').style.border='solid 1px #38B8E8'"
                    onmouseout="document.getElementById('login-txtbox').style.border='solid 1px #bbb'" />
            </div>
            <div class="login-txtbox" id="login-txtbox2">
                <i class="ico-pwd"></i>
                <input class="txt-input" type="password" id="password" name="password" placeholder="请输入密码" value="" tabindex="2" onmouseover="document.getElementById('login-txtbox2').style.border='solid 1px #38B8E8'"
                    onmouseout="document.getElementById('login-txtbox2').style.border='solid 1px #bbb'" />
            </div>
            <div class="login-code" id="imageVerifyArea">
                <div id="code-txtboxId" class="code-txtbox">
                    <i class="ico-code"></i>
                    <input class="txt-input" type="text" id="code" placeholder="请输入验证码" value="请输入验证码" autocomplete="off" maxlength="5" name="code" tabindex="3"
                    onmousedown="if(this.value=='请输入验证码'){this.value='';}" onblur="if(this.value==''){this.value='请输入验证码';}" />
                </div>
                <div class="code-num">
                    <a href="javascript:void(0);" onclick="document.getElementById('CodeImg').src='<%=Url.Action("yz", "Main")%>?tmp='+Math.random()">
                            <img id="CodeImg" onclick="this.src='<%=Url.Action("yz", "Main")%>?tmp='+Math.random()'" src="<%=Url.Action("yz", "Main",new {tmp = Guid.NewGuid()})%>" align="absmiddle" alt="看不清楚?换一张!" /></a>
                </div>
                <div class="code-link">
                    看不清 <a href="javascript:void(0);" onclick="document.getElementById('CodeImg').src='<%=Url.Action("yz", "Main")%>?tmp='+Math.random()">换一张</a>
                </div>
            </div>
            <div class="btn-login">
                <input id="btnLogin" type="submit" value="登 陆" name="btnLogin" class="login-submit" />
            </div>
            <div class="links-text">
                <a href="#" onclick="window.opener=null;window.close();" id="user_login_a">关闭后台登陆窗口</a>
            </div>
            <div class="user-frd">
                <span>Copyright &copy; <%=DateTime.Now.Year %>  All rights reserved.</span>
            </div>
            <%} %>
        </div>
    </div><%=ViewData["ReturnInfo"]%>
</body>
</html>
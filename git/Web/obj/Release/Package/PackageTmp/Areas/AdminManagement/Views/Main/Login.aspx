<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Model.AdminUser>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312">
    <meta name="viewport" content="initial-scale=1.0, minimum-scale=1.0, maximum-scale=2.0, user-scalable=no, width=device-width">
    <title>����Ա��¼</title>
    <link href="/content/admin/login/login.css" rel="stylesheet" type="text/css">
    <script type="text/javaScript">
        function FormSubmit() {
            var fm = window.document.frm_login;
            if (fm.username.value == "" || fm.username.value == "�������û���") {
                window.alert("�����������û���!");
                fm.username.focus();
                return false;
            }
            if (fm.password.value.length == "") {
                window.alert("��������������!");
                fm.password.focus();
                return false;
            }
            if (fm.code.value == "" || fm.code.value == "��������֤��") {
                window.alert("��������֤��!");
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
            <a href="/">������ҳ</a> | <a onclick="window.external.AddFavorite(location.href, document.title);" style="cursor: pointer" title="�����ղ�">�����ղ�</a>
        </div>
    </div>
    <div class="login-main">
        <div class="login-logo">��ҵ�����̨</div>
        <div class="login-form">
            <% using (Html.BeginForm("login", "main", FormMethod.Post, new { id = "frm_login", name = "frm_login", onsubmit = "javascript:return FormSubmit();" }))
                       { %>
            <div class="login-txtbox" id="login-txtbox">
                <i class="ico-usename"></i>
                <input class="txt-input text-email" type="text" id="username" name="username" placeholder="�������û���" value="<%=string.IsNullOrEmpty(Model.UserName) ? "�������û���" : Model.UserName%>" tabindex="1"
                    onmousedown="if(this.value=='�������û���'){this.value='';}"
                    onblur="if(this.value==''){this.value='�������û���';}" onmouseover="document.getElementById('login-txtbox').style.border='solid 1px #38B8E8'"
                    onmouseout="document.getElementById('login-txtbox').style.border='solid 1px #bbb'" />
            </div>
            <div class="login-txtbox" id="login-txtbox2">
                <i class="ico-pwd"></i>
                <input class="txt-input" type="password" id="password" name="password" placeholder="����������" value="" tabindex="2" onmouseover="document.getElementById('login-txtbox2').style.border='solid 1px #38B8E8'"
                    onmouseout="document.getElementById('login-txtbox2').style.border='solid 1px #bbb'" />
            </div>
            <div class="login-code" id="imageVerifyArea">
                <div id="code-txtboxId" class="code-txtbox">
                    <i class="ico-code"></i>
                    <input class="txt-input" type="text" id="code" placeholder="��������֤��" value="��������֤��" autocomplete="off" maxlength="5" name="code" tabindex="3"
                    onmousedown="if(this.value=='��������֤��'){this.value='';}" onblur="if(this.value==''){this.value='��������֤��';}" />
                </div>
                <div class="code-num">
                    <a href="javascript:void(0);" onclick="document.getElementById('CodeImg').src='<%=Url.Action("yz", "Main")%>?tmp='+Math.random()">
                            <img id="CodeImg" onclick="this.src='<%=Url.Action("yz", "Main")%>?tmp='+Math.random()'" src="<%=Url.Action("yz", "Main",new {tmp = Guid.NewGuid()})%>" align="absmiddle" alt="�������?��һ��!" /></a>
                </div>
                <div class="code-link">
                    ������ <a href="javascript:void(0);" onclick="document.getElementById('CodeImg').src='<%=Url.Action("yz", "Main")%>?tmp='+Math.random()">��һ��</a>
                </div>
            </div>
            <div class="btn-login">
                <input id="btnLogin" type="submit" value="�� ½" name="btnLogin" class="login-submit" />
            </div>
            <div class="links-text">
                <a href="#" onclick="window.opener=null;window.close();" id="user_login_a">�رպ�̨��½����</a>
            </div>
            <div class="user-frd">
                <span>Copyright &copy; <%=DateTime.Now.Year %>  All rights reserved.</span>
            </div>
            <%} %>
        </div>
    </div><%=ViewData["ReturnInfo"]%>
</body>
</html>
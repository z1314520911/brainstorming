<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master"
    Inherits="System.Web.Mvc.ViewPage<Commons.XmlEmail>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="layui-table" lay-even>
        <colgroup><col width="20%"><col width="30%"><col width="20%"><col width="30%"></colgroup>
        <tr>
            <td>
                <b>用户名：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.UserName, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>密码：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.PassWord, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>Smtp服务器地址：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Host, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>端口：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Port, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>发送者：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.FromMail, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>接收者：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.ToMail, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <button class="layui-btn" lay-submit="">修改信息</button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterContent" runat="server">
    <%=ViewData["ReturnAlert"]%>
</asp:Content>

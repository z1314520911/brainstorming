<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master"
    Inherits="System.Web.Mvc.ViewPage<Commons.XmlConfig>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="layui-table" lay-even>
        <colgroup><col width="20%"><col width="30%"><col width="20%"><col width="30%"></colgroup>
        <tr>
            <td>
                <b>网站名称：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Title, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>网站网址：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Web, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>网站关键字：</b>
            </td>
            <td colspan="3">
                <%: Html.TextAreaFor(model => model.KeyWord, new { @class = "layui-textarea" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>网站描述：</b>
            </td>
            <td colspan="3">
                <%: Html.TextAreaFor(model => model.Descrip, new { @class = "layui-textarea" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>公司名称：</b>
            </td>
            <td colspan="3">
                <%: Html.TextBoxFor(model => model.Company, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>电话：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Phone, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>手机：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Tel, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>公司传真：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Fax, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>公司邮编：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Zip, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>公司简介：</b>
            </td>
            <td colspan="3">
                <%: Html.TextAreaFor(model => model.About, new { @class = "layui-textarea" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>公司地址①：</b>
            </td>
            <td >
                <%: Html.TextBoxFor(model => model.Address, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>公司地址②：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Address1, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>QQ①：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.QQ, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>QQ②：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.QQ1, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>E-Mail①：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Email, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>E-Mail②：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Email1, new { @class = "layui-input" })%>
            </td>
        </tr>
        <tr>
            <td>
                <b>Msn：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Msn, new { @class = "layui-input" })%>
            </td>
            <td>
                <b>IPC备案：</b>
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.Icp, new { @class = "layui-input" })%>
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
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="layui-table" lay-even>
        <colgroup><col width="20%"><col width="30%"><col width="20%"><col width="30%"></colgroup>
        <tr>
            <td>
                <b>通知人：</b>
            </td>
            <td><a href="##" class="layui-btn iframe" id="UserIdButton">请选择</a><input id="UserId" name="UserId" type="hidden" value="" /><script>var buttonId = ['UserId'];var urlId = ['/Admin/Model/Search.aspx?table=T_User'];</script><script type="text/javascript" src="/Content/admin/js/iframe.js"></script>
            </td>
            <td>
                <b>单独发送：</b>
            </td>
            <td>
                <input id="alone" type="checkbox" name="alone" value="1" checked="checked" title="单独发送通知人">
            </td>
        </tr>
        <tr>
            <td>
                <b>标题：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="first" name="first" type="text" value="系统最新升级，欢迎来瓜分，{Coin}链克奖池！链克兑换、提现、提币费率全免！" />
            </td>
        </tr>
        <tr>
            <td>
                <b>升级用户：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="keyword1" name="keyword1" type="text" value="{User}" />
            </td>
        </tr>
        <tr>
            <td>
                <b>升级计划：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="keyword2" name="keyword2" type="text" value="" />
            </td>
        </tr>
        <tr>
            <td>
                <b>升级日期：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="keyword3" name="keyword3" type="text" value="<%=DateTime.Now.ToString("yyyy-MM-dd HH:mm") %>" />
            </td>
        </tr><%--        
        <tr>
            <td>
                <b>通知内容：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="keyword4" name="keyword4" type="text" value="" />
            </td>
        </tr>--%>
        <tr>
            <td>
                <b>备注：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="remark" name="remark" type="text" value="查看详情" />
            </td>
        </tr>
        <tr>
            <td>
                <b>链接：</b>
            </td>
            <td colspan="3">
                <input class="layui-input" id="url" name="url" type="text" value="http://shop.wkebi.com/member/mine" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <button class="layui-btn" lay-submit="">发送消息</button>
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                <b>可用变量：</b>
            </td>
            <td>{User}</td>
            <td colspan="2">用户名</td>
        </tr>
        <tr>
            <td>{Coin}</td>
            <td colspan="2">链克数</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterContent" runat="server">
    <%=ViewData["ReturnAlert"]%>
    <script>
        layui.use(['form'], function () {
            var form = layui.form,
                layer = layui.layer;
        });
    </script>
</asp:Content>

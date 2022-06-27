<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master"
    Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="layui-table layui-table-admin" lay-even lay-size="sm"><thead>
        <tr>
            <%if (Convert.ToString(ViewData["LeftFunShow"]) == "0" || Convert.ToString(ViewData["LeftFunShow"]) == "2")
              { %>
            <th class="caozuo">
                <input type="checkbox" lay-filter="CheckAll" lay-skin="primary" /><span class="checkall">全选</span><input type="submit" class="layui-btn layui-btn-small" value="删除" onclick = 'this.form.action="<%=Url.Action("Index", Commons.Common.routeValues(ViewData["page"], ViewData["KeyWord"], ViewData["list"], ViewData["Table"], ViewData["Languages"], null, "DeleteAll",ViewData["OtherKey"])) %>";return confirm("确定要删除全部数据吗？");' />
            </th><%}else{ %><th></th><%} %>
            <%if (Convert.ToString(ViewData["SortShow"]) == "1")
              { %>
            <th class="paixu">
                <input type="submit" value="排序" class="layui-btn layui-btn-small" onclick='this.form.action="<%=Url.Action("Index", Commons.Common.routeValues(ViewData["page"], ViewData["KeyWord"], ViewData["list"], ViewData["Table"], ViewData["Languages"], null, "Sort",ViewData["OtherKey"])) %>";' />
            </th><%} %>
            <%=ViewData["TableTh"]%>
        </tr></thead>
        <%=ViewData["TableTd"]%></table>
    <%=Html.Pager("keyword", Convert.ToString(ViewData["KeyWord"])+ViewData["OtherKey"]==""?"":"&other="+ViewData["OtherKey"], true, 9, Model)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FooterContent" runat="server">
    <script type="text/javascript" src="/content/admin/js/JScript.js?v=<%= Commons.Config.GetKeyValue("version") %>"></script>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">        var area2 = ['92%', '92%'];
        var url2 = ['/Admin/Model/Index.aspx?page=1&table=T_TgShip&Languages=Cn&list='];
    </script>
    <script src="/content/admin/js/JScript.js"></script>
    
    <div class="site-text site-block">
        <form action="/Admin/Bill/Index.aspx?page=1" class="layui-form" enctype="multipart/form-data" method="post">
            <blockquote class="layui-elem-quote layui-quote-nm layui-elem-quote-admin">
                请输入关键字：<input type="text" class="layui-input" id="keyword" name="keyword" value="" />
                <input type="submit" class="layui-btn" value="查询" onclick='this.form.action = "TgList.aspx?type=Search&page=1";' />
            </blockquote>

            <table class="layui-table layui-table-admin" lay-even lay-size="sm">
                <thead>
                    <tr>
                        <th></th>
                        <th>发货</th>
                        <th>名称</th>
                        <th>价格</th>
                        <th>成团数量</th>
                        <th>已售数量</th>
                        <th>开团时间</th>
                        <th>截止时间</th>
                    </tr>
                </thead>
        <%
            DataTable dt = ViewBag.Dt as DataTable;
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
        %>
                <tr>
                    <td><a href="TgOrderList.aspx?id=<%= row["Id"] %>&page=1&title=<%= row["Name"] %>">查看订单</a></td>
                    <td>
                        <% if (row["ShipStatus"].ToString().Trim().Equals("未成团")){ %>
                        <a style="color:coral;"><%= row["ShipStatus"] %></a>
                        <% } else { %>
                        <a href='##' style="color:cornflowerblue;" onclick='openIframe2(<%= row["Id"] %>,0)'><%= row["ShipStatus"] %></a>
                        <% } %>
                    </td>
                    <td><%= row["Name"] %></td>
                    <td><%= row["Price"] %></td>
                    <td><%= row["TGNum"] %></td>
                    <td><%= row["Number"] %></td>
                    <td><%= row["AddTime"] %></td>
                    <td><%= row["TGEndTime"] %></td>
                </tr>
        <%
                }
            }
        %>
            </table>

            <%--分页函数--%>
            <%=Html.Pager("keyword", Convert.ToString(ViewData["KeyWord"]), true, 9, Model)%>

        </form>
    </div>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

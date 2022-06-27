<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="site-text site-block">
        <form action="/Admin/Bill/Index.aspx?page=1" class="layui-form" enctype="multipart/form-data" method="post">
            <blockquote class="layui-elem-quote layui-quote-nm layui-elem-quote-admin">
                请输入关键字：<input type="text" class="layui-input" id="keyword" name="keyword" value="" />
                <script type="text/javascript">
                    var area2 = ['80%', '80%'];
                    var url2 = [
                        '/Admin/Bill/Index.aspx?page=1&keyword=1_admin&list='
                    ];
                </script>
                <input type="submit" class="layui-btn" value="查询" onclick='this.form.action="TgOrderList.aspx?page=1&id=<%= ViewBag.id %>&title=<%= ViewBag.title %>";' />
            </blockquote>
            

            <%--订单商品Id
            订单编号
            审核
            发货
            用户名
            商品的总金额
            实际支付金额
            优惠抵扣金额
            余额抵扣金额
            支付类型
            订单支付时间--%>
            <table class="layui-table layui-table-admin" lay-even lay-size="sm">
                <thead>
                    <tr>
                        <%--<th></th>--%>
                        <th>订单编号</th>
                        <th>商品名称</th>
                        <th>用户名</th>
                        <th>订单状态</th>
                        <th>商品的总金额</th>
                        <th>实际支付金额</th>
                        <th>优惠抵扣金额</th>
                        <th>余额抵扣金额</th>
                        <th>支付类型</th>
                        <th>订单支付时间</th>
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
                    <%--<td><a href="TgOrderList.aspx?id=6&page=1">查看订单</a></td>--%>
                    <td><%= row["OrderNo"] %></td>
                    <td><%= row["GoodsName"] %></td>
                    <td><%= row["UserName"] %></td>
                    <td><%= row["StatusName"] %></td>
                    <td><%= row["Amount"] %></td>
                    <td><%= row["MoneyPaid"] %></td>
                    <td><%= row["MoneyCoupon"] %></td>
                    <td><%= row["MoneyBalance"] %></td>
                    <td><%= row["Payment"] %></td>
                    <td><%= row["PayTime"] %></td>
                </tr>
        <%//a.UserName, a.Amount, a.MoneyPaid, a.MoneyCoupon, a.MoneyBalance, a.Payment, a.PayTime
                }
            }
        %>
            </table>

            <%--分页函数--%>
            <%=Html.Pager("keyword", Convert.ToString(ViewData["KeyWord"]), true, 9, Model)%>

        </form>
    </div>

    <script src="/content/admin/js/JScript.js"></script>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

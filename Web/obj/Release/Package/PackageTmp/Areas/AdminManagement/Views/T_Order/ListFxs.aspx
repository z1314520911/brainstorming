<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Commons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="/Content/admin/js/axios.min.js"></script>
    <script type="text/javascript" src="/Content/admin/js/view/OrderList.js?t=<%= Config.GetKeyValue("version") %>.3"></script>
    <script type="text/javascript">        var area2 = ['92%', '92%'];
        var url2 = ['/Admin/Model/Index.aspx?page=1&table=T_User&Languages=Cn&keyword=&list='];
    </script>
    <script src="/content/admin/js/JScript.js"></script>
    <%

    %>
    <div class="site-text site-block">
        <form action="/Admin/T_Order/ListFxs.aspx?page=1" class="layui-form" enctype="multipart/form-data" method="post">
            <%--<blockquote class="layui-elem-quote layui-quote-nm layui-elem-quote-admin">
                <input type="text" class="layui-input" id="time" name="time" placeholder="开始时间 - 结束时间" value="<%= ViewData["time"] %>" lay-key="7" readonly />
                <input type="text" class="layui-input" id="name" name="name" placeholder="名称" value="<%= ViewData["name"] %>" lay-key="7">
                <input type="text" class="layui-input" id="phone" name="phone" placeholder="电话" value="<%= ViewData["phone"] %>" lay-key="7">
                <input type="submit" class="layui-btn" value="查询" onclick='this.form.action = "OrderList.aspx?type=Search&page=1";' />
            </blockquote>--%>

            <table class="layui-table layui-table-admin tableSelect" lay-even lay-size="sm">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>真实姓名</th>
                        <th>联系电话</th>
                        <%--<th>下级会员</th>--%>
                        <th>OpenId</th>
                        <th>微信昵称</th>
                        <th>头像</th>
                        <th>分销佣金</th>
                    </tr>
                </thead>
                <%
                    DataTable dt = ViewBag.Dt as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                %>
                <tr>
                    <%--<td><%= row["订单编号"] %></td>
                    <td style="text-align:left; width:200px;"><%= row["订单编号"] %></td>--%>
                    <td><%= row["Id"] %></td>
                    <td align="left" style="padding-left: 5px;"><%= row["Name"] %></td>
                    <td align="left" style="padding-left: 5px;"><%= row["Phone"] %></td>
                    <%--<td><a href='##' onclick='openIframe2(<%= row["UserId"] %>,0,"<%= row["RealName"] %> - 下级会员")'>（下级会员）</a></td>--%>
                    <td><%= row["OpenId"] %></td>
                    <td><%= row["NickName"] %></td>
                    <td><img style="width:60px;height:60px;" src="<%= row["Avatar"] %>" /></td>
                    <td><%= row["Commission"] %></td>
                </tr>
                <%
                        }
                    }
                %>
            </table>

            <%--分页函数--%>
            <%=Html.Pager("time", ViewData["time"]+"&name="+ViewData["name"]+"&phone="+ViewData["phone"], true, 9, Model)%>
        </form>
    </div>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

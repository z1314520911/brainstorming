<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Commons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript" src="/Content/admin/js/axios.min.js"></script>
    <script type="text/javascript" src="/Content/admin/js/view/OrderList.js?t=<%= Config.GetKeyValue("version") %>.7"></script>
    <script type="text/javascript">
      
    </script>
    <script src="/content/admin/js/JScript.js"></script>
    <%
        List<SelectListItem> list = ViewBag.ListStatus as List<SelectListItem>;
        SelectListItem itemSelect = new SelectListItem();
        itemSelect.Text = "所有状态";
        itemSelect.Value = "-1";
        List<SelectListItem> listAll = new List<SelectListItem>();
        listAll.Add(itemSelect);
        listAll.AddRange(list);
    %>
    <div class="site-text site-block">
        <form action="/Admin/T_Order/OrderList.aspx?page=1" class="layui-form" enctype="multipart/form-data" method="post">
            <blockquote class="layui-elem-quote layui-quote-nm layui-elem-quote-admin">
                <%--请输入关键字：<input type="text" class="layui-input" id="keyword" name="keyword" value="" />--%>
                <%--<%= Common.BindControls("datepicker", "time", "", "", "开始时间 - 结束时间") %>--%>
                <input type="text" class="layui-input" id="time" name="time" placeholder="开始时间 - 结束时间" value="<%= ViewData["time"] %>" lay-key="7" readonly />
                <%= Common.BindControls("dropdownlist", "status", ViewData["status"].ToString(), listAll) %>
                <input type="text" class="layui-input" id="orderNo" name="orderNo" placeholder="订单号" value="<%= ViewData["orderNo"] %>" lay-key="7">
                <input type="text" class="layui-input" id="name" name="name" placeholder="收件人名称" value="<%= ViewData["name"] %>" lay-key="7">
                <input type="text" class="layui-input" id="phone" name="phone" placeholder="收件人电话" value="<%= ViewData["phone"] %>" lay-key="7">
                <input type="submit" class="layui-btn" value="查询" onclick='this.form.action="OrderList.aspx?type=Search&page=1";' />
                <input type="button" class="layui-btn" value="导出" onclick='Export();' />
                <%--<span style="color:red;">模板文件第二列“公司编号必填”</span>：--%>
                <input name="filename" id="myxls" type="file" style="width:150px;" />
                <input type="button" class="layui-btn" value="快递单号导入" onclick="Express()" />
            </blockquote>

        <table class="layui-table layui-table-admin tableSelect" lay-even lay-size="sm">
            <thead>
                <tr>
                    <%--<th></th>--%>
                    <th>订单编号</th>
                    <th>商品名称</th>
                    <th>订单类型</th>
                    <th>商品状态</th>
                    <th>份数</th>
                    <th>订单状态</th>
                    <th>快递单号</th>
                    <th>总金额</th>
                    <th>单价</th>
                    <th>支付金额</th>
                    <th>优惠</th>
                    <%--<th>余额抵扣金额</th>--%>
                    <th>用户信息</th>
                    <%--<th>用户地址</th>--%>
                    <th>分销商信息</th>
                    <%--<th>下单时间</th>--%>
                </tr>
            </thead>
<%
DataTable dt = ViewBag.Dt as DataTable;
if(dt.Rows.Count > 0)
{
    string[] strs;
    int index = 1;
    string strName = string.Empty;
    string strNum = string.Empty;
    string strRefund = string.Empty;
    string strId = string.Empty;
    string strPt = string.Empty;
    string strExpressCode = string.Empty;
    string strAddress = string.Empty;
    string strPrice = string.Empty;
    string strStatus = string.Empty;
    foreach(DataRow row in dt.Rows)
    {
        index = 1;
        switch (row["订单类型"].ToString())
        {
            case "0": strPt = "普通订单"; break;
            case "1": strPt = "团购"; break;
            case "2": strPt = "<span style='color:red;'>拼团</span>"; break;
        }
        if (!string.IsNullOrEmpty(row["商品名称"].ToString()))
        {
        strs = row["商品名称"].ToString().Split('★');
        strAddress = row["详细地址"].ToString();
        strStatus = row["Status"].ToString();
        foreach(string item in strs)
        {
            strName = item.Split('＄')[0];
            strNum = item.Split('＄')[1];
            strRefund = item.Split('＄')[2];
            strId = item.Split('＄')[3];
            strExpressCode = item.Split('＄')[4] == "_" ? "请输入单号" : item.Split('＄')[4].Split('_')[0] + "　<a href='##' id='a_"+strId+"' lang=\""+item.Split('＄')[4].Split('_')[0]+"\" rel=\""+item.Split('＄')[4].Split('_')[1]+"\" onclick=\"ExpressSearch('a_"+strId+"');\" style='color:blue;'>查询</a>";
            strPrice = item.Split('＄')[5];
            switch (strRefund)
            {
                case "0": strRefund = "正常"; break;
                case "1": strRefund = "<a href='##' onclick='openIframe2("+strId+",0);'>申请退款</a>"; break;
                case "2": strRefund = "已退款"; break;
            }
            if(index == 1)
            {
%>
            <tr>
                <%--<td><a href="TgOrderList.aspx?id=<%= row["Id"] %>&page=1&title=<%= row["Name"] %>">查看订单</a></td>--%>
                <td><%= row["订单编号"] %></td>
                <td style="text-align:left; width:200px;"><%= strName %></td><%--<%= row["商品名称"].ToString().Replace("★", "<br />") %>--%>
                <td style="text-align:left; width:52px;"><%= strPt %></td>
                <td style="text-align:left; width:52px;"><%= strRefund %></td>
                <td style="text-align:left; width:26px;"><%= strNum %></td>
                <td style="text-align:left; width:96px;">
                    <%= Common.BindControls("dropdownlist", "status_" + row["订单编号"].ToString()+"_"+row["Status"].ToString(), row["Status"].ToString(), list) %>
                    <%--<%= row["订单状态"] %>--%>
                </td>
                <td style="text-align:left; width:150px;"><a onclick="ChangeExpress('ec_<%= strId %>')"><span id="ec_<%= strId %>"><%= strExpressCode %></span></a></td>
                <td><%= row["商品的总金额"] %></td>
                <td style="text-align:left; width:66px;">
                    <span id="sp_<%= strId %>"><%= strPrice %></span> 
                    <% if(strStatus == "1") { %><a onclick="ChangePrice('sp_<%= strId %>')">改价</a><% } %>
                </td>
                <td><span id="sale_sp_<%= strId %>"><%= row["应收款"] %></span></td>
                <td><%= row["优惠抵扣金额"] %></td>
                <%--<td><%= row["余额抵扣金额"] %></td>--%>
                <td style="text-align:left;"><a onclick="ChangeAddress('addr_<%= strId %>', '<%= row["订单编号"] %>')"><%= row["会员姓名"] %><br /><%= row["联系电话"] %></a><span id="addr_<%= strId %>" style="display:none;"><%= strAddress %></span></td>
                <%--<td style="text-align:left; width:120px;"><%= row["详细地址"] %></td>--%>
                <td style="text-align:left;">
                    <%= row["一级分销商"] + ":" + item.Split('＄')[7] %>
                        <br />
                    <%= row["二级分销商"] + ":" + item.Split('＄')[8] %>
                </td>
                <%--<td style="text-align:left; width:96px;"><%= row["下单时间"] %></td>--%>
            </tr>
        <%
            } else { 
        %>
            <tr>
                <td></td>
                <td style="text-align:left; width:200px;"><%= strName %></td>
                <td></td>
                <td style="text-align:left; width:52px;"><%= strRefund %></td>
                <td style="text-align:left; width:26px;"><%= strNum %></td>
                <td></td>
                <td style="text-align:left; width:96px;"><a onclick="ChangeExpress('ec_<%= strId %>')"><span id="ec_<%= strId %>"><%= strExpressCode %></span></a></td>
                <td></td>
                <td style="text-align:left; width:66px;">
                    <span id="sp_<%= strId %>"><%= strPrice %></span> 
                    <% if(strStatus == "1") { %><a onclick="ChangePrice('sp_<%= strId %>')">改价</a><% } %>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <%--<td>详细地址</td>--%>
                <td style="text-align:left;">
                    <%= row["一级分销商"] + ":" + item.Split('＄')[7] %>
                        <br />
                    <%= row["二级分销商"] + ":" + item.Split('＄')[8] %>
                </td>
                <%--<td></td>--%>
            </tr>
<%
            }
            index++;
        }

    }
    }
}
%>
        </table>

        <%--分页函数--%>
        <%=Html.Pager("orderNo", ViewData["orderNo"]+"&time="+ViewData["time"]+"&status="+ViewData["status"]+"&name="+ViewData["name"]+"&phone="+ViewData["phone"], true, 9, Model)%>

        </form>
    </div>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

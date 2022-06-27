<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="/Content/admin/js/img.js"></script>
<script type="text/javascript" src="/Content/ueditor/ueditor.config.js"></script>
<script type="text/javascript" src="/Content/ueditor/ueditor.all.js"></script>
<script type="text/javascript" charset="utf-8" src="/Content/ueditor/lang/zh-cn/zh-cn.js"></script>
                    <mvc:mvcrepeater id="MvcRepeater2" key="myNavBoxList" runat="server">
                        <HeaderTemplate><div class="am-tabs" id="someTabs"><ul class="am-nav-tabs"></HeaderTemplate>
                        <ItemTemplate>
      <li<%# Container.ItemIndex==0?" class=\"am-active\"":""%>><a href="#tab<%# Container.ItemIndex + 1%>"><%#((string)Container.DataItem)%></a></li> 
                        </ItemTemplate>
                        <FooterTemplate></ul><div class="am-tabs-bd" id="someDiv"></FooterTemplate>
                    </mvc:mvcrepeater>
                    <mvc:mvcrepeater id="MvcRepeater1" key="BoxControls" runat="server">
                        <ItemTemplate>
                            <div class="am-padding" <%# Container.ItemIndex==0?"":"style=\"display:none;\""%> id="tab<%# Container.ItemIndex + 1%>">
    <table class="layui-table"><colgroup><col width="20%"><col width="30%"><col width="20%"><col width="30%"></colgroup>
        <%#Container.DataItem%>
    </table></div>
                        </ItemTemplate>
                        <FooterTemplate></div><div class="am-margin-top"><button class="layui-btn" id="layuiBtn1" lay-submit="">提交保存</button> <button type="button" class="layui-btn layui-btn-primary" onclick="history.go(-1)">放弃保存</button>
  </div>
        <script type="text/javascript">
            $(function () {
                BindDivChange("#someTabs li", "am-active", "#someDiv .am-padding");
            })
            function BindDivChange(titleId, titleClass, changeDiv) {
                var $tab1 = $(titleId);
                $tab1.click(function () {
                    var index = $tab1.index(this)
                    $(titleId).removeClass(titleClass)
                    $(this).addClass(titleClass);
                    $(changeDiv).eq(index).show().siblings(changeDiv).hide();
                });
            }
        </script></FooterTemplate>
                    </mvc:mvcrepeater>
                    <mvc:mvcrepeater id="MvcRepeater3" key="Controls" runat="server">
                        <HeaderTemplate>
    <table class="layui-table" lay-even><colgroup><col width="20%"><col width="30%"><col width="20%"><col width="30%"></colgroup></HeaderTemplate>
                        <ItemTemplate><%#Container.DataItem%></ItemTemplate>
                        <FooterTemplate></table></FooterTemplate>
                    </mvc:mvcrepeater>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterContent" runat="server">
    <script>
        layui.use(['form'], function () {
            var form = layui.form,
                layer = layui.layer;
        });
    </script>
    <%=ViewData["ReturnInfo"] %>
</asp:Content>
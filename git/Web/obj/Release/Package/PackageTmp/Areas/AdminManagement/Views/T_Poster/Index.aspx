<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/AdminManagement/Views/Shared/WebAdmin.Master"
    Inherits="System.Web.Mvc.ViewPage<Commons.XmlConfig>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .container {
            padding: 10px;
            border: 5px solid #F90;
            width: 800px;
            height: 200px;
            border-radius: 10px;
            margin: 0 auto;
            position: relative;
        }

        .drag {
            width: 140px;
            height: 140px;
            background: #F90;
            border-radius: 10px;
            margin: 0 10px 10px 0;
            position: absolute;
            left: 0;
            right: 0;
        }

        #poster {
            width: 320px;
            height: 504px;
            border: 1px solid #ccc;
            position: relative
        }

        #poster .bg {
            position: absolute;
            width: 100%;
            z-index: 0
        }

        #poster .drag[type=img] img, #poster .drag[type=thumb] img {
            width: 100%;
            height: 100%;
        }

        #poster .drag {
            position: absolute;
            width: 80px;
            height: 80px;
            border: 1px solid #000;
        }


        #poster .drag[type=nickname] {
            width: 80px;
            height: 40px;
            font-size: 16px;
            font-family: 黑体;
        }

        #poster .drag img {
            position: absolute;
            z-index: 0;
            width: 100%;
        }

        #poster .rRightDown, .rLeftDown, .rLeftUp, .rRightUp, .rRight, .rLeft, .rUp, .rDown {
            position: absolute;
            width: 7px;
            height: 7px;
            z-index: 1;
            font-size: 0;
        }


        #poster .rRightDown, .rLeftDown, .rLeftUp, .rRightUp, .rRight, .rLeft, .rUp, .rDown {
            background: #C00;
        }

        .rLeftDown, .rRightUp {
            cursor: ne-resize;
        }

        .rRightDown, .rLeftUp {
            cursor: nw-resize;
        }

        .rRight, .rLeft {
            cursor: e-resize;
        }

        .rUp, .rDown {
            cursor: n-resize;
        }

        .rLeftDown {
            left: -4px;
            bottom: -4px;
        }

        .rRightUp {
            right: -4px;
            top: -4px;
        }

        .rRightDown {
            right: -4px;
            bottom: -4px;
        }

        .rRightDown {
            background-color: #00F;
        }

        .rLeftUp {
            left: -4px;
            top: -4px;
        }

        .rRight {
            right: -4px;
            top: 50%;
            margin-top: -4px;
        }

        .rLeft {
            left: -4px;
            top: 50%;
            margin-top: -4px;
        }

        .rUp {
            top: -4px;
            left: 50%;
            margin-left: -4px;
        }

        .rDown {
            bottom: -4px;
            left: 50%;
            margin-left: -4px;
        }

        .context-menu-layer {
            z-index: 9999;
        }

        .context-menu-list {
            z-index: 9999;
        }
    </style>
    <div id='poster'>
        <img src='http://wx.ityizu.com/attachment/images/2/2017/08/GR26t82U2Kf85gh2KzTfyQFkyyHyF6.jpg' class='bg' />


    </div>
    <%--<div class="container">
        <div class="drag">
            <div class="rRightDown"></div>
            <div class="rLeftDown"></div>
            <div class="rRightUp"></div>
            <div class="rLeftUp"></div>
            <div class="rRight"></div>
            <div class="rLeft"></div>
            <div class="rUp"></div>
            <div class="rDown"></div>
        </div>
        <div class="drag"></div>
        <div class="drag"></div>
    </div>--%>
    <div class="form-group" id="bgset">
        <label class="col-sm-2 control-label">背景图片</label>
        <div class="col-sm-9 col-xs-12">
            <div class="input-group ">
                <input type="text" name="bg" value="images/3/2017/09/gDAPxdVVxdf8F1SkvfV8IFBIPqKpvf.jpg" class="form-control" autocomplete="off">
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button" onclick="showImageDialog(this);">选择图片</button>
                </span>
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">海报元素</label>
        <div class="col-sm-9 col-xs-12">
            <button class='btn btn-default btn-com' type='button' data-type='head'>头像</button>
            <button class='btn btn-default btn-com' type='button' data-type='nickname'>昵称</button>
            <button class='btn btn-default btn-com' type='button' data-type='qr'>二维码</button>
            <%--<button class='btn btn-default btn-com' type='button' data-type='img'>图片</button>--%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterContent" runat="server">
    <%=ViewData["ReturnAlert"]%>
    <script type="text/javascript" src="/Content/draggabilly/draggabilly.pkgd.min.js"></script>
    <script type="text/javascript" src="/Content/draggabilly/jquery.contextmenu.r2.js"></script>
    <script type="text/javascript" src="/Content/draggabilly/designer.js"></script>
    <script type="text/javascript" src="/Content/draggabilly/poster.js"></script>
</asp:Content>

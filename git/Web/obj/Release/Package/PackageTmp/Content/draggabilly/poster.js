document.writeln("<div class=\'contextMenu\' id=\'myMenu1\'><ul><li id=\'next\'>调整到上层</li><li id=\'prev\'>调整到下层</li><li id=\'last\'>调整到最顶层</li><li id=\'first\'>调整到最低层</li><li id=\'delete\'>删除元素</li></ul></div>");
$(function () {
    //var $drag = $('.drag').draggabilly({ containment: true });
    //$drag.on('dragEnd', function (event, pointer) {

    //    var draggie = $(this).data('draggabilly');
    //    console.log('eventName happened', draggie.position.x, draggie.position.y);
    //});
    //$('.drag').each(function () {
    //    bindEvents($(this));
    //})


    $('.btn-com').click(function () {
        var index = $('#poster .drag').length + 1;
        //var index = obj.attr('index');

        if ($('#poster img').length <= 0) {
            //alert('请选择背景图片!');
            //return;
        }
        var type = $(this).data('type');
        var img = "";
        if (type == 'qr') {
            img = '<img src="http://wx.ityizu.com/addons/ewei_shopv2/plugin/poster/static/images/qr.jpg" />';
        }
        else if (type == 'head') {
            img = '<img src="http://wx.ityizu.com/addons/ewei_shopv2/plugin/poster/static/images/head.jpg" />';
        } else if (type == 'img' || type == 'thumb') {
            img = '<img src="http://wx.ityizu.com/addons/ewei_shopv2/plugin/poster/static/images/img.jpg" />';
        }
        else if (type == 'nickname') {
            img = '<div class=text>昵称</div>';
        } else if (type == 'title') {
            img = '<div class=text>商品名称</div>';
        } else if (type == 'marketprice') {
            img = '<div class=text>商品现价</div>';
        } else if (type == 'productprice') {
            img = '<div class=text>商品原价</div>';
        } else {
            img = '<img src="http://wx.ityizu.com/addons/ewei_shopv2/plugin/poster/static/images/qr.jpg" />';
        }

        var obj = $('<div class="drag" type="' + type + '" index="' + index + '" style="z-index:' + index + '">' + img + '<div class="rRightDown"> </div><div class="rLeftDown"> </div><div class="rRightUp"> </div><div class="rLeftUp"> </div><div class="rRight"> </div><div class="rLeft"> </div><div class="rUp"> </div><div class="rDown"></div></div>');
        $('#poster').append(obj);
        bindEvents(obj);

    });

    bindDrag("QR");
    bindDrag("Head");
    bindDrag("NickName");

});


function bindEvents(obj) {

    var index = obj.attr('index');

    var rs = new Resize(obj, { Max: true, mxContainer: "#poster" });
    rs.Set($(".rRightDown", obj), "right-down");
    rs.Set($(".rLeftDown", obj), "left-down");
    rs.Set($(".rRightUp", obj), "right-up");
    rs.Set($(".rLeftUp", obj), "left-up");
    rs.Set($(".rRight", obj), "right");
    rs.Set($(".rLeft", obj), "left");
    rs.Set($(".rUp", obj), "up");
    rs.Set($(".rDown", obj), "down");
    rs.Scale = true;
    var type = obj.attr('type');
    if (type == 'nickname' || type == 'img' || type == 'title' || type == 'marketprice' || type == 'productprice') {
        //rs.Scale = false;
    }
    //new Drag(obj, { Limit: true, mxContainer: "#poster" });
    new Drag(obj, {
        Limit: true, mxContainer: "#poster", onStop: function () {
            //console.log(obj.css("top"));
            //var draggie = $(this).data('draggabilly');
            //console.log('eventName happened', draggie.position.x, draggie.position.y);
        }
    });
    $('.drag .remove').unbind('click').click(function () {
        $(this).parent().remove();
    })

    obj.contextMenu('myMenu1',
        {
            bindings:
                {
                    'next': function () {
                        var nextdiv = obj.next('.drag');
                        if (nextdiv.length > 0) {
                            nextdiv.insertBefore(obj);
                        }
                        dragReset();
                    },
                    'prev': function () {
                        var prevdiv = obj.prev('.drag');
                        if (prevdiv.length > 0) {
                            obj.insertBefore(prevdiv);
                        }
                        dragReset();
                    },
                    'last': function () {
                        var index = parseInt(obj.attr('zindex'));
                        var len = $('.drag').length;
                        if (index >= len - 1) {
                            return;
                        }
                        var last = $('#poster .drag:last');
                        if (last.length > 0) {
                            obj.insertAfter(last);
                        }
                        dragReset();
                    },
                    'first': function () {
                        var index = obj.index();
                        if (index <= 1) {
                            return;
                        }
                        var first = $('#poster .drag:first');
                        if (first.length > 0) {
                            obj.insertBefore(first);
                        }
                        dragReset();
                    },
                    'delete': function () {
                        obj.remove();
                        dragReset();
                    }
                }
        });
}

function dragReset() {
    var n = 1;
    $('.drag').each(function () {
        $(this).css("z-index", n);
        n++;
    })
}

function bindDrag(id) {
    var date = $("#" + id).val();
    if (date != "") {
        var str = date.split(",");
        for (var i = 0; i < str.length; i++) {
            if (str[i] != "") {
                var arr = str[i].split("|");
                if (arr.length == 5) {
                    if (id == 'Head') {
                        $('.head').click();
                    }
                    else if (id == 'QR') {
                        $('.qr').click();
                    }
                    else if (id == 'NickName') {
                        $('.nickname').click();
                    }
                    var drag = $("#poster .drag:last");
                    drag.css("z-index", arr[0]);
                    drag.css("left", arr[1]);
                    drag.css("top", arr[2]);
                    drag.css("width", arr[3]);
                    drag.css("height", arr[4]);
                }
            }
        }
    }
}

$(function () {
    layui.use('form', function () {
        var form = layui.form;
        form.on('select(Pic)', function (data) {
            $(".bg").attr("src", data.value);
        });
    });
    $("#layuiBtn1").on('click', function (event) {
        event.preventDefault();

        $("#QR").val("");
        $("#Head").val("");
        $("#NickName").val("");
        $('.drag').each(function () {
            var type = $(this).attr('type');
            var hidden;
            if (type == 'qr') {
                hidden = $("#QR");
            }
            else if (type == 'head') {
                hidden = $("#Head");
            }
            else if (type == 'nickname') {
                hidden = $("#NickName");
            }
            console.log(type);
            console.log(hidden);
            if (hidden.val() != "") {
                hidden.val(hidden.val() + ",");
            }
            hidden.val(hidden.val() + $(this).css('z-index') + "|" + $(this).css('left') + "|" + $(this).css('top') + "|" + $(this).css('width') + "|" + $(this).css('height'));
        })
        $("form").submit();
    });
});
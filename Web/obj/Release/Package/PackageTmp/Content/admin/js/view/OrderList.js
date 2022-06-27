var area2 = ['92%', '92%'];
var url2 = ['/Admin/Model/Edit.aspx?table=T_ReFund★ByDetailId&Languages=Cn&id='];

$(function () {
    layui.use(['laydate', 'jquery', 'form'], function () {
        var laydate = layui.laydate;
        laydate.render({ elem: '#time', range: "~" });

        var $ = layui.jquery;
        var form = layui.form;
        form.on('select', function (data) {
            var sid = data.elem.id;
            var befordValue = sid.split("_")[2];
            if (sid != "status") {
                layer.confirm('确认更改订单状态？', {
                    btn: ['确认', '取消'],
                    cancel: function () {
                        $("[name=" + sid + "]").val(befordValue);
                        form.render('select');
                        layer.msg("取消更改，恢复原状态");
                    }
                }, function () {
                    var loadIndex = layer.load(1, {
                        shade: [0.6, '#3e3e3e'] //0.1透明度的白色背景
                    });
                    $.ajax({
                        url: "/Admin/T_Order/ChOrderStauts.aspx",
                        dataType: "json",
                        type: "post",
                        data: { OrderNo: sid.split("_")[1], NS: data.value, S: befordValue },
                        success: function (res) {
                            layer.msg(res.Message);
                        },
                        complete: function () {
                            layer.close(loadIndex);
                        }
                    })
                }, function () {
                    $("[name=" + sid + "]").val(befordValue);
                    form.render('select');
                    layer.msg("取消更改，恢复原状态");
                }
                );
            }
            //form.render('select');
        });
    });
});

function Search() {
    var url = "OrderList.aspx?type=Search&page=1";
    this.form.action = url;
}

function Export() {
    var url = "/Admin/T_Order/OrderListExport.aspx?t=1";
    if ($("#status").val().trim() != "") {
        url += "&status=" + $("#status").val();
    }
    if ($("#time").val().trim() != "") {
        url += "&time=" + $("#time").val();
    }
    window.open(url);
}

function Refund(id) {
    var url = "/Admin/T_Order/Refund.aspx";
    $.ajax({
        url: url,
        dataType: "json",
        type: "post",
        data: { id: id },
        success: function (res) {
            layer.msg(res.Message);
        },
        complete: function () {
            layer.close(loadIndex);
        }
    })
}

function Express() {
    let file = $("#myxls")[0].files[0];
    var formData = new FormData();
    formData.append("myxls", $("#myxls")[0].files[0]);
    console.log(file);
    layer.load(2, {
        shade: [0.9, '#6a6a6a']
    });
    axios.post('/Admin/T_Order/ExpressUpload.aspx', formData)
        .then(function (res) {
            //此处演示关闭
            layer.closeAll('loading');
            console.log(res);
            if (res.data.Message != "") {
                alert(res.data.Message);
            }
            if (res.data.Code > 0) {
                window.location.reload();
            }
        })
        .catch(function (error) {
            console.log(error);
        });
}

function ChangeExpress(id) {
    //prompt层
    layer.prompt({ title: id + ':请输入快递单号，并确认', formType: 0 }, function (text, index) {
        $.ajax({
            url: "/Admin/T_Order/ChangeExpress.aspx",
            dataType: "json",
            type: "post",
            data: { id: id.split("_")[1], ec: text },
            success: function (res) {
                layer.msg(res.Message);
                $("#" + id).html(text);
                $("#a_" + id.split("_")[1]).attr("lang", text);
            },
            complete: function () {
                layer.close(index);
            }
        })
    });
}
//快递查询
function ExpressSearch(id) {
    var code = $("#"+id).attr("lang");
    var com = $("#"+id).attr("rel");
    $.ajax({
        url: "/Api.ashx",
        dataType: "json",
        type: "post",
        data: { action: "Express", code: code, com: com },
        success: function (res) {
            if (res.message == "ok") {
                var html = "<table><th style='width:158px;'>时间</th><th>描述</th>";
                $.each(res.data, function (i, obj) {
                    html += "<tr style='line-height:28px;'><td>" + obj.time + "</td><td>" + obj.context + "</td></tr>";
                });
                html += "</table>";
                layer.open({
                    type: 1,
                    skin: 'layui-layer-rim', //加上边框
                    area: ['680px', '380px'], //宽高
                    content: html
                });
            } else {
                layer.msg(res.message);
            }
        },
        complete: function () {

        }
    })
}
function getLocalTime(ns) {
    return new Date(parseInt(ns) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ');
}

function ChangePrice(id) {
    //prompt层
    layer.prompt({ title: id + ':改价确认', formType: 0 }, function (text, index) {
        $.ajax({
            url: "/Admin/T_Order/ChangePrice.aspx",
            dataType: "json",
            type: "post",
            data: { id: id.split("_")[1], sp: text },
            success: function (res) {
                layer.msg(res.Message);
                if (res.Code > 0) {
                    $("#" + id).html(text);
                    $("#sale_" + id).html(res.Detail);
                }
            },
            complete: function () {
                layer.close(index);
            }
        })
    });
}

function ChangeAddress(id, oid) {
    var addr = $("#" + id).html();
    layer.prompt({ title: oid + ':修改订单地址', formType: 2, value: addr }, function (text, index) {
        $.ajax({
            url: "/Admin/T_Order/ChangeAddress.aspx",
            dataType: "json",
            type: "post",
            data: { id: id.split("_")[1], sp: text },
            success: function (res) {
                layer.msg(res.Message);
                if (res.Code > 0) {
                    $("#" + id).html(text);
                    $("#addr_" + id).html(res.Detail);
                }
            },
            complete: function () {
                layer.close(index);
            }
        })
    });
}
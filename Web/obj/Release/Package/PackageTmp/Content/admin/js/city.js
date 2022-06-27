$(function () {
    layui.use('form', function () {
        var form = layui.form;
        SelectBind("Province", 0, form);

        SelectBind("City", $("#ProvinceId").val(), form);
        $("#City").val($("#CityId").val());

        SelectBind("Area", $("#CityId").val(), form);
        $("#Area").val($("#AreaId").val());

        form.on('select(Province)', function (data) {
            SelectBind("City", data.value, form);
        });

        form.on('select(City)', function (data) {
            SelectBind("Area", data.value, form);
        });
    });
});

function SelectBind(id, pid, form) {
    var value = "";
    var level = 2;
    if (id == "Province") {
        value = "请选择省份";
        $("#City").empty();
        $("#City").append("<option value=''>请选择城市</option>");
        $("#Area").empty();
        $("#Area").append("<option value=''>请选择区域</option>");
    } else if (id == "City") {
        level = 3;
        value = "请选择城市";
        $("#Area").empty();
        $("#Area").append("<option value=''>请选择区域</option>");
    } else if (id == "Area") {
        level = 4;
        value = "请选择区域";
    }
    if (id == "Province" || pid > 0) {
        var url = '/Admin/T_City/GetCity.aspx?pid=' + pid + "&level=" + level;
        $.ajax({
            url: url,
            dataType: "json",
            type: "post",
            success: function (data) {
                $("#" + id).empty();
                $("#" + id).append("<option value=''>" + value + "</option>");
                $.each(data.rows, function (i, item) {
                    $("#" + id).append("<option value='" + item.Id + "'>" + item.Name + "</option>");
                });
                if (id == "Province") {
                    var tpid = $("#ProvinceId").val();
                    if (tpid == '' || tpid == 0) {
                        tpid = 21;
                        SelectBind("City", tpid, form);
                    }
                    $("#Province").val(tpid);
                }
                if (id == "City") {
                    var tpid = $("#CityId").val();
                    if (tpid == '' || tpid == 0) {
                        tpid = 269;
                        SelectBind("Area", tpid, form);
                    }
                    $("#City").val(tpid);
                }
                if (id == "Area") {
                    $("#Area").val($("#AreaId").val());
                }
                form.render('select');
            }
        });
    }
}

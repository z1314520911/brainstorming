$(function () {
    layui.use('form', function () {
        var form = layui.form;
        SelectBind("RoomId", $("#RoomType").val(), form);

        form.on('select(RoomType)', function (data) {
            SelectBind("RoomId", data.value, form);
        });
    });
});

function SelectBind(id, pid, form) {
    var value = "请选择房间";

    if (pid != 2) {
        value = "该类型不需要选择房间";
    }

    if (id == "RoomId" || pid == 2) {
        var url = '/Admin/T_Room/GetRoom.aspx?catId=' + pid;
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
                $("#RoomId").val($("#RoomIdId").val());
                form.render('select');
            }
        });
    }
}

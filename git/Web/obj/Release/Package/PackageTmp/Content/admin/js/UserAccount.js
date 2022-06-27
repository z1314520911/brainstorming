
layui.use(['form'], function () {
    var form = layui.form,
        layer = layui.layer;
    form.on('submit(formAjax)', function (data) {
        $.ajax({
            cache: true,
            type: "POST",
            url: "/Admin/T_UserAccount/CreateAccount.aspx",
            data: $('#form1').serialize(),
            dataType: "text",
            async: false,
            error: function (request) {
                alert("Connection error");
            },
            success: function (data) {
                if (data == "100") {
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                    parent.location.reload();
                }
                else {
                    layer.msg(data);
                }
            }
        });
        return false;
    });
});
function SaveSelect() {
    $("#save").click(function () {
        var data = [];
        $(".selection").each(function () {
            data.push($.trim($(this).val()));
        })

        alert(data.join(','));
    });
}
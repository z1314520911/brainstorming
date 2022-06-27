$(document).ready(function () {
    var getData = function (obj) {
        $.ajax({
            url: '/Admin/T_UserAccount/GetUser.aspx?userId=' + $("#list").val(),
            type: 'POST',
            data: { "pid": obj.val() },
            dataType: 'html',
            async: false,
            success: function (data) {
                if ($(".selection").length) {
                    $(".selection:gt(" + $(obj).index() + ")").remove(); //移除后面所有子级下拉菜单
                    $(".selection:last").after(data);     //添加子级下拉菜单
                } else {
                    $("#selctContainer").append(data);     //初始加载情况
                }
                //所有下拉响应
                $(".selection").unbind().change(function () {
                    getData($(this));
                });
            },
            error: function (msg) {
                alert('error');
            }
        });
    }
    getData($(this));
});
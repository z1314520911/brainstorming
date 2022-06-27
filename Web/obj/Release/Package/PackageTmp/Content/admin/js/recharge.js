
function recharge(id, username, money) {
    layui.use('layer', function () {
        var $ = layui.jquery, layer = layui.layer;
        layer.open({
            type: 1
            , title: false
            , closeBtn: false
            , area: '300px;'
            , shade: 0.8
            , id: 'LAY_layuipro'
            , btn: ['确认入账', '取消']
            , btnAlign: 'c'
            , moveType: 1
            , content: '<div style="padding: 50px; line-height: 22px; background-color: #393D49; color: #fff; font-weight: 300;">账号：' + username + '<br/>金额：' + money + '</div>'
            , success: function (layero) {
                var btn = layero.find('.layui-layer-btn');
                btn.find('.layui-layer-btn0').attr({
                    href: url + id
                });
            }
        });
    });
}
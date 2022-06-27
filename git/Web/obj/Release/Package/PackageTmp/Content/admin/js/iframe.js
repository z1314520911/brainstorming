$(function () {
    layui.use('layer', function () {
        var layer = layui.layer;
        var areaNew;
        if (typeof (area) == "undefined") areaNew = ['92%', '78%'];
        else areaNew = area;
        for (var i = 0; i < buttonId.length; i++) {
            var url = urlId[i] + "&id=" + buttonId[i];
            $('#' + buttonId[i] + "Button").bind("click", url, function (url) {
                layer.open({
                    type: 2,
                    content: url.data,
                    offset: '10%',
                    area: areaNew,
                    success: function (layero, index) {
                        //var body = layer.getChildFrame('body', index);
                        //var iframeWin = window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                        //console.log(body.html()) //得到iframe页的body内容
                        //body.find('input').val('Hi，我是从父页来的')
                    }
                });
            });
        }
    });
});
function BindCheckBox(name, value, text) {
    if ($("#" + name + value).length == 0) {
        layui.use('form', function () {
            var form = layui.form;
            //if (name == "Avatar") {
            //    text = "<img src='" + text + "' />";
            //}
            $("#" + name + "Div").append("<input type=\"checkbox\" id=\"" + name + value + "\" name=\"" + name + "\" value=\"" + value + "\" checked  title=\"" + text + "\">");
            form.render('checkbox');
        });
    }
}
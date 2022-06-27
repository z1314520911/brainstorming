
layui.use('form', function () {
    var form = layui.form;

    form.on('checkbox(CheckAll)', function (data) {
        var items = document.getElementsByTagName("input");
        for (i = 0; i < items.length; i++) {
            if (items[i].type == "checkbox") {
                items[i].checked = data.elem.checked;
            }
        }
        form.render('checkbox');
    });
});

function openIframe(id, txt) {
    var areaNew;
    if (typeof (area) == "undefined") areaNew = ['92%', '78%'];
    else areaNew = area;
    layui.use('layer', function () {
        var layer = layui.layer;
        layer.open({
            type: 2,
            title: txt,
            area: areaNew,
            fixed: false, //不固定
            maxmin: true,
            content: url + id
        });
    });
}

function openIframe2(id, type, title, key) {
    var areaNew, titleNew;
    if (typeof (area2) == "undefined") areaNew = ['92%', '78%'];
    else areaNew = area2;
    if (typeof (title) == "undefined") titleNew = "信息";
    else titleNew = title;
    var urlTmp = url2[type] + id;
    if (typeof (key) != "undefined") urlTmp = url2[type] + id + "&keyword=" + key;
    layui.use('layer', function () {
        var layer = layui.layer;
        layer.open({
            type: 2,
            title: titleNew,
            area: areaNew,
            fixed: false, //不固定
            maxmin: true,
            content: urlTmp
        });
    });
}

function openIframe22(id, type, key) {
    var areaNew;
    if (typeof (area2) == "undefined") areaNew = ['92%', '78%'];
    else areaNew = area2;
    var urlTmp = url2[type] + id;
    if (typeof (key) != "undefined") urlTmp = url2[type].replace("★", key) + id;
    layui.use('layer', function () {
        var layer = layui.layer;
        layer.open({
            type: 2,
            area: areaNew,
            fixed: false, //不固定
            maxmin: true,
            content: urlTmp
        });
    });
}

function openIframe3(id) {
    var areaNew;
    if (typeof (area3) == "undefined") areaNew = ['92%', '78%'];
    else areaNew = area3;
    layui.use('layer', function () {
        var layer = layui.layer;
        layer.open({
            type: 2,
            area: areaNew,
            fixed: false, //不固定
            maxmin: true,
            content: url3 + id,
            cancel: function (index) {
                location.reload();
                layer.close(index);
            }
        });
    });
}
jQuery.fn.extend({
    uploadPreview: function () {
        var _self = this,
            _this = $(this);
        var opts = jQuery.extend({
            Img: "ImgPr",
            Width: "100%",
            Height: "100px",
            ImgType: ["gif", "jpeg", "jpg", "bmp", "png"],
            Callback: function () { }
        }, opts || {});
        _self.getObjectURL = function (file) {
            var url = null;
            if (window.createObjectURL != undefined) {
                url = window.createObjectURL(file)
            } else if (window.URL != undefined) {
                url = window.URL.createObjectURL(file)
            } else if (window.webkitURL != undefined) {
                url = window.webkitURL.createObjectURL(file)
            }
            return url
        };
        _this.change(function () {
            if (this.value) {
                if (!RegExp("\.(" + opts.ImgType.join("|") + ")$", "i").test(this.value.toLowerCase())) {
                    alert("选择文件错误,图片类型必须是" + opts.ImgType.join("，") + "中的一种");
                    this.value = "";
                    return false
                }
                var obj = $("#" + $(this).attr('id') + "ImgPr");
                if (/msie/.test(navigator.userAgent.toLowerCase())) {
                    try {
                        obj.attr('src', _self.getObjectURL(this.files[0]))
                    } catch (e) {
                        var src = "";
                        var div = obj.parent("div")[0];
                        _self.select();
                        if (top != self) {
                            window.parent.document.body.focus()
                        } else {
                            _self.blur()
                        }
                        src = document.selection.createRange().text;
                        document.selection.empty();
                        obj.attr('src', '');
                        obj.hide();
                        obj.parent("div").css({
                            'filter': 'progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)',
                            'width': opts.Width,
                            'height': opts.Height
                        });
                        div.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = src
                    }
                } else {
                    obj.attr('src', _self.getObjectURL(this.files[0]))
                }
                opts.Callback();
            }
        })
    }
});
$.extend({
    include: function (file) {
        var files = typeof file == "string" ? [file] : file;
        for (var i = 0; i < files.length; i++) {
            var name = files[i].replace(/^\s|\s$/g, "");
            var att = name.split('.');
            var ext = att[att.length - 1].toLowerCase();
            var isCSS = ext == "css";
            var tag = isCSS ? "link" : "script";
            var attr = isCSS ? " type='text/css' rel='stylesheet' " : " language='javascript' type='text/javascript' ";
            var link = (isCSS ? "href" : "src") + "='" + name + "'";
            if ($(tag + "[" + link + "]").length == 0) document.write("<" + tag + attr + link + "></" + tag + ">");
        }
    }
});
if (!/Android|webOS|iPhone|iPod|iPad|BlackBerry/i.test(navigator.userAgent)) {
    $.include(['/js/jquery.cookie.js']);
}
$(function () {
    if ($.cookie("isClose") != 'yes') {
        var onclick = "";
        if (imUrl != "") {
            onclick = "onclick =\"javascript: window.location = '" + imUrl + "';\"";
        }
        $("body").append("<style>.box{position:fixed;z-index:9999}.box_bg{top:0;bottom:0;left:0;right:0;position:fixed;z-index:100;background-color:#000;filter:alpha(opacity=50);-moz-opacity:0.5;opacity:0.5}.box_img{top:0;bottom:0;left:0;right:0;position:fixed;z-index:200;background-position:left top;background-size:cover}.close{position:fixed;top:1rem;right:1rem;color:#fff;padding:8px 10px 10px;z-index:300;background-color:#000;filter:alpha(opacity=50);-moz-opacity:0.5;opacity:0.5;cursor:pointer}.close span{font-weight:bold;color:#cc0;padding:0 5px}</style><div class=\'box\' " + onclick + "><div class=\'box_bg\'></div><div class=\'box_img\'style=\'background-image:url(" + image + ")\'></div><div class=\'close\'>时间还剩<span id=\'closeTime\'>" + time + "</span>秒</div></div>");
        adstime();
        $('.close').click(function () {
            //$('.box').hide();
            //$.cookie("isClose", 'yes', { expires: 1 });
        })
    } else {
    }
});
function adstime() {
    $t = $('#closeTime').html();
    if ($t != 0) {
        $('#closeTime').html($t - 1);
        setTimeout("adstime()", 1000);
    } else {
        $('.box').hide();
        //$.cookie("isClose", 'yes', { expires: 1 });
    }
};
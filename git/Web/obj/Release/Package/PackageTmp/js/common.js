
//倒计时
function Timer() {
    // 当前时间
    var nowTime = new Date().getTime();
    //开始时间
    var startTime = new Date(Date.parse($("#time_s").val()));
    // 结束时间
    var endTime = new Date(Date.parse($("#time_e").val()));
    // 相差的时间
    var t = 0;
    if (nowTime < startTime.getTime()) {
        status = '开始';
        t = startTime.getTime() - nowTime;
    } else {
        var status = '结束';
        t = endTime.getTime() - nowTime;
    }
    if (t < 0) {
        var y = endTime.getYear();
        var m = endTime.getMonth() + 1;
        var d = ""; //endTime.getDay();
        var h = endTime.getHours();
        var i = endTime.getMinutes();
        var s = endTime.getSeconds();
        var html = '<p><i class="iconfont icon-time fontcolor"></i> 投票于<span>' + m + '</span>月<span>' + d + '</span>日<span>' + h + '</span>时<span>' + i + '</span>分<span>' + s + '</span>秒<span style="background: none;">' + status + '</span></p>';
        $('.status-line').html(html);
        return false;
    }
    if (t > 0) {
        var d = formatNum(Math.floor(t / 1000 / 60 / 60 / 24));
        var h = formatNum(Math.floor(t / 1000 / 60 / 60 % 24));
        var i = formatNum(Math.floor(t / 1000 / 60 % 60));
        var s = formatNum(Math.floor(t / 1000 % 60));

        var html = '<p><i class="iconfont icon-time fontcolor"></i> 投票<span style="background: none;">' + status + '</span>倒计时<span>' + d + '</span>天<span>' + h + '</span>时<span>' + i + '</span>分<span>' + s + '</span>秒</p>';
        $('.status-line').html(html);
        setTimeout(Timer, 1000);
    }
    if (t == 0) {
        location.reload();
    }
}
function formatNum(num) {
    return ("0" + num).slice(-2);
}
function isWeiXin() {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == 'micromessenger') {
        return true;
    } else {
        return false;
    }
}
$(document).ready(function () {
    Timer();
    $("#play").click(function () {
        if ($("#bgMusic").get(0).paused) {
            $("#bgMusic").get(0).play()
            $(this).addClass("rotate");
        } else {
            $("#bgMusic").get(0).pause();
            $(this).removeClass("rotate");
        }
    });
    var swiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        paginationClickable: true,
        /*spaceBetween: 30,*/
        autoplay: 2000
    });
});
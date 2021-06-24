$(document).ready(function () {
    var offset = $('#LeftMenu').offset();
    var topPadding = $('#topPadding').height();
    $(window).scroll(function () {
        if ($(window).scrollTop() > offset.top) {
            $('#LeftMenu').stop().animate({ marginTop: $(window).scrollTop() - offset.top + topPadding });
        }
        else {
            $('#LeftMenu').stop().animate({ marginTop: 0 });
        }
    });
});
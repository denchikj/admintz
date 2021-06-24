//drag & resize
$(function () {
    // thanks to http://stackoverflow.com/a/22885503
    var waitForFinalEvent = function () { var b = {}; return function (c, d, a) { a; b[a] && clearTimeout(b[a]); b[a] = setTimeout(c, d) } }();
    var fullDateString = new Date();

    function isBreakpoint(alias) {
        return $('.device-' + alias).is(':visible');
    }

    var options = {
        float: false
    };

    $('.grid-stack').gridstack(options);
    function resizeGrid() {
        var grid = $('.grid-stack').data('gridstack');
        $('#grid-size').text('One column mode');
    };

    $(window).resize(function () {
        waitForFinalEvent(function () {
            resizeGrid();
        }, 300, fullDateString.getTime());
    });
});




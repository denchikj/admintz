function Animate() {
    var time = 4;
    $('span').each(function () {
        var i = 0,
            num = $(this).data('num'),
            step = time * 200 / num,
            that = $(this),
            int = setInterval(function () {
                if (i <= num) {
                    that.html(i);
                }
                else {
                    clearInterval(int);
                }
                i++;
            }, step);
    });
}
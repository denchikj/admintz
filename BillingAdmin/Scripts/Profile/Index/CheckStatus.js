function checkStatus(status, curr, prev) {
    var a = parseInt($(curr).text().replace(/[^\d]/ig, ''));
    var b = parseInt($(prev).text().replace(/[^\d]/ig, ''));
    if (a < b) {
        $(status).addClass('fa-caret-down font-grey');
    }
    if (a > b) {
        $(status).addClass('fa-caret-up');
    }
}
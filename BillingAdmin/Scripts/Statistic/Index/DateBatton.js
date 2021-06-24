$('body').on('click', '#BtnToDay', function () {
    var currentDate = new Date().toLocaleDateString();
    InsertDate(currentDate, currentDate);
    statisticAjaxRequest();
});
$('body').on('click', '#BtnThisWeek', function () {
    var dateFrom = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate() - new Date().getDay() + 1).toLocaleDateString();
    var dateTo = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate() - new Date().getDay() + 7).toLocaleDateString();
    InsertDate(dateFrom, dateTo);
    statisticAjaxRequest();
});
$('body').on('click', '#BtnThisMonth', function () {
    var dateFrom = new Date(new Date().getFullYear(), new Date().getMonth(), 1).toLocaleDateString();
    var dateTo = new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).toLocaleDateString();
    InsertDate(dateFrom, dateTo);
    statisticAjaxRequest();
});
$('body').on('click', '#BtnYesterday', function () {
    var date = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate() - 1).toLocaleDateString();
    InsertDate(date, date);
    statisticAjaxRequest();
});
$('body').on('click', '#BtnLastWeek', function () {
    var dateFrom = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate() - new Date().getDay() - 6).toLocaleDateString();
    var dateTo = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate() - new Date().getDay()).toLocaleDateString();
    InsertDate(dateFrom, dateTo);
    statisticAjaxRequest();
});
$('body').on('click', '#BtnLastMonth', function () {
    var dateFrom = new Date(new Date().getFullYear(), new Date().getMonth() - 1, 1).toLocaleDateString();
    var dateTo = new Date(new Date().getFullYear(), new Date().getMonth(), 0).toLocaleDateString();
    InsertDate(dateFrom, dateTo);
    statisticAjaxRequest();
});

function InsertDate(dateFrom, dateTo) {
    $('#dateFrom').val(dateFrom);
    $('#dateTo').val(dateTo);
}
function statisticAjaxRequest()
{
    $('#form0').submit();   
}
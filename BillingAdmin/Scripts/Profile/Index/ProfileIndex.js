$(function () {
    Animate();
    iteratesStatus();
    AjaxRequest(0);
});
$('body').on('change', '#SelStatisiticProject', function () {
    var id = $(this).val();
    AjaxRequest(id);
});
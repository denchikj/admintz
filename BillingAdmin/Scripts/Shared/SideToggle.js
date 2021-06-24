$('.left-side-collapse').on('click', function () {
    $('#left-side').toggleClass('active');
});
$('#left-side a').on('click', function () {
    hideSideOnClick('#left-side');
});
function hideSideOnClick(side) {
    if ($(window).width() < 768) $(side).removeClass('active');
}
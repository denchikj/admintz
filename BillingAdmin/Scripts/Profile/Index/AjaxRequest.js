function AjaxRequest(id) {
    LoaderShow();
    //$.ajax({
    //    type: 'GET',
    //    url: 'Profile/StatisticBlock/' + id,
    //    success: function (html) {
    //        $('#StatisticBlock').replaceWith(html);
    //        Animate();
    //        iteratesStatus();
    //    }
    //});
    $.ajax({
        type: 'GET',
        url: 'Profile/ProfileInfo',
        success: function (html) {
            $('#ProfileInfo').replaceWith(html);            
        }
    });
    $.ajax({
        type: 'GET',
        url: 'Profile/Documents',
        success: function (html) {
            $('#Documents').replaceWith(html);
        }
    });
    $.ajax({
        type: 'GET',
        url: 'Profile/Projects',
        success: function (html) {
            $('#Projects').replaceWith(html);
        }
    });
    $.ajax({
        type: 'GET',
        url: 'Profile/News',
        success: function (html) {
            $('#News').replaceWith(html);
        }
    });
}       
function LoaderShow() {
    $('#StatisticBlock').replaceWith(
        "<div id=\"StatisticBlock\" style=\" position: center; text-align: center; min-height: 10vh; width: 100 %; height: 100 %; z-index: 9999; \">" +
        "<img src='/images/Loader.gif' alt='Идет загрузка'>" +
        "</div >"
    );
}
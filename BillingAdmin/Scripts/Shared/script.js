(function($) {
    $(document).ready(function() {
        /*-------меню для мобильной версии---------*/
        $('.navbar-toggle').on('click',
            function() {
                $(this).addClass('active');
                if ($('.navbar-collapse').hasClass('in')) {
                    $(this).removeClass('active');
                }
            });
        new jBox('Tooltip',
            {
                attach: '.tooltip-offset',
                getContent: 'data-jbox-content',
                width: 440,
                offset: {
                    y: -10
                }
            });
    });
})(jQuery);
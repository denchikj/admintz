(function ($) {
    $(document).ready(function () {
        $("a.scrollto").click(function () {
            $("html, body").animate({
                scrollTop: $($(this).attr("href")).offset().top + "px"
            }, {
                    duration: 500,
                    easing: "swing"
                });
            return false;
        });

        SetMask();

        /*-------CUSTOM SCRIPTS ADD CLASS ACTIVE---------*/
        $('.navbar-toggle').on('click', function () {
            $(this).addClass('active');
            if ($('.navbar-collapse').hasClass('in')) {
                $(this).removeClass('active');
            }
        });

        /*-------OWL CARUSEL---------*/
        $('.owl-carousel').owlCarousel({
            loop: true,
            nav: false,
            items: 1,
            autoplay: true,
            autoplayTimeout: 7000
        });

        /*-------ACCARDION CUSTOM SCRIPT---------*/
        $('.solutions-link').click(function () {
            $(this).toggleClass('active');
            $(this).parent().parent().siblings().find('.solutions-link').removeClass('active');
            $(this).parent().parent().siblings('#more-solutions').find('.solutions-link').removeClass('active').parent().siblings().removeClass('in');
            if ($(this).parents().is('#more-solutions')) {
                $(this).parents('#more-solutions').siblings().find('.solutions-link').removeClass('active').parent().siblings().removeClass('in');
            }
            if ($('#sl1').hasClass('active'))
                $('#ph1').addClass('border-orange');
            else $('#ph1').removeClass('border-orange');

            if ($('#sl2').hasClass('active'))
                $('#ph2').addClass('border-orange');
            else $('#ph2').removeClass('border-orange');

            if ($('#sl3').hasClass('active'))
                $('#ph3').addClass('border-orange');
            else $('#ph3').removeClass('border-orange');

            if ($('#sl4').hasClass('active'))
                $('#ph4').addClass('border-orange');
            else $('#ph4').removeClass('border-orange');
        });

       
        /*static remove class to tablet*/
        function fixToPhone() {
            $('.header_two').affix({
                offset: 75
            })
        }
        if ($(window).width() < 768) {
            $('.add_service-control').removeClass('in');
            fixToPhone();

            $('.btn[data-toggle="modal"]').click(function () {
                if ($('.header_two').hasClass('affix')) {
                    $('.modal.right .modal-dialog').addClass('pos_top')
                } else {
                    $('.modal.right .modal-dialog').removeClass('pos_top')
                }
            })
        }

        /*-------WHEN RESIZE WINDOW HIDE LIST---------*/
        $(window).resize(function () {
            if ($(window).width() < 768) {
                $('.add_service-control').removeClass('in');
                fixToPhone();
            } else {
                $('.add_service-control').addClass('in');
            }

            $('.btn[data-toggle="modal"]').click(function () {
                if ($('.header_two').hasClass('affix')) {
                    $('.modal.right .modal-dialog').addClass('pos_top')
                } else {
                    $('.modal.right .modal-dialog').removeClass('pos_top')
                }
            })
        });

        /*-------TOOLTIP---------*/
        new jBox('Tooltip', {
            attach: '.tooltip-offset',
            getContent: 'data-jbox-content',
            width: 440,
            offset: {
                y: -10
            }
        });

        /*-------TEXT IN MODAL---------*/
        $("a").click(function () {
            if ($(this).hasClass("show-demo-txt")) {
                $("#message-demo").removeClass('hidden');
                $("#message-order").addClass('hidden');
            }
            else if ($(this).hasClass("show-order-txt")) {
                $("#message-demo").addClass('hidden');
                $("#message-order").removeClass('hidden');
            }
            else {
                $("#message-demo").addClass('hidden');
                $("#message-order").addClass('hidden');
            }
        });
        /*-------underline---------*/
        $('ul.nav a').each(function () {
            if (this.href == location.href) $(this).parent().addClass('current');
        });
        /*-------NAVIGATION---------*/
        $(document).ready(function () {
            $('ul.nav a').each(function () {
                if (this.href == location.href) $(this).parent().addClass('current');
            });
        });
        /*-------END---------*/
    });
    /*-------INPUT PHONE MASKED---------*/
    function SetMask() {
        $("input[type=tel]").mask("+7 (999) 9999999");
        $("input[name=time]").mask("99:99", {
            placeholder: '--'
        });
    };
})(jQuery);
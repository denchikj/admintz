$(function () {	
    $('.datetimepicker').datetimepicker({
        format: 'DD.MM.YYYY',
        locale: 'ru',
        icons: {
            date: "fa fa-calendar",
            up: "fa fa-angle-up",
            down: "fa fa-angle-down",
            previous: "fa fa-angle-left",
            next: "fa fa-angle-right"
        }
    });
	$('input.dateYear').datetimepicker({		
        format: 'YYYY',			
        locale: 'ru',		
        icons: {
            date: "fa fa-calendar",
            up: "fa fa-angle-up",
            down: "fa fa-angle-down",
            previous: "fa fa-angle-left",
            next: "fa fa-angle-right"
        },
		useCurrent: false
		
    });	
	$('input.dateMonthYear').datetimepicker({
		viewMode: 'years',
        format: 'MM.YYYY',			
        locale: 'ru',		
        icons: {
            date: "fa fa-calendar",
            up: "fa fa-angle-up",
            down: "fa fa-angle-down",
            previous: "fa fa-angle-left",
            next: "fa fa-angle-right"
        },
		useCurrent: false		
    });	
});
$('body').on('click','.dateYear',function(){	
	$(this).on('dp.show', function (e) {
        $(this).data('DateTimePicker').viewMode('years');
    });
});
$('body').on('click','.dateMonthYear',function(){	
	$(this).on('dp.show', function (e) {
        $(this).data('DateTimePicker').viewMode('years');
    });
});

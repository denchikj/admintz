function DateTime() {
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
}
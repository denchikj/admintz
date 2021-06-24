$('#AccoutsTable').DataTable(
    {
        "fixedHeader": true,
        "language": {
            "decimal": "",
            "emptyTable": "Нет данных",
            "info": "Показано c _START_ по _END_ из _TOTAL_",
            "infoEmpty": "Показано c 0 по 0 из 0",
            "infoFiltered": "(отфильтровано из _MAX_ записей)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "_MENU_",
            "loadingRecords": "Загрузка...",
            "processing": "В процессе...",
            "search": "",
            "searchPlaceholder": "Фильтр",
            "zeroRecords": "Совпадений не найдено",
            "paginate": {
                "first": "Первая",
                "last": "Последняя",
                "next": "Следующая",
                "previous": "Предыдущая"
            },
            "aria": {
                "sortAscending": ": сортировка по возрастанию",
                "sortDescending": ": сортировка по убыванию"
            }


        },
        "dom": '<"row panel no-back" <"col-xs-12 col-lg-3 col-md-4" l><"col-xs-12 col-lg-6 col-md-4" <"#InsertButtons">><"col-xs-12 col-lg-3 col-md-4" f>>t' +
        '<"row"<"col-xs-12 col-md-3"i><"col-xs-12 col-md-9"p>>',
        "lengthMenu": [[10, 25, 50, -1], ["Показывать по 10", "Показывать по 25", "Показывать по 50", "Показывать все"]]
    }
);
var table = $('#AccoutsTable').DataTable();
new $.fn.dataTable.Buttons(table, {
    buttons: [
        {
            extend: 'copyHtml5', text: '<i class="fa fa-files-o" ></i>', titleAttr: 'Копировать', className: 'btn btn-sm'
        },
        {
            extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i>', titleAttr: 'Копировать в Excel', className: 'btn btn-sm'
        }
    ]
});
table.buttons().container().appendTo($('#InsertButtons'));
$('.dataTables_filter input[type = "search"]').addClass('cv no-border');
$('.dataTables_length select').addClass('selectpicker no-back');
$('.dataTables_length select').selectpicker('refresh');

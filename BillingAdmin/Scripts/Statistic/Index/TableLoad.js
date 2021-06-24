function TableLoad() {
    $('#StatisticTable').DataTable(
        {
            "order": [[1, "desc"]],
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
            "lengthMenu": [[25, 50, 100, -1], ["Показывать по 25", "Показывать по 50", "Показывать по 100", "Показывать все"]]
        }
    );
    var table = $('#StatisticTable').DataTable();
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
}

function TableLoadGrop() {
    $('#RecordStatistic').DataTable(
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
                "searchPlaceholder": "Поиск в таблице",
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
            "order": [[2, 'asc']],
            "rowGroup": {
                "dataSrc": 1
            },
            "dom": '<"row panel no-back" <"col-xs-12 col-lg-3 col-md-3" l>' +
                '<"col-xs-12 col-lg-2 col-md-1" <"#InsertButtons">>' +
                '<"col-xs-12 col-lg-2 col-md-2" <"#InsertFilterListTalk">>' +
                '<"col-xs-12 col-lg-2 col-md-2" <"#InsertFilterTalk">>' +
                '<"col-xs-12 col-lg-3 col-md-4" f>>t' +
                '<"row"<"col-xs-12 col-md-3"i><"col-xs-12 col-md-9"p>>',
            "lengthMenu": [[25, 50, 100, -1], ["Показывать по 25", "Показывать по 50", "Показывать по 100", "Показывать все"]]
        }
    );
    var table = $('#RecordStatistic').DataTable();
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
    $.fn.dataTableExt.afnFiltering.push(
        function (oSettings, aData, iDataIndex) {
            var valueFilter = $(`#talkFilter`).val();
            var dataFilter = aData[6];
            var match = true;
            talkFilter(valueFilter, dataFilter);

            function talkFilter(searchValue, rowValue) {
                if (searchValue) {
                    var compareChar = searchValue.charAt(0);
                    var compareValue = parseFloat(searchValue.substr(1, searchValue.length - 1));
                    rowValue = $(rowValue).text() ? $(rowValue).text() : rowValue;
                    match = false;
                    switch (compareChar) {
                        case '<':
                            if (compareValue > rowValue) match = true;
                            break;
                        case '>':
                            if (compareValue < rowValue) match = true;
                            break;
                        case '=':
                            if (compareValue == rowValue) match = true;
                            break;
                    }
                }
            }
            return match;
        }
    );
    var inputFilter = "<input type='text' class='cv no-border' id='talkFilter' name='talkFilter' placeholder='Фильтр по времени разговора' title='знак сравнения можно ввести в это поле'/>";
    var dropdownFilter = "<select id='ListMark' class='selectpicker form-control no-back'>" +
        "<option value='='> Равно </option>" +
        "<option value='>'> Больше </option>" +
        "<option value='<'> Меньше </option>" +
        "</select>";
    $(`#InsertFilterListTalk`).html(`${dropdownFilter}`);
    $(`#InsertFilterTalk`).html(`${inputFilter}`);
    $(`#talkFilter`).keyup(function () {
        this.value=CheckMark(this);
        OnlyNumAndComp(this);
        table.draw();
    });
    table.buttons().container().appendTo($('#InsertButtons'));
    $('.dataTables_filter input[type = "search"]').addClass('cv no-border');
    $('.dataTables_length select').addClass('selectpicker no-back');
    $('.dataTables_length select').selectpicker('refresh');
    
    $('.selectpicker').selectpicker();
}
function OnlyNumAndComp(elem) {
    var inputN = elem;
    inputN.value = inputN.value.match(/^[>=<]?\d*$/g);    
}
function CheckMark(elem) {
    var inputValue = elem.value;
    if ($.isNumeric(inputValue.charAt(0))){
        var mark = $(`#ListMark`).val();
        var val= mark + inputValue;
        return val;
    }
    return inputValue;
}

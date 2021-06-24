function check() {

    var f1 = check_form(); //проверяем, все ли поля заполнены в анкете
    var f2 = true;

    //если резюме заполняем на сайте
    var file = document.getElementById('q27_1'); //резюме не прикреплено
    if (file.value == '') {
        f2 = check_cv(); //проверяем поля в резюме
    }
    else {
        f2 = true;
    }

    if (f1 & f2) {
        document.getElementById('message').classList.add("hidden");
        location.href = '#';
    }
}

/*Проверка в поле резюме*/
function check_form() {
    var f = true; //поля заполнены
    var fields = document.querySelectorAll('.form[required]');
    fields.forEach(function (inp) {
        if (inp.value == '') {
            inp.classList.add('data-error');
            f = false;
            document.getElementById('message').classList.remove("hidden");
        }
        else inp.classList.remove("data-error");
    });
    return f;
}

/*Проверка в поле резюме*/
function check_cv() {
    var f = true;
    var fields = document.querySelectorAll('.cv[required]');
    fields.forEach(function (inp) {
        if (inp.value == '') {
            inp.classList.add('data-error');
            f = false;
            document.getElementById('message').classList.remove("hidden");
        }
        else inp.classList.remove("data-error");
    });

    var z1 = document.getElementsByName('cv1'), s1 = false;
    for (var i = 0; i < z1.length; i++) {
        if (z1[i].checked) {
            s1 = true;
            break;
        }
        else document.getElementById('message').classList.remove("hidden");
    }

    var z2 = document.getElementsByName('cv2'), s2 = false;
    for (var i = 0; i < z2.length; i++) {
        if (z2[i].checked) {
            s2 = true;
            break;
        }
        else document.getElementById('message').classList.remove("hidden");
    }

    var z4 = document.getElementsByName('cv4'), s4 = false;
    for (var i = 0; i < z4.length; i++) {
        if (z4[i].checked) {
            s4 = true;
            break;
        }
        else document.getElementById('message').classList.remove("hidden");
    }

    var z5 = document.getElementsByName('cv5'), s5 = false;
    for (var i = 0; i < z5.length; i++) {
        if (z5[i].checked) {
            s5 = true;
            break;
        }
        else document.getElementById('message').classList.remove("hidden");
    }

    return s1 && s2 && s4 && s5 && f;
}

/*-------CHECK FOR RANGE---------*/
var arr = [];
function checkNum(obj, i) {

    var max = 10;
    var min = 1;
    if (obj.value > max) obj.value = max;
    if (obj.value < min) obj.value = min;

    arr[i] = obj.value;

    for (j = 0; j < i; j++) {
        if ((arr[j] == obj.value) && (j != i)) {
            obj.value = '';
        }
    }
}

/*Добавление строк к таблицам*/
$(document).ready(function () {
    var i = 2;
    $("#add_row_cv2").click(function () {
        $('#table-cv2').append('<tr id="tr-cv2-' + i + '"></tr>');
        $('#tr-cv2-' + i).html(
		"<td><input type='text' name='cv2_from' id='cv2_" + i + "_1_from' class='dateYear no-border text-center' placeholder='гггг' size='4'>" +
		"<span>-</span>" +
		"<input type='text' name='cv2_to' id='cv2_" + i + "_1_to' class='dateYear no-border text-center' placeholder='гггг' size='4'></td>" +
		"<td><input type='text' name='cv2' id='cv2_" + i + "_2' class='no-border cv'></td>" +
		"<td><input type='text' name='cv2' id='cv2_" + i + "_3' class='no-border cv'></td>" +
		"<td><input type='text' name='cv2' id='cv2_" + i + "_4' class='no-border cv'></td>" +
		"<td><input type='text' name='cv2' id='cv2_" + i + "_5' class='no-border cv'></td>"
		);
        i++;
        $('input.dateYear').datetimepicker({
            viewMode: 'years',
            format: 'YYYY',
            locale: 'ru',
            icons: {
                date: "fa fa-calendar",
                up: "fa fa-angle-up",
                down: "fa fa-angle-down",
                previous: "fa fa-angle-left",
                next: "fa fa-angle-right"
            }
        });
    });
});
$(document).ready(function () {
    var i = 2;
    $("#add_row_cv3").click(function () {
        $('#table-cv3').append('<tr id="tr-cv3-' + i + '"></tr>');
        $('#tr-cv3-' + i).html(
		"<td><input type='text' name='cv3_from' id='cv3_" + i + "_1_from' class='dateYear no-border text-center' placeholder='гггг' size='4'>" +
		"<span>-</span>" +
		"<input type='text' name='cv3_to' id='cv3_" + i + "_1_to' class='dateYear no-border text-center' placeholder='гггг' size='4'></td>" +
		"<td><input type='text' name='cv3' id='cv3_" + i + "_2' class='no-border cv'></td>" +
		"<td><input type='text' name='cv3' id='cv3_" + i + "_3' class='no-border cv'></td>" +
		"<td><input type='text' name='cv3' id='cv3_" + i + "_4' class='no-border cv'></td>"
		);
        i++;
        $('input.dateYear').datetimepicker({
            format: 'YYYY',
            locale: 'ru',
            icons: {
                date: "fa fa-calendar",
                up: "fa fa-angle-up",
                down: "fa fa-angle-down",
                previous: "fa fa-angle-left",
                next: "fa fa-angle-right"
            }
        });
    });
});
$(document).ready(function () {
    var i = 2;
    $("#add_row_cv7").click(function () {
        $('#table-cv7').append('<tr id="tr-cv7-' + i + '"></tr>');
        $('#tr-cv7-' + i).html(
		"<td><input type='text' name='cv7_from' id='cv7_" + i + "_1_from' class='dateMonthYear no-border text-center' placeholder='мм.гггг' size='7'>" +
		"<span>-</span>" +
		"<input type='text' name='cv7_to' id='cv7_" + i + "_1_to' class='dateMonthYear no-border text-center' placeholder='мм.гггг' size='7'></td>" +
		"<td><input type='text' name='cv7' id='cv7_" + i + "_2' class='no-border cv'></td>" +
		"<td><input type='text' name='cv7' id='cv7_" + i + "_3' class='no-border cv'></td>" +
		"<td><input type='text' name='cv7' id='cv7_" + i + "_4' class='no-border cv'></td>"
		);
        i++;
        $('input.dateMonthYear').datetimepicker({
            format: 'MM.YYYY',
            locale: 'ru',
            icons: {
                date: "fa fa-calendar",
                up: "fa fa-angle-up",
                down: "fa fa-angle-down",
                previous: "fa fa-angle-left",
                next: "fa fa-angle-right"
            }
        });
    });
});

$(document).ready(function () {
    /*Показать имя загружаемого файла (резюме)*/
    $(".file_upload input[type=file]").change(function () {
        var obj = $("#span_file_name");
        obj.parent().removeClass('hidden');
        var filename = $(this).val().replace(/.*\\/, "");
        if (filename == "") obj.html("Резюме не прикреплено");
        else obj.html('Прикреплено резюме: ' + filename);
    });
    /*Вычислить возраст*/
    $('#q2_1').on('dp.change', function () {
        var str = $(this).val();
        var strList = str.split(".");

        var cDate = new Date();
        var cDay = cDate.getDate();
        var cMonth = cDate.getMonth() + 1;
        var cYear = cDate.getFullYear();

        var age = cYear - strList[2];

        if (strList[1] > cMonth) {
            age = parseInt(age) - 1;
        }
        else if (strList[1] == cMonth) {
            if (strList[0] > cDay) {
                age = parseInt(age) - 1;
            }
        }
        $('#q3_1').val(age);
        if (this.value == '') $('#q3_1').val('');
    });
    /*Задать подпись к кнопке*/
    function windowSize() {
        if ($(window).width() <= '768') {
            $("#file_name").val('Прикрепить');
        }
        else {
            $("#file_name").val('Прикрепить резюме');
        }
    };
    windowSize();
    $(window).resize(windowSize);
});
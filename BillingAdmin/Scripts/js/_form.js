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
/*Добавление строк к таблицам*/
$(document).ready(function () {
    var i = 2;
    $("#add_row_cv2").click(function () {
        $('#tr-cv2-' + i).html("<td><input type='text' name='cv2' id='cv2_" + i + "_1' class='no-border'></td><td><input type='text' name='cv2' id='cv2_" + i + "_2' class='no-border'></td><td><input type='text' name='cv2' id='cv2_" + i + "_3' class='no-border'></td><td><input type='text' name='cv2' id='cv2_" + i + "_4' class='no-border'></td><td><input type='text' name='cv2' id='cv2_" + i + "_5' class='no-border'></td>");
        $('#table-cv2').append('<tr id="tr-cv2-' + (i + 1) + '"></tr>');
        i++;
    });
});
$(document).ready(function () {
    var i = 2;
    $("#add_row_cv3").click(function () {
        $('#tr-cv3-' + i).html("<td><input type='text' name='cv3' id='cv3_" + i + "_1' class='no-border'></td><td><input type='text' name='cv3' id='cv3_" + i + "_2' class='no-border'></td><td><input type='text' name='cv3' id='cv3_" + i + "_3' class='no-border'></td><td><input type='text' name='cv3' id='cv3_" + i + "_4' class='no-border'></td>");
        $('#table-cv3').append('<tr id="tr-cv3-' + (i + 1) + '"></tr>');
        i++;
    });
});
$(document).ready(function () {
    var i = 2;
    $("#add_row_cv7").click(function () {
        $('#tr-cv7-' + i).html("<td><input type='text' name='cv7' id='cv7_" + i + "_1' class='no-border'></td><td><input type='text' name='cv7' id='cv7_" + i + "_2' class='no-border'></td><td><input type='text' name='cv7' id='cv7_" + i + "_3' class='no-border'></td><td><input type='text' name='cv7' id='cv7_" + i + "_4' class='no-border'></td>");
        $('#table-cv7').append('<tr id="tr-cv7-' + (i + 1) + '"></tr>');
        i++;
    });
});
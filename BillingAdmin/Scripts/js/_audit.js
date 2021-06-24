function check() {
    var f = true;
    var fields = document.querySelectorAll('input.data-form[required]');
    fields.forEach(function (inp) {
        if (inp.value == '') {
            inp.classList.add('data-error');
            f = false;
        }
        else inp.classList.remove("data-error");
    });

    if (f == true) location.href = "_audit_result.html";
}
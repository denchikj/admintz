var selector = 'input:not([readonly])[class*="comment"]';
$('table')
    .on('change', selector, EditComment)
    .on('paste', selector, EditComment)
    .on('keyup', selector, EditComment);

function EditComment(e) {
    var value = e.target.value;
    $(`input[id=${e.target.id}]`).each(function () {
        $(this).val(value);
    });
}

$('table').on("click",
    ".fa-pencil-square-o",
    function (e) {
        var callId = e.target.id;
        $(`i[id=${callId}]`).each(function () {
            $(this).toggleClass("fa-pencil-square-o").toggleClass("fa-floppy-o");
            $(`input[id=${callId}]`).each(function () {
                $(this).attr("readonly", false);
            });
        });
    });
$('table').on('click', '.fa-floppy-o', function (e) {
    var callId = e.target.id;
    var comment = $(`input[id=${callId}]`).val();
    $.ajax({
        url: "/Statistic/SaveCommentClient",
        type: "POST",
        data: {
            callId: callId,
            comment: comment
        },
        statusCode: {
            201: function () {
                $(`i[id=${callId}]`).each(function () {
                    $(this).toggleClass("fa-floppy-o").toggleClass("fa-pencil-square-o");
                    $(`input[id=${callId}]`).each(function () {
                        $(this).attr("readonly", true);
                    });
                });
            }
        }
    });
});
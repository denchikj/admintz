function FormData() {
    var editProject = {
        Id: $('#projectId').val(),
        Name: $('input[name="Name"]').val(),
        AccountId: $('#AccountId').val(),
        AccountName: $('#AccountName').val(),
        OktelCode: $('input[name="OktelCode"]').val(),
        TypeProjectId: $('#TypeProjectId').val(),
        Statistic: []
    };
    $('.Statistic').each(function () {
        var item = {
            Id: $(this).attr('id'),
            IdSettings: $(this).attr('name'),            
            StatisticStatus: $(this).prop('checked')
        };
        editProject.Statistic.push(item);
    });
    return {
        token: $('#project input[name="__RequestVerificationToken"]').val(),
        method: $('#project').attr('method'),
        action: $('#project').attr('action'),
        newProject: editProject
    };
}
$('body').on('submit', '#project', function (e) {
    e.preventDefault();
    var data = FormData();
    var ddd;
    $.ajax({
        url: data.action,
        type: data.method,
        data: {
            __RequestVerificationToken: data.token,
            model: data.newProject
        }
    })
        .then(function (data) {
            if (data.text) {
                alert(data.text);
            } else {
                document.location = data.url;
            }
        });
    return 0;
});
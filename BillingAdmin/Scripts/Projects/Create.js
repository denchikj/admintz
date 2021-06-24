function FormData() {
    var newProject = {
        Name: $('input[name="Name"]').val(),
        AccountId: $('input[name="AccountId"]').val(),
        AccountName: $('input[name="AccountName"]').val(),
        OktelCode: $('input[name="OktelCode"]').val(),
        TypeProjectId: $('#TypeProjectId').val(),
        Statistic: []        
    };
    $('.Statistic').each(function () {
        var item = {
            IdSettings: $(this).attr('name'),
            StatisticStatus: $(this).prop('checked')
        };
        newProject.Statistic.push(item);
    });    
    return {
        token: $('#project input[name="__RequestVerificationToken"]').val(),
        method: $('#project').attr('method'),
        action: $('#project').attr('action'),
        newProject: newProject
    };
}

$('body').on('submit', '#project', function (e) {
    e.preventDefault();
    var data = FormData();        
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
function RequestPreference(id) {
    $.ajax({
        type: "POST",
        url: "/Projects/Preference/",
        data: {
            id: id
        },
        success: function (html) {
            $('#preferenceService').replaceWith(html);
        }
    });
}
function RequestAddService(data) {
    $.ajax({
        url: "/Projects/AddService/",
        type: 'POST',
        data: data,
        success: function () {
            RefreshService(data.ProjectId);
            alert("Услуга успешно добавлена!");
        },
        error: function () {
            alert("Произошла ошибка!");
        }
    });
}
function RefreshService(id) {
    $.ajax({
        url: "/Projects/ServiceOfProject/" + id,
        type: 'GET',      
        success: function (html) {
            $('#serviceOfProject').replaceWith(html);           
        }
    });
}
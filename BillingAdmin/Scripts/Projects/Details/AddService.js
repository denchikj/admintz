$('body').on('submit', '#AddService', function (e) {
    e.preventDefault();
    var data = FormAddService();
    RequestAddService(data);
});
function FormAddService() {
    var ProjectId = $('#AddService').find('input[name="ProjectId"]').val();
    var ServiceId = $('#AddService').find('option:selected').val();
    var Preferences = [];
    $('.PreferenceOfService').each(function () {
        var item = {
            Key: $(this).attr('name'),
            Value: $(this).val()
        };
        Preferences.push(item);
    });
    return {
        ProjectId: ProjectId,
        ServiceId: ServiceId,
        Preferences: Preferences
    };
}
$('body').on('change', '#selectService', function () {
    var serviceId = $(this).val();
    RequestPreference(serviceId);
});
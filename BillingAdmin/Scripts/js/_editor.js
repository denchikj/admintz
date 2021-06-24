function gettoken() {
    var token = '<input name="__RequestVerificationToken" type="hidden" value="ZyAe5lf2yGFQlWqGeUlyZibsSaeXZOLl03FjMfhYPkVN7NXqroU_aKEn5dfWclLbK4_vyNjmT6JWgLB9zLjpLv-pBTTug5KfWQLs7MlNbP_mK4-L8CisyXkcLPL42Sq6AQkTIjiJvRDtPqlEIUSIVA2" />';
    token = $(token).val();
    return token;
}

$(document).ready(function () {
    // Handler for .ready() called.
    $.fn.editable.defaults.mode = 'inline';

    $('#name').editable({
        url: '/Projects/Edit/1',
        type: 'text',
        pk: 1,
        name: 'name',
        title: 'Введите название проекта',
        placement: 'bottom',

        params: function (params) {  //params already contain `name`, `value` and `pk`
            var data = {};
            data['id'] = 1;
            data['accountId'] = 1;
            data[params.name] = params.value;
            data['__RequestVerificationToken'] = gettoken();
            return data;
        }
    });

});

function saveInfo() {
    $('#btnSave').removeClass();
    $('#btnSave').addClass("btn btn-success loading");

    $.ajax({
        url: '/Projects/UpdateProject',
        type: 'POST',
        data: $("#frmEditService").serialize(),
        success: function (data) {
            /*if (data) {
                $(':input', '#frmAddService')
                    .not(':button, :submit, :reset, :hidden')
                    .val('')
                    .removeAttr('checked')
                    .removeAttr('selected');
            }*/
            //window.location = window.location.pathname;
            // alert($("#frmAddService").serialize());
            $('#btnSave').removeClass();
            $('#btnSave').addClass("btn btn-default");
            $('#btnSave').blur();
        }
    });
}

$(document).ready(function () {
    var url = document.location.toString();
    if (url.match('#')) {
        $('#' + url.split('#')[1]).addClass('in');
        var cPanelBody = $('#' + url.split('#')[1]);
        var cPanelHeading = cPanelBody.prev();


        cPanelHeading.find(".panel-title a").removeClass('collapsed');            
    }
    $("html,body").animate({ scrollTop: 0 }, 1);
    //window.scrollTo(0, 0);
});




$("#modalPopup").dialog({
    autoOpen: false,
    height: 400,
    width: 600,
    modal: true,
    buttons: {
        "Сохранить": function () {
            saveNewService();
            // $(".ui-dialog-titlebar-close").trigger('click');
        },
        "Отмена": function () {
            $(".ui-dialog-titlebar-close").trigger('click');
        }
    }
    ,
    open: function (event, ui) {
        $(this).load($("#modalPopup").dialog("option","url"));

           
    }
});

$("#modalPay").dialog({
    autoOpen: false,
    height: 400,
    width: 600,
    modal: true,
    open: function (event, ui) {
        $(this).load($("#modalPay").dialog("option", "url"));
    }
});

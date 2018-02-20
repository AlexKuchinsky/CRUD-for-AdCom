function AddApplication() {
    $('.en-selected_specialities').on('click', '.en-js-app_add_button', function () {

    });
}

//$(document).ready(function {
//    var mainApp = $('.en-selected_specialities').find('.en-application').first()[0];
//    var mainGroup = mainApp ? mainApp.dataset.group : null;

//    var onSuccess = function () {

//    }

//    var onError = function () {
//        alert('Error check friendly group' + errorData.responseText);
//    }

//    $.ajax({
//        url: '/Admin/CheckFriendlyGroups',
//        method: 'post',
//        contentType: 'application/json',
//        data: JSON.stringify({
//            'mainGroup': mainGroup
//        }),
//        dataType: 'json',
//        success: onSuccess,
//        error: onError
//    });
//})

$(document).ready(function () {
    //delete app
    $('.en-selected_applications').on('click', '.en-js-app_del_button', function (eventObj) {
        if (confirm('Are you sure?')) {
            var appId = $(eventObj.target).parents('.en-application').first()[0].dataset.id;

            var onSuccess = function (data) {
                if (data) {
                    $(eventObj.target).parents('.en-application').first().remove();
                    return;
                }
                alert('Error deleting application');
            }

            var onError = function () {
                alert('Error deleting application' + errorData.responseText);
                return;
            }

            $.ajax({
                url: '/Admin/DeleteApplication',
                method: 'post',
                contentType: 'application/json',
                data: JSON.stringify({
                    'applicationId': appId
                }),
                dataType: 'json',
                success: onSuccess,
                error: onError
            });
        }
    });

    //send e-mail
    $(document.body).on('click', '.en-js-send_email', function () {
        var onSuccess = function (data) {
            if (data) {
                alert('Success');
            }
            else alert('Oops, error sending email!');
        }

        var onError = function () {
            alert('Error sending email' + errorData.responseText);
            return;
        }

        $('.en-js-send_email').prop('disabled', true);
        $.ajax({
            url: '/Admin/SendEmail',
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify({
                'toEmail': 'glotovartemalex@gmail.com'
            }),
            dataType: 'json',
            success: onSuccess,
            
            error: onError
        });
    });

    //$('.en-selected_applications').on('click', '.en-js-app_loadpdf_button', function (eventObj) {
    //    var appId = $(eventObj.target).parents('.en-application').first()[0].dataset.id;

    //    var onError = function () {
    //        alert('Error load pdf application' + errorData.responseText);
    //        return;
    //    }

    //    $.ajax({
    //        url: '/Admin/LoadPDF',
    //        method: 'post',
    //        contentType: 'application/json',
    //        data: JSON.stringify({
    //            'applicationId': appId
    //        }),
    //        dataType: 'pdf',
    //        success: onSuccess,
    //        error: onError
    //    });
    //});


});
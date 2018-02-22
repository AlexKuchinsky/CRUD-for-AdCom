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

var convert = {
            'А': 'A',
            'Ф': 'A',
            'F': 'A',
            'И': 'B',
            'В': 'B',
            'D': 'B',
            'Ь': 'M',
            'М': 'M',
            'V': 'M',
            'Y': 'H',
            'Н': 'H',
            'R': 'K',
            'К': 'K',
            'Л': 'K',
            'Р': 'P',
            'З': 'P',
            'С': 'C',
            'Щ': 'O',
            'О': 'O',
            'J': 'O',
            'Ш': 'I'
        }

    $('.en-js-passport')[0].oninput = function (event) {
        var inputChar = event.data.toUpperCase()
        //manipulation with char
        var resultChar = inputChar in convert ? convert[inputChar] : inputChar;
        event.target.value = event.target.value.substring(0, event.target.value.length - 1) + resultChar;
    };

    //$('.en-js-passport')[0].onkeypress = function (e) {
    //    // спец. сочетание - не обрабатываем
    //    if (e.ctrlKey || e.altKey || e.metaKey) return;

    //    var char = getChar(e);

    //    if (!char) return; // спец. символ - не обрабатываем

    //    this.value = char.toUpperCase();

    //    return false;
    //};


});
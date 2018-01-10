function tree(id, url) {
    var element = document.getElementById(id);

    function hasClass(elem, className) {
        return new RegExp("(^|\\s)" + className + "(\\s|$)").test(elem.className);
    }

    function toggleNode(node) {
        var newClass = hasClass(node, 'ExpandOpen') ? 'ExpandClosed' : 'ExpandOpen';
        var re = /(^|\s)(ExpandOpen|ExpandClosed)(\s|$)/;
        node.className = node.className.replace(re, '$1' + newClass + '$3');
    }

    function load(node) {
        function showLoading(on) {
            var expand = node.getElementsByTagName('DIV')[0];
            expand.className = on ? 'ExpandLoading' : 'Expand';
        }

        function onSuccess(data) {
            if (!data.errcode) {
                onLoaded(data);
                showLoading(false);
            } else {
                showLoading(false);
                onLoadError(data);
            }
        }

        function onAjaxError(xhr, status) {
            showLoading(false);
            var errinfo = { errcode: status };
            if (xhr.status !== 200) {
                errinfo.message = xhr.statusText;
            } else {
                errinfo.message = 'Некорректные данные с сервера';
            }
            onLoadError(errinfo);
        }

        function onLoaded(data) {
            for (var i = 0; i < data.length; i++) {
                var child = data[i];
                var li = document.createElement('li');
                li.id = child.Id;

                li.className = "Node Expand" + (child.isFolder ? 'Closed' : 'Leaf');
                if (i === data.length - 1) li.className += ' IsLast';

                li.innerHTML = '<div class="Expand"></div><div class="Content">' + child.Title + '</div>';
                if (child.isFolder) {
                    li.innerHTML += '<ul class="Container"></ul>';
                }
                node.getElementsByTagName('ul')[0].appendChild(li);
            }
            node.isLoaded = true;
            toggleNode(node);
        }

        function onLoadError(error) {
            var msg = "Ошибка " + error.errcode;
            if (error.message) msg = msg + ' :' + error.message;
            alert(msg);
        }

        showLoading(true);

        $.ajax({
            url: url,
            method: "post",
            contentType: "application/json",
            data: JSON.stringify({
                "id": node.id
            }),
            dataType: "json",
            success: onSuccess,
            error: onAjaxError,
            cache: false
        });
    }

    element.onclick = function (event) {
        event = event || window.event;
        var clickedElem = event.target || event.srcElement;

        if (!hasClass(clickedElem, 'Expand')) {
            return;
        }

        var node = clickedElem.parentNode;
        if (hasClass(node, 'ExpandLeaf')) {
            return;
        }

        if (node.isLoaded || node.getElementsByTagName('li').length) {
            toggleNode(node);
            return;
        }


        if (node.getElementsByTagName('LI').length) {
            toggleNode(node);
            return;
        }

        load(node);
    };
}

function universitySpecialties()
{
    var paymentDropdown = ajaxDropdown("payment1");
    var facultyDropdown = ajaxDropdown("faculty1");
    var specialtyDropdown = ajaxDropdown("specialty1");

    var paymentElement = document.getElementById("payment1");
    var facultyElement = document.getElementById("faculty1");

    paymentDropdown.loadPayment("/Admin/LoadPayment");
    paymentElement.onchange = function () {
        facultyDropdown.loadFaculties("/Admin/LoadFaculties", paymentElement.value);
        specialtyDropdown.clear();
    }
    facultyElement.onchange = function () {
        specialtyDropdown.loadSpecialties("/Admin/LoadSpecialties", paymentElement.value, facultyElement.value);
    };
}

//$(function () {
//    $(".en-js-university_main").on("click", ".en-js-university_each", function () {
//        var paymentElement = $(this).find(".payment");
//        var facultyElement = $(this).find(".faculty");
//        var specialtyElement = $(this).find(".specialty");
//        var facultyDropdown = ajaxDropdown(facultyElement);
//        var specialtyDropdown = ajaxDropdown(specialtyElement);
//        //var facultyElement = document.getElementById("faculty1");
//        paymentElement.onchange = function () {
//            var facultyElement = $(this).find(".faculty");
//            var paymentElement = $(this).find(".payment");
//            var facultyDropdown = ajaxDropdown(facultyElement);
//            facultyDropdown.loadFaculties("/Admin/LoadFaculties", paymentElement.value);
//        }
//        facultyElement.onchange = function () {
//            specialtyDropdown.loadSpecialties("/Admin/LoadSpecialties", paymentElement.value, facultyElement.value);
//        };
//    });
//});

function ajaxDropdown(id) {
    var element = document.getElementById(id);

    var onLoaded = function (data) {
        element.options[0] = new Option("-Not selected-", null);
        for (var i = 0; i < data.length; i++) {
            var text = data[i].Text;
            var value = data[i].Value;
            element.options[i+1] = new Option(text, value);
            element.options[i + 1].title = data[i].Tooltip;
        }   
    };

    var onLoadError = function (error) {
        var msg = "Ошибка " + error.errcode;
        if (error.message) msg = msg + ' :' + error.message;
        alert(msg);
    };

    var showLoading = function (on) {
        element.disabled = on;
    };

    var onSuccess = function (data) {
        if (!data.errcode) {
            onLoaded(data);
            showLoading(false);
        } else {
            showLoading(false);
            onLoadError(data);
        }
    };

    var onAjaxError = function (xhr, status) {
        showLoading(false);
        var errinfo = { errcode: status };
        if (xhr.status !== 200) {
            errinfo.message = xhr.statusText;
        } else {
            errinfo.message = 'Некорректные данные с сервера';
        }
        onLoadError(errinfo);
    };

    return {
        clear: function () {
            while (element.firstChild) {
                element.removeChild(element.firstChild);
            }
        },

        loadPayment: function (url) {
            showLoading(true);
            while (element.firstChild) {
                element.removeChild(element.firstChild);
            };
            $.ajax({
                url: url,
                method: "post",
                dataType: "json",
                success: onSuccess,
                error: onAjaxError,
                cache: false
            });
        },

        loadFaculties: function (url, isPaid) {
            showLoading(true);

            while (element.firstChild) {
                element.removeChild(element.firstChild);
            };
            if (isPaid !== "null")
            {
                $.ajax({
                    url: url,
                    method: "post",
                    contentType: "application/json",
                    data: JSON.stringify({
                        "isPaid": isPaid
                    }),
                    dataType: "json",
                    success: onSuccess,
                    error: onAjaxError,
                    cache: false
                });
            }           
        },

        loadSpecialties: function (url, isPaid, idFac) {
            showLoading(true);

            while (element.firstChild) {
                element.removeChild(element.firstChild);
            }
            if ((idFac !== "null") && (isPaid !== "null"))
            {
                $.ajax({
                    url: url,
                    method: "post",
                    contentType: "application/json",
                    data: JSON.stringify({
                        "idFac": idFac,
                        "isPaid": isPaid
                    }),
                    dataType: "json",
                    success: onSuccess,
                    error: onAjaxError,
                    cache: false
                });
            }         
        }
    };
}



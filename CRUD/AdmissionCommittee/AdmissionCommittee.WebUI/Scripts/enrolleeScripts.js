$(function () {
    //$(".ul-dropfree div.drop").one('click', function () {
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Admin/LoadBrunch/',
    //        data: JSON.stringify({ parentname: this.data('name') }),
    //        success: function (data) {
    //            if (data.length > 0) {
    //                var ul = $('<ul/>');
    //                $.each(data, function (index, item) {
    //                    var li = $('<li/>', {
    //                        text: item.Name,
    //                        data: {
    //                            "name": item.Name,
    //                            "type": item.type 
    //                        }
    //                    });
    //                    ul.append(li);
    //                })
    //                this.append(ul);
    //            }
    //        }
    //    });
    //})
});

function tree(id, url) {
    var element = document.getElementById(id);

    function hasClass(elem, className) {
        return new RegExp("(^|\\s)" + className + "(\\s|$)").test(elem.className);
    }

    function toggleNode(node) {
        // определить новый класс для узла
        var newClass = hasClass(node, 'ExpandOpen') ? 'ExpandClosed' : 'ExpandOpen';
        // заменить текущий класс на newClass
        // регулярка находит отдельно стоящий open|close и меняет на newClass
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
                // может быть статус 200, а ошибка
                // из-за некорректного JSON
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
            return; // клик не там
        }

        // Node, на который кликнули
        var node = clickedElem.parentNode;
        if (hasClass(node, 'ExpandLeaf')) {
            return; // клик на листе
        }

        if (node.isLoaded || node.getElementsByTagName('li').length) {
            // Узел уже загружен через AJAX(возможно он пуст)
            toggleNode(node);
            return;
        }


        if (node.getElementsByTagName('LI').length) {
            // Узел не был загружен при помощи AJAX, но у него почему-то есть потомки
            // Например, эти узлы были в DOM дерева до вызова tree()
            // Как правило, это "структурные" узлы
            // ничего подгружать не надо
            toggleNode(node);
            return;
        }

        // загрузить узел
        load(node);
    };
}

function universitySpecialties()
{
    var facultyDropdown = ajaxDropdown("faculty1");
    var specialtyDropdown = ajaxDropdown("specialty1");
    var facultyElement = document.getElementById("faculty1");
    document.getElementById("payment1").onchange = function () {
        facultyDropdown.loadFaculties("/Admin/LoadFaculties", document.getElementById("payment1").value);
    }
    document.getElementById("faculty1").onchange = function () {
        specialtyDropdown.loadSpecialties("/Admin/LoadSpecialties", document.getElementById("payment1").value, document.getElementById("faculty1").value);
    };
}

function ajaxDropdown(id) {
    var element = document.getElementById(id);

    var onLoaded = function (data) {
        for (var i = 0; i < data.length; i++) {
            var text = data[i].Text;
            var value = data[i].Value;
            element.options[i] = new Option(text, value);
            //element.options[i].data("ispaid") = true;
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
        loadFaculties: function (url, isPaid) {
            showLoading(true);

            while (element.firstChild) {
                element.removeChild(element.firstChild);
            }

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
        },

        loadSpecialties: function (url, isPaid, idFac) {
            showLoading(true);

            while (element.firstChild) {
                element.removeChild(element.firstChild);
            }

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
    };
}



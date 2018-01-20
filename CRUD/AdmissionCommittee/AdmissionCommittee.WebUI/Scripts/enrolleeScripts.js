function tree(url) {
    var element = document.getElementsByClassName("en-js-tree")[0];
    element.isFirstLoading = true;
    function hasClass(elem, className) {
        return new RegExp("(^|\\s)" + className + "(\\s|$)").test(elem.className);
    }

    function toggleNode(node) {
        var newClass = hasClass(node, 'en-tree_expand_open') ? 'en-tree_expand_closed' : 'en-tree_expand_open';
        var re = /(^|\s)(en-tree_expand_open|en-tree_expand_closed)(\s|$)/;
        node.className = node.className.replace(re, '$1' + newClass + '$3');
    }

    function load(node) {
        function showLoading(on) {
            if (element.isFirstLoading) return;
            var expand = node.getElementsByTagName('div')[0];
            expand.className = on ? 'en-tree_expand_loading' : 'en-tree_expand';   
        }

        function onSuccess(data) {
            if (!data.errcode) {
                onLoaded(data);
                showLoading(false);
            } else {
                showLoading(false);
                onLoadError(data);
            }
            element.isFirstLoading = false;
        }

        function onAjaxError(xhr, status) {
            showLoading(false);
            var errinfo = { errcode: status };
            if (xhr.status !== 200) {
                errinfo.message = xhr.statusText;
            } else {
                errinfo.message = 'Incorrect data from server';
            }
            onLoadError(errinfo);
        }

        function onLoadError(error) {
            var msg = "Error: " + error.errcode;
            if (error.message) msg = msg + ' :' + error.message;
            alert(msg);
        }

        function onLoaded(data) {
            for (var i = 0; i < data.length; i++) {
                var child = data[i];

                //create <li>
                var li = document.createElement('li');
                li.setAttribute('data-id', child.Id);
                li.className = "en-tree_node en-tree_expand" + (child.NumChildren !== 0 ? '_closed' : '_leaf');
                if (element.isFirstLoading) {
                    li.className += ' en-tree_is_root';
                }
                if (i === data.length - 1) {
                    li.className += ' en-tree_is_last';
                }
                
                //create innerHTML for li
                var stringHTML = '<div class="en-tree_expand"></div>';
                if ($(node).children('input').length !== 0 && $(node).children('input')[0].checked === true) {
                    stringHTML += '<input type="checkbox" checked="true">';
                }  
                else {
                    stringHTML += '<input type="checkbox">';
                }
                stringHTML += '<div class="en-tree_content" Title="' + child.FullName + '">' + child.Name;
                if (child.NumChildren > 0) {
                    stringHTML += ' (' + child.NumChildren + ')';
                }
                stringHTML += '</div>';
                if (child.NumChildren > 0) {
                    stringHTML += '<ul class="en-tree_container"></ul>';
                }
                li.innerHTML = stringHTML;

                node.getElementsByTagName('ul')[0].appendChild(li);
            }
            if (element.isFirstLoading) return;
            node.isLoaded = true;
            toggleNode(node);
        }   

        showLoading(true);
        $.ajax({
            url: url,
            method: "post",
            contentType: "application/json",
            data: JSON.stringify({
                "id": node.dataset.id
            }),
            dataType: "json",
            success: onSuccess,
            error: onAjaxError,
            cache: false
        });
    }

    $(document).ready(function () {
        load(element);
    });

    function checkParent(checkbox) {
        var isLevelFull = checkSiblings(checkbox);
        var isSomeInLevel;

        if (isLevelFull === "some") {
            isSomeInLevel = true;
            isLevelFull = false;
        }
        else {
            isSomeInLevel = isLevelFull;
        }

        var isChecked = checkbox.checked;
        if ($(checkbox).parents('ul').first().parent().data('id') === 0) {
            return;
        }
        var parent_checkbox = $(checkbox).parents('ul').first().siblings('input')[0];

        if (isLevelFull && isChecked) {
            parent_checkbox.checked = true;
            parent_checkbox.indeterminate = false;
        }
        else if (isSomeInLevel || isChecked || checkbox.indeterminate) {
            parent_checkbox.checked = false;
            parent_checkbox.indeterminate = true;
        }
        else {
            parent_checkbox.checked = false;
            parent_checkbox.indeterminate = false;
        }

            checkParent(parent_checkbox); 
    }

    function checkSiblings(checkbox) {
        var lis = $(checkbox).parent().siblings('li');
        var numTrue = 0;
        for (i = 0; i < lis.length; i++) {          
            if ($(lis[i]).children('input')[0].checked === true) {
                numTrue++;         
            }         
        }
        if (numTrue === lis.length) {
            return true;
        }
        else if (numTrue === 0) {
            return false;
        }
        return "some";
    }

    function checkChild(checkbox) {
        var isChecked = checkbox.checked;
        var lis = $(checkbox).siblings('ul').first().children('li');
        for (var i = 0; i < lis.length; i++) {
            var child_checkbox = $(lis[i]).children('input')[0];
            child_checkbox.checked = isChecked;
            child_checkbox.indeterminate = false;
            checkChild(child_checkbox);
        }
    }

    function checkRelations(checkbox) {   
        checkParent(checkbox);
        checkChild(checkbox);
    }

    element.onclick = function (event) {
        event = event || window.event;
        var clickedElem = event.target || event.srcElement;

        if (clickedElem.type === "checkbox") {
            var isChecked = clickedElem.checked;
            checkRelations(clickedElem);
        }
        if (!hasClass(clickedElem, 'en-tree_expand')) {
            return;
        }
        var node = clickedElem.parentNode;
        if (hasClass(node, 'en-tree_expand_leaf')) {
            return;
        }
        if (node.isLoaded || node.getElementsByTagName('li').length) {
            toggleNode(node);
            return;
        }
        if (node.getElementsByTagName('li').length) {
            toggleNode(node);
            return;
        }
        load(node);
    };
}

function initFirst(firstDropdown, callback) {
    firstDropdown.loadNode("/Admin/LoadDropdown", null, callback);
}

function setRelations(parentDropdown, childDropdown, parentId) {
    parentDropdown.clearChild = function () {
        while (childDropdown.element.firstChild) {
            childDropdown.element.removeChild(childDropdown.element.firstChild);
        }
        if (childDropdown.clearChild) childDropdown.clearChild();
    };

    parentDropdown.element.onchange = function () {
        parentDropdown.clearChild();
        childDropdown.loadNode("/Admin/LoadDropdown", parentDropdown.element.value, null);       
    };
}

function onDropdownLoaded(parentDropdown, childDropdown, parentId, onChildLoaded) {
    parentDropdown.element.value = parentId;
    if (childDropdown) {
        childDropdown.loadNode("/Admin/LoadDropdown", parentId, onChildLoaded);
    }
}

function initSpecialityInfo(info)
{
    var university = ajaxDropdown(document.getElementById("university1"));
    var faculty= ajaxDropdown(document.getElementById("faculty1"));
    var specialty = ajaxDropdown(document.getElementById("specialty1"));
    var specialization = ajaxDropdown(document.getElementById("specialization1"));
    var formOfStudy = ajaxDropdown(document.getElementById("formOfStudy1"));
    var payment = ajaxDropdown(document.getElementById("payment1"));

    var onPaymentLoaded = function(){ onDropdownLoaded(payment, null, info.PaymentId, null) };
    var onFormOfStudyLoaded = function () { onDropdownLoaded(formOfStudy, payment, info.FormOfStudyId, onPaymentLoaded) };
    var onSpecializationLoaded = function () { onDropdownLoaded(specialization, formOfStudy, info.SpecializationId, onFormOfStudyLoaded) };
    var onSpecialtyLoaded = function () { onDropdownLoaded(specialty, specialization, info.SpecialtyId, onSpecializationLoaded) };
    var onFacultyLoaded = function () { onDropdownLoaded(faculty, specialty, info.FacultyId, onSpecialtyLoaded) };
    var onUniversityLoaded = function () { onDropdownLoaded(university, faculty, info.UniversityId, onFacultyLoaded) };

    setRelations(formOfStudy, payment);
    setRelations(specialization, formOfStudy);
    setRelations(specialty, specialization);
    setRelations(faculty, specialty);
    setRelations(university, faculty);

    initFirst(university, onUniversityLoaded);
}

function ajaxDropdown(element) {
    var thisCallback;
    var onLoaded = function (data) {
        element.options[0] = new Option("-Not selected-", -1);
        for (var i = 0; i < data.length; i++) {
            var text = data[i].Name;
            var value = data[i].NodeId;
            element.options[i + 1] = new Option(text, value);
            element.options[i + 1].title = data[i].FullName;
        }
        if (thisCallback) {
            thisCallback();
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
        element: element,
        loadNode: function (url, parentId, callback) {
            showLoading(true);
            thisCallback = callback;
            if (parentId !== -1)
            {
                $.ajax({
                    url: url,
                    method: "post",
                    contentType: "application/json",
                    data: JSON.stringify({
                        "parentId": parentId
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



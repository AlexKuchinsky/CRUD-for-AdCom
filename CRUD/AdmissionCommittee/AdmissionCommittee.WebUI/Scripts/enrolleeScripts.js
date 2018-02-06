//function tree(url) {
//    var element = document.getElementsByClassName("en-js-tree")[0];
//    element.isFirstLoading = true;
//    function hasClass(elem, className) {
//        return new RegExp("(^|\\s)" + className + "(\\s|$)").test(elem.className);
//    }

//    function toggleNode(node) {
//        var newClass = hasClass(node, 'en-tree_expand_open') ? 'en-tree_expand_closed' : 'en-tree_expand_open';
//        var re = /(^|\s)(en-tree_expand_open|en-tree_expand_closed)(\s|$)/;
//        node.className = node.className.replace(re, '$1' + newClass + '$3');
//    }

//    function load(node) {
//        function showLoading(on) {
//            if (element.isFirstLoading) return;
//            var expand = node.getElementsByTagName('div')[0];
//            expand.className = on ? 'en-tree_expand_loading' : 'en-tree_expand';   
//        }

//        function onSuccess(data) {
//            if (!data.errcode) {
//                onLoaded(data);
//                showLoading(false);
//            } else {
//                showLoading(false);
//                onLoadError(data);
//            }
//            element.isFirstLoading = false;
//        }

//        function onAjaxError(xhr, status) {
//            showLoading(false);
//            var errinfo = { errcode: status };
//            if (xhr.status !== 200) {
//                errinfo.message = xhr.statusText;
//            } else {
//                errinfo.message = 'Incorrect data from server';
//            }
//            onLoadError(errinfo);
//        }

//        function onLoadError(error) {
//            var msg = "Error: " + error.errcode;
//            if (error.message) msg = msg + ' :' + error.message;
//            alert(msg);
//        }

//        function onLoaded(data) {
//            for (var i = 0; i < data.length; i++) {
//                var child = data[i];

//                //create <li>
//                var li = document.createElement('li');
//                li.setAttribute('data-id', child.Id);
//                li.className = "en-tree_node en-tree_expand" + (child.NumChildren !== 0 ? '_closed' : '_leaf');
//                if (element.isFirstLoading) {
//                    li.className += ' en-tree_is_root';
//                }
//                if (i === data.length - 1) {
//                    li.className += ' en-tree_is_last';
//                }
                
//                //create innerHTML for li
//                var stringHTML = '<div class="en-tree_expand"></div>';
//                if ($(node).children('input').length !== 0 && $(node).children('input')[0].checked === true) {
//                    stringHTML += '<input type="checkbox" checked="true">';
//                }  
//                else {
//                    stringHTML += '<input type="checkbox">';
//                }
//                stringHTML += '<div class="en-tree_content" Title="' + child.FullName + '">' + child.Name;
//                if (child.NumChildren > 0) {
//                    stringHTML += ' (' + child.NumChildren + ')';
//                }
//                stringHTML += '</div>';
//                if (child.NumChildren > 0) {
//                    stringHTML += '<ul class="en-tree_container"></ul>';
//                }
//                li.innerHTML = stringHTML;

//                node.getElementsByTagName('ul')[0].appendChild(li);
//            }
//            if (element.isFirstLoading) return;
//            node.isLoaded = true;
//            toggleNode(node);
//        }   

//        showLoading(true);
//        $.ajax({
//            url: url,
//            method: "post",
//            contentType: "application/json",
//            data: JSON.stringify({
//                "id": node.dataset.id
//            }),
//            dataType: "json",
//            success: onSuccess,
//            error: onAjaxError,
//            cache: false
//        });
//    }

//    load(element);

//    function checkParent(checkbox) {
//        if ($(checkbox).parents('ul').first().parent().data('id') === null) {
//            return;
//        }

//        var levelState = checkLevel(checkbox);   
//        var parent_checkbox = $(checkbox).parents('ul').first().siblings('input')[0];

//        if (levelState === "full") {
//            parent_checkbox.checked = true;
//            parent_checkbox.indeterminate = false;
//        }
//        else if (levelState === "empty") {
//            parent_checkbox.checked = false;
//            parent_checkbox.indeterminate = false;
//        }
//        else {
//            parent_checkbox.checked = false;
//            parent_checkbox.indeterminate = true;
//        }

//        checkParent(parent_checkbox); 
//    }

//    function checkLevel(checkbox) {
//        var sibsCheckboxes = $(checkbox).parent().siblings('li').children('input');
//        var numTrue = 0;
//        if (checkbox.checked) numTrue++;
//        if (checkbox.indeterminate) return "some";

//        for (i = 0; i < sibsCheckboxes.length; i++) {          
//            if ($(sibsCheckboxes[i])[0].checked === true) {
//                numTrue++;         
//            }        
//            if ($(sibsCheckboxes[i])[0].indeterminate === true) {
//                return 'some';
//            }
//        }

//        if (numTrue === sibsCheckboxes.length + 1) {
//            return 'full';
//        }
//        else if (numTrue === 0) {
//            return 'empty';
//        }
//        return 'some';
//    }

//    function checkChild(checkbox) {
//        var isChecked = checkbox.checked;
//        var lis = $(checkbox).siblings('ul').first().children('li');
//        for (var i = 0; i < lis.length; i++) {
//            var child_checkbox = $(lis[i]).children('input')[0];
//            child_checkbox.checked = isChecked;
//            child_checkbox.indeterminate = false;
//            checkChild(child_checkbox);
//        }
//    }

//    function checkRelations(checkbox) {   
//        checkParent(checkbox);
//        checkChild(checkbox);
//    }

//    element.onclick = function (event) {
//        event = event || window.event;
//        var clickedElem = event.target || event.srcElement;

//        if (clickedElem.type === "checkbox") {
//            //var isChecked = clickedElem.checked;
//            checkRelations(clickedElem);
//        }

//        if (!hasClass(clickedElem, 'en-tree_expand')) {
//            return;
//        }
//        var node = clickedElem.parentNode;
//        if (hasClass(node, 'en-tree_expand_leaf')) {
//            return;
//        }
//        if (node.isLoaded || node.getElementsByTagName('li').length) {
//            toggleNode(node);
//            return;
//        }
//        if (node.getElementsByTagName('li').length) {
//            toggleNode(node);
//            return;
//        }
//        load(node);
//    };
//}

//function submitTree() {
//    var data = [];

//    function StartTreeDFS() {
//        var mainLies = $('div[data-id = null]').children('ul').children('li');
//        for (var j = 0; j < mainLies.length; j++)
//            TreeDFS(mainLies[j].dataset.id, 1);

//    }

//    function TreeDFS(LiId, level) {
//        var li = $('li[data-id = ' + LiId + ']');
//        var input = $(li).children('input')[0];
//        if (input.checked === true) {
//            var x = {
//                "Id": +li[0].dataset.id,
//                "Level": level
//            };
//            data.push(x);
//        }
//        else if (input.indeterminate === true) {
//            var childLies = $(li).children('ul').children('li');
//            for (var j = 0; j < childLies.length; j++)
//                TreeDFS(childLies[j].dataset.id, level + 1);
//        }
//    }

//    function onSuccess(data) {
//        if (!data.errcode) {
//            onLoaded(data);
//            showLoading(false);
//        } else {
//            showLoading(false);
//            onLoadError(data);
//        }
//    }
//    function onAjaxError(xhr, status) {
//        var errinfo = { errcode: status };
//        if (xhr.status !== 200) {
//            errinfo.message = xhr.statusText;
//        } else {
//            errinfo.message = 'Некорректные данные с сервера';
//        }
//        onLoadError(errinfo);
//    }
//    function onLoadError(error) {
//        var msg = "Ошибка " + error.errcode;
//        if (error.message)
//            msg = msg + ' :' + error.message;
//        alert(msg);
//    }

//    function onLoaded(data) {
//        var div = $('#enrolleeTable');
//        div.empty();
//        var html = $.parseHTML(data);
//        div.append(html);
//    }

//    return {
//        loadData: function () {
//            StartTreeDFS();
//            $.ajax({
//                url: "/Admin/LoadEnrolleeTable",
//                data: JSON.stringify(data),
//                contentType: "application/json",
//                dataType: "html",
//                success: onSuccess,
//                error: onAjaxError,
//                cache: false,
//                method: "post"
//            });
//        }
//    };
//}
////////////////////////////////////////////////////////////////
var group = { id: '', countElement: '' };

$(function () {
    $('#sortable').sortable({
        start: function (event, ui) {
        },
        update: function (event, ui) {
            UpdatePriority($(ui.item).parents('.en-js-specialities_container').first());
        },
        items: '.en-speciality:not(.en-main_speciality)'
    });
    $('#sortable').disableSelection();
});

function UpdatePriority() {
    $('.en-js-specialities_container').children('.en-speciality')
        .find('.en-speciality_priority')
        .text(function (index, oldtext) {
            return $(this).closest('.en-speciality').prevAll('.en-speciality').length + 1;
        });
}

function SpecialityDeleteButton() {
    $('#selected_specialities').on('click', '.en-js-spec_del_button', function (eventObj) {
        if (confirm('Remove this speciality?')) {
            $(eventObj.target).parents('.en-speciality').first().remove();
            UpdatePriority();
        }     
        UpdateCountSpecBlock($('.en-speciality').length, group.countElement);
        if ($('.en-speciality').length < group.countElement && group.countElement !== 0) {
            $('.en-js-spec_add_button').css('display', 'block');
            //eventObj.target.style.display = 'block';
        }             
    });
}

function SpecialityAddButton() {
    $('#selected_specialities').on('click', '.en-js-spec_add_button', function (eventObj) {
        var onSuccess = function (data) {
            $('.en-js-specialities_container').append(data);
            UpdatePriority();
            UpdateCountSpecBlock($('.en-speciality').length, group.countElement);
            if ($('.en-speciality').length >= group.countElement && group.countElement !== 0) {
                eventObj.target.style.display = 'none';
            }             
        };

        var onError = function (errorData) {
            alert('Error adding speciality' + errorData.responseText);
        };

        $.ajax({
            url: '/Admin/LoadEmptySpeciality',
            method: "post",
            dataType: "html",
            success: onSuccess,
            error: onError
        });
    });
}

function UpdateCountSpecBlock(currentNum, maxNum) {
    $('.en-js-spec_count').text(currentNum + ' / ' + maxNum);
}

function updateDataGroup(callback) {
    var onSuccess = function (data) {
        group.id = data.Id;
        group.countElement = data.Count;
        if (callback) callback();
    };

    var onError = function (errorData) {
        alert('Error update dataGroup' + errorData.responseText);
    };
    var specialityId = $('.en-speciality.en-main_speciality').data('id');
    $.ajax({
        url: '/Admin/UpdateGroupData',
        method: 'post',
        contentType: 'application/json',
        data: JSON.stringify({
            'specialityId': specialityId
        }),
        dataType: 'json',
        success: onSuccess,
        error: onError
    });
}

function InitDropdowns(thisSection, thisUserParam, thisIsMain) {
    var section = thisSection;
    var userParameters = thisUserParam;
    var isMain = thisIsMain;
    var parameters = {
        educationPlace: "",
        financingType: "",
        speciality: "",
        educationForm: "",
        educationDuration: ""
    };

    var edPlaceEl = $(section).find('.en-js-edPlace')[0];
    var finTypeEl = $(section).find('.en-js-finType')[0];
    var specEl = $(section).find('.en-js-NCSQspec')[0];
    var edFormEl = $(section).find('.en-js-edForm')[0];
    var edDurEl = $(section).find('.en-js-edDur')[0];

    edPlaceEl.typeOfValue = 'educationPlace';
    finTypeEl.typeOfValue = 'financingType';
    specEl.typeOfValue = 'speciality';
    edFormEl.typeOfValue = 'educationForm';
    edDurEl.typeOfValue = 'educationDuration';

    setRelation(edPlaceEl, finTypeEl, '/Admin/LoadEducationPlaceOptions');
    setRelation(finTypeEl, edFormEl, '/Admin/LoadFinancingTypeOptions');
    setRelation(edFormEl, edDurEl, '/Admin/LoadEducationFormOptions');
    setRelation(edDurEl, specEl, '/Admin/LoadEducationDurationOptions');
    setRelation(specEl, null, '/Admin/LoadNCSQSpecialityOptions');

    edPlaceEl.load(function () {
        userParameters = null;
        updateDataId(function () {
            updateDataGroup(function () {
                UpdateCountSpecBlock($('.en-speciality').length, group.countElement);
            });
        }); 
    });

    function updateParameters() {
        parameters = {
            educationPlace: edPlaceEl.value,
            financingType: finTypeEl.value,
            speciality: specEl.value,
            educationForm: edFormEl.value,
            educationDuration: edDurEl.value
        };   
    }

    function setRelation(thisEl, nextEl, url) {
        thisEl.clear = function () {
            while (thisEl.firstChild) {
                thisEl.removeChild(thisEl.firstChild);
            }
            if (nextEl && nextEl.clear) nextEl.clear();
        };
        
        thisEl.load = function (callback) {         
            thisEl.clear();      

            loadDropdown(thisEl, url, function () {
                if (userParameters) {
                    parameters[thisEl.typeOfValue] = userParameters[thisEl.typeOfValue];
                    thisEl.value = userParameters[thisEl.typeOfValue];
                } else {
                    updateParameters();
                }

                if (nextEl && nextEl.load) {
                    nextEl.load(callback);
                } else {
                    if (callback) callback();
                }
            });     
        };

        if (isMain) {
            var oldValue;
            thisEl.onfocus = function () {
                oldValue = thisEl.value;
            };

            thisEl.onchange = function () {
                var otherSpec = $('.en-speciality:not(.en-main_speciality)');
                if (otherSpec.length === 0 || confirm('If you change this field other specialities will be deleted. Are you sure you want to do this?')) {
                    if (nextEl) {
                        otherSpec.remove();
                        nextEl.load(function () {
                            updateDataId(function () {
                                updateDataGroup();
                            });                    
                        });
                    } 
                } else {
                    thisEl.value = oldValue;
                }
            };
        } else {
            thisEl.onchange = function () {
                if (nextEl) {
                    nextEl.load(function () {
                        updateDataId();
                    });
                } else {
                    updateDataId();
                }   
            };
        }         
    } 

    function updateDataId(callback) {
        if (!userParameters) updateParameters();
        if (parameters.educationPlace > 0 &&
            parameters.financingType > 0 &&
            parameters.educationForm > 0 &&
            parameters.educationDuration > 0 &&
            parameters.speciality > 0) {

            var onSuccess = function (data) {
                if (data.Id > 0) {
                    section.dataset.id = data.Id;
                } else {
                    section.dataset.id = 0;
                }
                if (callback) callback();
            };

            var onError = function (errorData) {
                alert('Error update dataId' + errorData.responseText);
            };

            $.ajax({
                url: '/Admin/UpdateSpecialityDataId',
                method: 'post',
                contentType: 'application/json',
                data: JSON.stringify({
                    'GroupId': group.id,
                    'EducationPlaceId': parameters.educationPlace,
                    'FinancingTypeId': parameters.financingType,
                    'SpecialityId': parameters.speciality,
                    'EducationFormId': parameters.educationForm,
                    'EducationDurationId': parameters.educationDuration
                }),
                dataType: 'json',
                success: onSuccess,
                error: onError
            });
        } else {
            section.dataset.id = 0;
        }
    }

    function loadDropdown(element, url, callback) {
        if(!userParameters) updateParameters();
        var onSuccess = function (data) {
            if (data.length > 1) {
                element.options[0] = new Option('-Not selected-', null);
                for (var i = 0; i < data.length; i++) {
                    element.options[i + 1] = new Option(data[i].Text, data[i].Value);
                    element.options[i + 1].title = data[i].Label;
                }
            } else if (data.length === 1) {
                element.options[0] = new Option(data[0].Text, data[0].Value);
            } else {
                element.options[0] = new Option('-Not exist-', null);
            }

            if(callback) callback();
        };

        var onError = function (errorData) {
            alert('Error load dropdown' + errorData.responseText);
        };
        var groupId = isMain ? "" : group.id;

        $.ajax({
            url: url,
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify({
                'GroupId': groupId,
                'EducationPlaceId': parameters.educationPlace,
                'FinancingTypeId': parameters.financingType,
                'SpecialityId': parameters.speciality,
                'EducationFormId': parameters.educationForm,
                'EducationDurationId': parameters.educationDuration,
                'SelectedSpecialities': GetSelectedSpecIds()
            }),
            dataType: 'json',
            success: onSuccess,
            error: onError
        });
    }

    function GetSelectedSpecIds() {
        var ids = [];
        $(section).siblings('.en-speciality').each(function (i, spec) {
            if (spec.dataset.id > 0) {
                ids.push(spec.dataset.id);
            }
        });
        return ids;
    } 
}




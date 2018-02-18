$(function () {
    $('#sortable').sortable({
        start: function (event, ui) {
        },
        update: function (event, ui) {
            UpdatePriority($(ui.item).parents('.en-js-specialities_container').first());
            EnableSaveButton();
        },
        items: '.en-speciality:not(.en-main_speciality)'
    });
    $('#sortable').disableSelection();
});

var group = { id: '', countElement: '' };

function SpecialityDeleteButton() {
    $('#selected_specialities').on('click', '.en-js-spec_del_button', function (eventObj) {
        if (confirm('Remove this speciality?')) {
            $(eventObj.target).parents('.en-speciality').first().remove();
            EnableSaveButton();
            UpdatePriority();
        }     
        UpdateCountSpecBlock();
        CheckAddButton();
    });
}

function SpecialityAddButton() {
    $('#selected_specialities').on('click', '.en-js-spec_add_button', function (eventObj) {
        var onSuccess = function (data) {
            $('.en-js-specialities_container').append(data);
            UpdatePriority();
            UpdateCountSpecBlock();
            CheckAddButton();            
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

function CheckAddButton() {
    if ($('.en-speciality').length < group.countElement && group.countElement > 0) {
        $('.en-js-spec_add_button').css('display', 'block');
    } else {
        $('.en-js-spec_add_button').css('display', 'none');
    }
}

function EnableSaveButton() {
    $('.en-js-app_save_button').prop('disabled', false);
}

function ApplicationSaveButton(enrolleeId, applicationId) {
    $(document).on('click', '.en-js-app_save_button', function () {
        var specialities = [];

        var onSuccess = function (data) {
            if (data) {
                $('.en-js-app_save_button').prop('disabled', true);
            } else {
                alert('Error save application in database');
            }
        };

        var onError = function () {
            alert('Error send data' + errorData.responseText);
        };

        $('.en-speciality').each(function (index, spec) {
            if (spec.dataset.id === 0) {
                alert('Check the correctness of the entered data');
                return;
            } else {
                specialities.push({
                    'Priority': +$(spec).find('.en-speciality_priority').text(),
                    'SpecialityId': +spec.dataset.id,
                    'ApplicationId': +applicationId
                });
            }
        });

        $.ajax({
            url: '/Admin/EditApplication',
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify({
                'ApplicationId': applicationId,
                'EnrolleeId': enrolleeId,
                'Specialities': specialities
            }),
            dataType: 'json',
            success: onSuccess,
            error: onError
        });
    });
}

function UpdatePriority() {
    $('.en-js-specialities_container').children('.en-speciality')
        .find('.en-speciality_priority')
        .text(function (index, oldtext) {
            return $(this).closest('.en-speciality').prevAll('.en-speciality').length + 1;
        });
}

function UpdateCountSpecBlock() {
    var currentNum = $('.en-speciality').length;
    var maxNum = group.countElement;
    $('.en-js-spec_count').text(currentNum + ' / ' + (maxNum === 0 ? '*' : maxNum));
}

function UpdateDataGroup(callback) {
    var onSuccess = function (data) {
        group.id = data.Id;
        group.countElement = data.Count;
        if (callback) callback();
    };

    var onError = function (errorData) {
        alert('Error update dataGroup' + errorData.responseText);
    };
    var specialityId = $('.en-speciality.en-main_speciality')[0].dataset.id;
    if (specialityId == 0) {
        group.id = 0;
        group.countElement = 0;
        if (callback) callback();
        return;
    }
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

    SetRelation(edPlaceEl, finTypeEl, '/Admin/LoadEducationPlaceOptions');
    SetRelation(finTypeEl, edFormEl, '/Admin/LoadFinancingTypeOptions');
    SetRelation(edFormEl, edDurEl, '/Admin/LoadEducationFormOptions');
    SetRelation(edDurEl, specEl, '/Admin/LoadEducationDurationOptions');
    SetRelation(specEl, null, '/Admin/LoadNCSQSpecialityOptions');

    edPlaceEl.load(function () {
        userParameters = null;
        UpdateDataId(function () {
            UpdateDataGroup(function () {
                UpdateCountSpecBlock();
            });
        }); 
    });

    function UpdateParameters() {
        parameters = {
            educationPlace: edPlaceEl.value,
            financingType: finTypeEl.value,
            speciality: specEl.value,
            educationForm: edFormEl.value,
            educationDuration: edDurEl.value
        };   
    }

    function UpdateDataId(callback) {
        if (!userParameters) UpdateParameters();
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
                    'GroupId': isMain ? '' : group.id,
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
            if (callback) callback();
        }
    }

    function SetRelation(thisEl, nextEl, url) {
        thisEl.clear = function () {
            while (thisEl.firstChild) {
                thisEl.removeChild(thisEl.firstChild);
            }
            if (nextEl && nextEl.clear) nextEl.clear();
        };
        
        thisEl.load = function (callback) {         
            thisEl.clear();      

            LoadDropdown(thisEl, url, function () {
                if (userParameters) {
                    parameters[thisEl.typeOfValue] = userParameters[thisEl.typeOfValue];
                    thisEl.value = userParameters[thisEl.typeOfValue];
                } else {
                    UpdateParameters();
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
                    otherSpec.remove();
                    NextElementlOrCallback(nextEl, function () {
                        UpdateDataId(function () {
                            UpdateDataGroup(function () {
                                UpdateCountSpecBlock();
                                CheckAddButton();
                                EnableSaveButton();
                            });
                        });
                    });
                } else {
                    thisEl.value = oldValue;
                }
            };
        } else {
            thisEl.onchange = function () {
                NextElementlOrCallback(nextEl, function () {
                    UpdateDataId();
                    EnableSaveButton();
                });  
            };
        }         
    } 

    function NextElementlOrCallback(nextEl, callback) {
        if (nextEl) {
            nextEl.load(function () {
                callback();
            });
        } else {
            callback();
        }
    }

    function LoadDropdown(element, url, callback) {
        if(!userParameters) UpdateParameters();
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

        $.ajax({
            url: url,
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify({
                'GroupId': isMain ? "" : group.id,
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




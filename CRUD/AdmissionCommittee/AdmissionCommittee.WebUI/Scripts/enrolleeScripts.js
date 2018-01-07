
$(function () {
    $(".ul-dropfree").find("li:has(ul)").prepend('<div class="drop"></div>');
    $(".ul-dropfree div.drop").one('click', function () {
        //первая подгрузка наследников 
    })
    $(".ul-dropfree div.drop").click(function () {
        if ($(this).nextAll("ul").css('display') == 'none') {
            $(this).nextAll("ul").slideDown(400);
            $(this).css({ 'background-position': "-11px 0" });
        } else {
            $(this).nextAll("ul").slideUp(400);
            $(this).css({ 'background-position': "0 0" });
        }
    });
    $(".ul-dropfree").find("ul").slideUp(400).parents("li").children("div.drop").css({ 'background-position': "0 0" });
})

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
})

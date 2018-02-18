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
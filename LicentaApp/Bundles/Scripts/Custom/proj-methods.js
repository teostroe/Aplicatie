function getAutocompleteDataFromArray(array) {
    var obj = {};
    for (var i = 0; i < array.length; ++i) {
        obj[array[i]] = null;
    }
    return obj;
}

function loadAutocomplete(elementId, array) {
    $(elementId).autocomplete({
        data: getAutocompleteDataFromArray(array),
        limit: 3
    });
}

function anyContains(array, value) {
    for (var i = 0; i < array.length; ++i) {
        if (array[i].includes(value)) {
            return true;
        }
    }
}

function addOrRemove(array, value, id) {
    var index = array.indexOf(id);
    if (value && index < 0) {
        array.push(id);
        return;
    }

    if (!value && index >= 0) {
        array.splice(index, 1);
        return;
    }
}

function showHide(conditionToDisplay, element) {
    if (conditionToDisplay) {
        element.show();
    } else {
        element.hide();
    }
}


//containerId: container to load data;
//url: url of controller with/without params
//nextChangeFunction: method for the next change event
//dom: $(this)
function loadStepOnItemChange(containerId, url, nextChangeFunction, dom, conditionToDisplay) {
    conditionToDisplay = conditionToDisplay || dom.val();
    $(containerId).load(url,
        function() {
            $('select').formSelect();
            if (nextChangeFunction) {
                for (var i = 0; i < nextChangeFunction.length; ++i) {
                    nextChangeFunction[i]();
                }
            };
        });
    showHide(conditionToDisplay, $(containerId));
}

function getSqlDateFormat(value) {
    //get type: dd/MM/yyyy
    //result type: yyyy-MM-dd
    var items = value.split('/');
    return items[2] + '-' + items[1] + '-' + items[0];
}
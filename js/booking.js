$(document).ready(function () {
    var detailsArray = new Array();
    detailsArray = '@Html.Raw(Json.Encode(ViewBag.HallDetail))';
    $("#date").attr("name", "detailsObjList[" + 0 + "].date");
    var newFields = document.getElementById('bookingForm').cloneNode(true);
    newFields.id = '';
    newFields.style.display = '';
    var newField = newFields.childNodes;
    for (var i = 0; i < newField.length; i++) {
        var newId = newField[i].id
        if (newId) {
            newField[i].id = newId + click;
            if (newField[i].nodeName == "TABLE") {
                var tableChildren = newField[i].childNodes;
                for (var j = 0; j < tableChildren.length; j++) {
                    var tableChildId = tableChildren[j].id;
                    if (tableChildId) {
                        tableChildren[j].id = tableChildId + click;
                        if (tableChildren[j].nodeName == "TBODY") {
                            var tbodyChildren = tableChildren[j].childNodes;
                            for (var k = 0; k < tbodyChildren.length; k++) {
                                var tbodyChildId = tbodyChildren[k].id;
                                if (tbodyChildId) {
                                    tbodyChildren[k].id = tbodyChildId + click;
                                    if (tbodyChildren[k].nodeName == "TR") {
                                        var trChild = tbodyChildren[k].childNodes;
                                        for (var q = 0; q < trChild.length; q++) {
                                            var trChildId = trChild[q].id;
                                            if (trChildId) {
                                                trChild[q].id = trChildId + click;
                                                if (trChild[q].nodeName == "TD") {
                                                    var tdChild = trChild[q].childNodes;
                                                    for (var r = 0; r < tdChild.length; r++) {
                                                        var tdChildId = tdChild[r].id;
                                                        if (tdChildId) {
                                                            tdChild[r].id = tdChildId + click;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    var insertHere = document.getElementById('writeroot');
    insertHere.parentNode.insertBefore(newFields, insertHere);
});
var click = 0;
var submitClick = 0;
var today = new Date();
$(function () {
    $("#date" + click).datepicker({
        minDate: '0',
        yearRange: '-0:+1',
        hideIfNoPrevNext: true,
        dateFormat: 'dd-mm-yy',
    });
});
var addMore = function (index, hall, slot, requirement) {
    click = click + 1;

    var newFields = document.getElementById('bookingForm').cloneNode(true);
    newFields.id = '';
    newFields.style.display = '';
    var newField = newFields.childNodes;
    for (var i = 0; i < newField.length; i++) {
        var newId = newField[i].id
        if (newId) {
            newField[i].id = newId + click;
            if (newField[i].nodeName == "TABLE") {
                var tableChildren = newField[i].childNodes;
                for (var j = 0; j < tableChildren.length; j++) {
                    var tableChildId = tableChildren[j].id;
                    if (tableChildId) {
                        tableChildren[j].id = tableChildId + click;
                        if (tableChildren[j].nodeName == "TBODY") {
                            var tbodyChildren = tableChildren[j].childNodes;
                            for (var k = 0; k < tbodyChildren.length; k++) {
                                var tbodyChildId = tbodyChildren[k].id;
                                if (tbodyChildId) {
                                    tbodyChildren[k].id = tbodyChildId + click;
                                    if (tbodyChildren[k].nodeName == "TR") {
                                        var trChild = tbodyChildren[k].childNodes;
                                        for (var q = 0; q < trChild.length; q++) {
                                            var trChildId = trChild[q].id;
                                            if (trChildId) {
                                                trChild[q].id = trChildId + click;
                                                if (trChild[q].nodeName == "TD") {
                                                    var tdChild = trChild[q].childNodes;
                                                    for (var r = 0; r < tdChild.length; r++) {
                                                        var tdChildId = tdChild[r].id;
                                                        if (tdChildId) {
                                                            tdChild[r].id = tdChildId + click;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    var insertHere = document.getElementById('writeroot');
    insertHere.parentNode.insertBefore(newFields, insertHere);
    //document.getElementById("bookingForm" + click).reset();

    var j = 1, k = 0;
    var iHall = hall;
    for (i = 1; i <= click; i++) {
        $("#date" + click).attr("name", "detailsObjList[" + i + "].date");
        $("#alternateDate" + click).attr("name", "detailsObjList[" + i + "].date");
        for (j = 1; j <= 6; j++) {
            iHall = hall;
            for (p = 1; p <= 4; p++) {
                $("#hiddenhall" + (hall + p - 1) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].hallID");
                $("#hiddenhallname" + (hall + p - 1) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].hallName");
                $("#slotcheck" + (slot + j) + "_" + (hall + p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].slotsArray[" + (slot + k) + "].isSelected");
                $("#slotcheck" + (slot + j) + "_" + (hall + p) + "_" + click).attr("class", "slots" + (hall + p));
                $("#hiddenslot" + (slot + j) + "_" + (hall + p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].slotsArray[" + (slot + k) + "].slotID");
                $("#reqcheck" + (requirement + j) + "_" + (hall + p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].requirementsArray[" + (requirement + k) + "].isSelected");
                $("#reqcheck" + (requirement + j) + "_" + (hall + p) + "_" + click).attr("class", "reqs" + (hall + p));
                $("#hiddenreq" + (requirement + j) + "_" + (hall + p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].requirementsArray[" + (requirement + k) + "].requirementID");
                $("#chairs_" + (hall + p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].noOfChairs");
                iHall++;
            }
            k++;
        }
    }
    var today = new Date();
    $(function () {
        $("#date" + click).datepicker({
            minDate: '0',
            yearRange: '-0:+1',
            hideIfNoPrevNext: true,
            dateFormat: 'dd-mm-yy',
        });
    });
}
var seeDetails = function (slotID, hallID) {
    if ($("#slotcheck" + slotID + "_" + hallID + "_" + click).is(':disabled')) {
        var id = "date" + click;
        var date = document.getElementById(id).value;
        $.ajax({
            type: "POST",
            data: {
                'date': date,
                'slotID': slotID,
                'hallID': hallID
            },
            dataType: 'JSON',
            url: "/BookUtility/GetBookedByDetails",
            success: function (department) {
                if (department != "") {
                    var title = "Booked By " + department;
                    //$("#slotcheck" + slotID + "_" + hallID + "_" + click).attr("title", title).tooltip({ trigger: 'hover' });
                    //$("#lblSlot" + slotID + "_" + hallID + "_" + click).attr("title", title).tooltip({ trigger: 'mouseover' });
                    //$("#slotcheck" + slotID + "_" + hallID + "_" + click).attr("title", title).tooltip({ trigger: 'mouseover' });
                    document.getElementById("span" + slotID + "_" + hallID + "_" + click).innerHTML = title;
                    //$('[data-toggle="tooltip"]').tooltip();
                    console.log(title);
                }
            }
        });
    }
}
var slotcheckchange = function (slotid, hallid) {
    var id = "#slotcheck" + slotid + "_" + hallid + "_" + click;
    if ($(id).prop('checked')) {
        if (slotid != 6)
            $("#slotcheck" + 6 + "_" + hallid + "_" + click).prop("disabled", true);
        else {
            $('.slots' + hallid).each(function () {
                $(this).prop('disabled', true);
            });
            $("#slotcheck" + 6 + "_" + hallid + "_" + click).prop("disabled", false);
        }
        $(id).val('true');
    }
    else {
        $("#slotcheck" + 6 + "_" + hallid + "_" + click).prop("disabled", false);
        if (slotid == 6) {
            $('.slots' + hallid).each(function () {
                $(this).prop('disabled', false);
            });
        }
    }
    if (submitClick > 0) {
        if ($("input.slots" + hallid + "[type=checkbox]:checked").val()) {
            $("#detailsErr" + click).hide();
        }
        else {
            $("#detailsErr" + click).show();
        }
    }
}
var reqcheckchange = function (reqid, hallid) {
    var id = "#reqcheck" + reqid + "_" + hallid + "_" + click;
    if ($(id).prop('checked')) {
        if (reqid == 3)
        {
            $("#chairs_" + (hallid) + "_" + click).show();
        }
        $(id).val('true');
    }
    else {
        $("#chairs_" + (hallid) + "_" + click).hide();
    }
}
var datechosen = function () {
    
        $("#date"+click).datepicker({
            dateFormat: 'dd-mm-yy',
            altFormat: "dd-mm-yy",
            altField: '#alternateDate'
    });
    $('#alternateDate' + click).val(document.getElementById("date" + click).value);
    var id = "alternateDate" + click;
    var date = document.getElementById(id).value;
    if (date != "") {
        $("#invalidDate" + click).hide();
        $.ajax({
            type: "POST",
            data: { 'date': date },
            dataType: 'JSON',
            url: "/BookUtility/GetBookedSlotsList",
            success: function (bookedSlotsList) {
                var bookedSlotsItem = '';
                $.each(bookedSlotsList, function (i, bookedSlotsItem) {
                    $("#slotcheck" + 6 + "_" + bookedSlotsItem.hallID + "_" + click).prop("disabled", false);
                    $("#slotcheck" + bookedSlotsItem.slotID + "_" + bookedSlotsItem.hallID + "_" + click).prop("disabled", true);
                    if (!$("#slotcheck" + 6 + "_" + bookedSlotsItem.hallID + "_" + click).attr('disabled')) {
                        $("#slotcheck" + 6 + "_" + bookedSlotsItem.hallID + "_" + click).prop("disabled", true);
                    }
                    else {
                        $('.slots' + bookedSlotsItem.hallID).each(function () {
                            $(this).prop('disabled', true);
                        });
                    }
                });
            }
        });
    }
    getRequirementsList();
    //}
    //else
    //{
    //    document.getElementById("invalidDate" + click).innerHTML = "Enter valid date";
    //}
}
var getRequirementsList = function () {
    $.ajax({
        type: "GET",
        dataType: 'JSON',
        url: "/BookUtility/GetRequirementsList",
        success: function (requirementsList) {
            var requirementsItem = '';
            $.each(requirementsList, function (i, requirementsItem) {
                $("#reqcheck" + requirementsItem.requirementID + "_" + requirementsItem.hallID + "_" + click).removeAttr("disabled");
            })
        }
    });
}


var testAllInput = function (hall, slot, requirement) {
    submitClick = submitClick + 1;
    var dateId = new Array();
    var date = new Array();
    var dateFlag = 1, hallFlag = 0;
    for (var i = 0; i <= click; i++) {
        dateId[i] = "alternateDate" + i;
        date[i] = document.getElementById(dateId[i]).value;
        if (date[i] == "") {
            document.getElementById("invalidDate" + i).innerHTML = "Enter Date";
            $("#invalidDate" + i).show();
            dateFlag = 0;
        }

        for (var j = 0; j < hall; j++) {
            var checkedSlot = $("input.slots" + (hall + 1) + "[type=checkbox]:checked").val();
            var checkedRequirement = $("input.reqs" + (hall + 1) + "[type=checkbox]:checked").val();
            if (checkedSlot && checkedRequirement) {
                hallFlag = 1;
                break;
            }
        }
        if (dateFlag != 1 && hallFlag != 1) {
            document.getElementById("detailsErr" + i).innerHTML = "Select details of atleast one hall";
            $("#detailsErr"+i).show();
        }
    }
}
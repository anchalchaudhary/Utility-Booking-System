﻿$(document).ready(function () {
    var detailsArray = new Array();
    detailsArray = '@Html.Raw(Json.Encode(ViewBag.HallDetail))';
    $("#date").attr("name", "detailsObjList[" + 0 + "].date");
    var newFields = document.getElementById('bookingForm').cloneNode(true);
    newFields.id = '';
    newFields.style.display = '';
    var newField = newFields.childNodes;
    for (var i = 0; i < newField.length; i++) {
        var newId = newField[i].id
        if (newId)
            newField[i].id = newId + click;
    }
    var insertHere = document.getElementById('writeroot');
    insertHere.parentNode.insertBefore(newFields, insertHere);
});
var click = 0;
var today = new Date();
$(function () {
    $("#date" + click).datepicker({
        minDate: '0',
        yearRange: '-0:+1',
        hideIfNoPrevNext: true
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
        if (newId)
            newField[i].id = newId + click;
    }
    var insertHere = document.getElementById('writeroot');
    insertHere.parentNode.insertBefore(newFields, insertHere);
    //document.getElementById("bookingForm" + click).reset();

    var j = 1, k = 0;
    var iHall = hall;
    for (i = 1; i <= click; i++) {
        $("#date" + click).attr("name", "detailsObjList[" + i + "].date");

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
            hideIfNoPrevNext: true
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
}
var reqcheckchange = function (reqid, hallid) {
    var id = "#reqcheck" + reqid + "_" + hallid + "_" + click;
    if ($(id).prop('checked')) {
        $(id).val('true');
    }
}
var datechosen = function () {
    var id = "date" + click;
    var date = document.getElementById(id).value;
    //var today = new Date(Date.now()).toLocaleString();

    //if (date > today) {
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
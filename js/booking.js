$(document).ready(function () {
        $(window).scroll(function () {
            var scroll = $(window).scrollTop();
            if (scroll > 80) {
                $("#navbaar").css("background", "#F0A830");
            }
            else {
                $("#navbaar").css("background", "rgba(255,255,255,0)");
            }
        })

        var detailsArray = new Array();
    
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

    $("#reqcheck4_2_0").hide();
    $("#reqcheck6_2_0").hide();
    $("#reqcheck4_3_0").hide();
    $("#reqcheck6_3_0").hide();
    $("#reqcheck6_4_0").hide();
    $("#req6_4_0").hide();
    $("#req6_3_0").hide();
    $("#req6_2_0").hide();
    $("#req4_2_0").hide();
    $("#req4_3_0").hide();


});
var click = 0;
var submitClick = 0;
var today = new Date();
$(function () {
    var d = new Date();
    var today = d.getDate();
    var month = d.getMonth()+1+2;
    var year = d.getFullYear();
    var days = new Date(year, month, 0).getDate();
    //if (month % 2 == 1)
    //    today = today + 1;
    $("#date" + click).datepicker({
        minDate: '0',
        changeMonth: true,
        changeYear: true,
        maxDate: "+2M"+(days-today),
        yearRange: '-0:+1',
        hideIfNoPrevNext: true,
        dateFormat: 'dd-mm-yy',
    });
});

var nonWokingHours = function () {
    if ($("#nonWorkingHours_"+click).prop('checked')) {
        $("#nonWorkingHours_" + click).val('true');
        $('#myModal').modal('show')
    }
}
var annexeRequired = function () {
    if ($("#annexe_" + click).prop('checked')) {
        $("#annexe_" + click).val('true');
    }
}
var photographer = function () {
    if ($("#photographer_" + click).prop('checked')) {
        $("#photographer_" + click).val('true');
    }
}
var addMore = function (index, hall, slot, requirement) {
    if (testAllInput(hall, slot, requirement))
    {
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
        $("#removenode_" + click).show();
        $("#reqcheck4_2_" + click).hide();
        $("#reqcheck6_2_" + click).hide();
        $("#reqcheck4_3_" + click).hide();
        $("#reqcheck6_3_" + click).hide();
        $("#reqcheck6_4_" + click).hide();
        $("#req6_4_" + click).hide();
        $("#req6_3_" + click).hide();
        $("#req6_2_" + click).hide();
        $("#req4_2_" + click).hide();
        $("#req4_3_" + click).hide();
        var j = 1, k = 0;
        //var iHall = hall;
        var iHall = 0;
        for (i = 1; i <= click; i++) {
            k = 0;
            $("#date" + click).attr("name", "detailsObjList[" + i + "].date");
            $("#alternateDate" + click).attr("name", "detailsObjList[" + i + "].date");
            for (j = 1; j <= 6; j++) {
                //iHall = hall;
                iHall = 0;
                for (p = 1; p <= 4; p++) {
                    $("#nonWorkingHours_" + click).attr("name", "detailsObjList[" + (index + i) + "].nonWorkingHours");
                    $("#annexe_" + click).attr("name", "detailsObjList[" + (index + i) + "].annexeRequired");
                    $("#photographer_" + click).attr("name", "detailsObjList[" + (index + i) + "].photographer");
                    $("#hiddenhall" + (p - 1) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].hallID");
                    $("#hiddenhallname" + (p - 1) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].hallName");
                    $("#slotcheck" + (j) + "_" + (p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].slotsArray[" + (k) + "].isSelected");
                    $("#slotcheck" + (j) + "_" + (p) + "_" + click).attr("class", "slots" + (p));
                    $("#hiddenslot" + (j) + "_" + (p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].slotsArray[" + (k) + "].slotID");
                    $("#reqcheck" + (j) + "_" + (p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].requirementsArray[" + (k) + "].isSelected");
                    $("#reqcheck" + (j) + "_" + (p) + "_" + click).attr("class", "reqs" + (p));
                    $("#hiddenreq" + (j) + "_" + (p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].requirementsArray[" + (k) + "].requirementID");
                    $("#chairs_" + (p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].noOfChairs");
                    $("#others_" + (p) + "_" + click).attr("name", "detailsObjList[" + (index + i) + "].hallsArray[" + iHall + "].otherRequirements");
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
}
var seeDetails = function (slotID, hallID) {
    if ($("#slotcheck" + slotID + "_" + hallID + "_" + click).is(':disabled')) {
        var id = "date" + click;
        var date = document.getElementById(id).value;
        var detailsURL = $("#detailsURL").val();
        $.ajax({
            type: "POST",
            data: {
                'date': date,
                'slotID': slotID,
                'hallID': hallID
            },
            dataType: 'JSON',
            url: detailsURL,
            success: function (department) {
                if (department != "") {
                    var title = "Booked By " + department;
                    //$("#slotcheck" + slotID + "_" + hallID + "_" + click).attr("title", title).tooltip({ trigger: 'hover' });
                    $("#lblSlot" + slotID + "_" + hallID + "_" + click).attr("title", title).tooltip({ trigger: 'mouseover' });
                    $("#slotcheck" + slotID + "_" + hallID + "_" + click).attr("title", title).tooltip({ trigger: 'mouseover' });
                    document.getElementById("span" + slotID + "_" + hallID + "_" + click).innerHTML = title;
                    $('[data-toggle="tooltip"]').tooltip();
                    console.log(title);
                }
            }
        });
    }
}
var slotcheckchange = function (slotid, hallid) {
    testAllInput(4, 6, 6);
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
            $("#btnSubmit").removeAttr("disabled");
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
    var dateURL = $("#dateURL").val();

    if (date != "") {
        $("#invalidDate" + click).hide();
        $.ajax({
            type: "POST",
            data: { 'date': date },
            dataType: 'JSON',
            url: dateURL,
            success: function (bookedSlotsList) {
                var bookedSlotsItem = '';
                if (bookedSlotsList.length != 0) {
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
                else {
                    for (var i = 1; i <= 4; i++) {
                        $('.slots' + i).each(function () {
                            $(this).removeAttr("disabled");
                        });
                    }
                }
            }
        });
    }
    getRequirementsList();
}
var getRequirementsList = function () {
    var reqURL = $("#reqURL").val();
    $.ajax({
        type: "GET",
        dataType: 'JSON',
        url: reqURL,
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
            var checkedSlot = $("input.slots" + (j + 1) + "[type=checkbox]:checked").val();
            var checkedRequirement = $("input.reqs" + (j + 1) + "[type=checkbox]:checked").val();
            if (checkedSlot && checkedRequirement) {
                hallFlag = 1;
                break;
            }
            else {
                hallFlag = 0;
            }
        }
        if (dateFlag != 1 && hallFlag != 1) {
            document.getElementById("detailsErr" + i).innerHTML = "Select details of chosen hall(s)";
            $("#detailsErr" + i).show();
            $("#btnSubmit").attr("disabled", true);
            return false;
        }
        if (dateFlag ==1 && hallFlag != 1) {
            document.getElementById("detailsErr" + i).innerHTML = "Select details of chosen hall(s)";
            $("#detailsErr" + i).show();
            $("#btnSubmit").attr("disabled", true);
            return false;
        }
        else
            return true;
    }
}
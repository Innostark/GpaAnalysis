function SendMessage() {
    if (validateContactUsForm()) {
        var serviceUrl = '/Clinic/ContactUs';
        var email = $("#email").val();
        var phone = $("#phone").val();
        var name = $("#name").val();
        var message = $("#message").val();
        var data = { name: name, email: email, phone: phone, message: message };
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successSendMessage,
            error: errorSendMessage
        });
    }
}

function successSendMessage(data, status) {
    //alert("You have successfully sent Email to Admin!");
    toastr.warning("Thanks for contacting us. We will get back to you shortly");

    resetContactUsForm();
}

function errorSendMessage(e, status) {
}

function validateContactUsForm()
{
    var listSimple = $('.contactus');
    var counter = 0;
    for (var i = 0; i < listSimple.length; i++) {
        var control = listSimple[i];
        var fieldvalue = $(control).val();
        var placeholder = $(control).attr('data-value');
        if (fieldvalue == null || fieldvalue == "" || fieldvalue == placeholder) {
            //$(control).prop('placeholder', $(control).prop('placeholder'));
            counter++;
            $(control).addClass("Error");
            //implementing scroll back to error
        }
        else {
            $(control).removeClass("Error");
        }
    }

    if (counter > 0) {
        //implementing focus back to error
        var divID = $(".Error")[0].id;
        if ($("#" + divID).length > 0)
            $("#" + divID).focus();
        return false;
    }
    return true;
}

function SaveAppointment() {
    if (validateAppointmentForm()) {
        var serviceUrl = '/Clinic/SaveAppointment';
        var email = $("#EmailAdress").val();
        var phone = $("#MobileNumber").val();
        var age = $("#Age").val();
        var nationality = $("#Nationality").val();
        var name = $("#FullName").val();
        var date = $("#datepicker").val();
        var data = { NameA: name, NameE: name, Email: email, Mobile: phone, Age: age, Nationality: nationality, AppointmentDate: date };
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successSaveAppointment,
            error: errorSaveAppointment
        });
    }
}

function successSaveAppointment(data, status) {
    //alert("You have successfully booked an appointment!");
    toastr.success("You have successfully booked an appointment");
    resetAppointmentForm();
}

function errorSaveAppointment(e, status) {
}

function validateEmail(email) {
    debugger
    var oEmail = $(email);
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test(oEmail.val())) {
        oEmail.val("");
        oEmail.attr("placeholder","Enter valid email.");
        return false;
    } else {
        return true;
    }
}

function validateAppointmentForm() {
    var listSimple = $('.appointment');
    var counter = 0;
    for (var i = 0; i < listSimple.length; i++) {
        var control = listSimple[i];
        var fieldvalue = $(control).val();
        var placeholder = $(control).attr('data-value');
        if (fieldvalue == null || fieldvalue == "" || fieldvalue == placeholder) {
            //$(control).prop('placeholder', $(control).prop('placeholder'));
            counter++;
            $(control).addClass("Error");
            //implementing scroll back to error
        }
        else {
            $(control).removeClass("Error");
        }
    }

    if (counter > 0) {
        //implementing focus back to error
        var divID = $(".Error")[0].id;
        if ($("#" + divID).length > 0)
            $("#" + divID).focus();
        return false;
    }
    //return validateDate();
    return true;
} 

function validateDate() {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var start = d.getFullYear() + '-' +
        (('' + month).length < 2 ? '0' : '') + month + '-' +
        (('' + day).length < 2 ? '0' : '') + day;
    
    var end = $('#datepicker').val();
    if (new Date(start) < new Date(end)) {
        $('#datepicker').removeClass("Error");
        return true;
    } else {
        $('#datepicker').addClass("Error");
        return false;
    }
}

function resetAppointmentForm()
{
    $(':input', '#contact')
        .not(':button, :submit, :reset, :hidden')
        .val('');

    $("#FullName").attr("placeholder", "الاسم");
    $("#EmailAdress").attr("placeholder", "البريد الالكتروني");
    $("#MobileNumber").attr("placeholder", "رقم الجوال");
    $("#Age").attr("placeholder", "العمر");
    $("#Nationality").attr("placeholder", "الجنسية");
    $("#datepicker").attr("placeholder", "التاريخ");
}

function resetContactUsForm() {
    $(':input', '#contactus')
        .not(':button, :submit, :reset, :hidden')
        .val('');

    $("#name").attr("placeholder", "الاسم");
    $("#email").attr("placeholder", "البريد الالكتروني");
    $("#phone").attr("placeholder", "رقم التواصل");
    $("#message").attr("placeholder", "الرسالة");
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    var str = this.valueOf();
    return true;
}

function ConvertDates(dateTobeChanged, fromCalender, toCalender) {
    var calender = $.calendars.instance(fromCalender);
    var dateToBeChanged = calender.parseDate("dd/mm/yyyy", dateTobeChanged);
    calender = $.calendars.instance(toCalender);
    var changedDate = calender.fromJD(dateToBeChanged.toJD());
    return calender.formatDate("dd/mm/yyyy", changedDate);
}
function HijriToGregorian(arabicCalendar, englishCalendar) {
    if ($(arabicCalendar).val() == "") {
        $(englishCalendar).val("");
    }
    else {
        var splittedDate = $(arabicCalendar).val().split('/');
        $(arabicCalendar).val(splittedDate[2] + '/' + splittedDate[1] + '/' + splittedDate[0]);
        var dateToBeChanged = $(arabicCalendar).val();
        var newDate = ConvertDates(dateToBeChanged, 'islamic', 'gregorian');
        $(englishCalendar).val(newDate);
    }
}
function GregorianToHijri(englishCalendar,arabicCalendar) {
    if ($(englishCalendar).val() == "") {
        $(arabicCalendar).val("");
    }
    else {
        var dateToBeChanged = $(englishCalendar).val();
        var newDate = ConvertDates(dateToBeChanged, 'gregorian', 'islamic');
        $(arabicCalendar).val(newDate);
    }
}
$(document).ready(function () {
    $('.select2me').select2({
        placeholder: "Select"
    });
    //$(".datepickerArabic").mask('99/99/9999');
    //$.datepicker.setDefaults($.datepicker.regional['en-US']);
    $(".dateFormatter").mask('99/99/9999');
    $(".datepickerGregorian").mask('99/99/9999');
    $(".datepickerGregorian").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        yearRange: "1950:2050"
    });
    // Arabic Date Picker
    var calendar = $.calendars.instance('islamic');
    $('.datepickerArabic').calendarsPicker({
        calendar: calendar,
        onSelect: function () {
            $(this).change();
        }
    });
});





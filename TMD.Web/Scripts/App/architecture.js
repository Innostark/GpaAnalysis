﻿//show toast on new item created or updated based on url parameter
$(function () {
    var messageVm = $("#Message").val();
    if ($("#IsSaved").val()) {
        if (messageVm !== '' && messageVm !== "" && messageVm !== null && messageVm !== undefined) {
            toastr.success(messageVm);
            //var m = $(".alert-success");
            //m.children("span").text(messageVm);
            //m.show();
        }
    }
    else if ($("#IsUpdated").val()) {
        if (messageVm !== '' && messageVm !== "" && messageVm !== null && messageVm !== undefined) {
            toastr.success(messageVm);
            //var m = $(".alert-success");
            //m.children("span").text(messageVm);
            //m.show();
        }
    }
    else if ($("#IsError").val()) {
        if (messageVm !== '' && messageVm !== "" && messageVm !== null && messageVm !== undefined) {
            toastr.error(messageVm);
            //var m = $(".alert-error");
            //m.children("span").text(messageVm);
            //m.show();
        }
    }
    else if ($("#IsInfo").val()) {
        if (messageVm !== '' && messageVm !== "" && messageVm !== null && messageVm !== undefined) {
            toastr.info(messageVm);
            //var m = $(".alert-info");
            //m.children("span").text(messageVm);
            //m.show();
        }
    }
    else {

    }

    $("#Message").val('');
});
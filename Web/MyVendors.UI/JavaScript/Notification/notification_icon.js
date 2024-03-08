/// <reference path="notification_icon.js" />
var counter;
$(document).ready(function () {
    $("#noti").click(function (e) {
        e.stopPropagation();
        if (counter > 0) {
            $('.noti-content').show();
        }
    });
    // hide notifications
    $('html').click(function () {
        $('.noti-content').hide();
        $('.noti-top-arrow').hide();

    });
});

$(document).ready(function () {
    getNotification();
});


function getNotification() {
    $.ajax({
        type: "GET",
        url: API_URL + "api/APInotificationlist/",
        dataType: "json",
        success: function (data) {
            if (data.noticounter > 0) {
                $("#noticount").text(data.noticounter);
                counter = data.noticounter;
                for (var i = 0; i < data.responsetext.length; i++) {
                    $("#notiContent").append("<li id='vendor" + i + "' onclick='getid($(this))'><span class='icon-circle-filled' style='color:blueviolet;position: relative;margin-left: -23px;'></span>&nbsp;&nbsp;&nbsp;From : " + data.responsetext[i].VendoreName
                     + "<br/>&nbsp;Payment is Pending for Order.<hr style='margin-left:-25px;width:288px'/></li>");
                }
            }
            else {
                $("#noticount").hide();
                $("#notiContent").hide();
                $('.noti-top-arrow').hide();
            }

        },
        error: function (error) {
            //alert("error");
        }
    });
}


function getid(e) {
    var listis = e.attr('id');
    if (counter == 0) {
        counter = null;
        $("#noticount").hide();
        $('.noti-top-arrow').hide();
        $('.noti-content').hide();
        $("#notiContent").hide();
        $('.noti-top-arrow').hide();

    }
    else if (counter > 0) {
        counter = counter - 1;
        $("#noticount").text(counter);
        $("#" + e.attr('id')).hide();
        counter == 0 ? $("#noticount").hide() : $("#noticount").show();
    }
    else {
        $("#noticount").hide();
        $('.noti-top-arrow').hide();
        $('.noti-content').hide();
        $("#notiContent").hide();
        $('.noti-top-arrow').hide();
    }
}

// redis and rabbitmq code Added By :Dhrupti Thesiya

//function getTransactionReacord() {

//    $.ajax({
//        type: "GET",
//        url: API_URL + "api/APInotificationlist/",
//        dataType: "json",
//        success: function (data) {
//            if (data.noticounter > 0) {

//                $("#noticount").text(data.noticounter);
//                for (var i = 0; i < data.responsetext.length; i++) {

//                    $("#notiContent").append("<li id='vendor" + i + "' onclick='getid($(this))'><span class='icon-circle-filled' style='color:blueviolet;position: relative;margin-left: -23px;'></span>&nbsp;&nbsp;&nbsp;From : " + data.responsetext[i] + "<br/><hr style='margin-left:-25px;width:240px'/></li>");

//                }

//            }
//            else {
//                $("#noticount").hide();
//                $("#notiContent").hide();
//                $('.noti-top-arrow').hide();
//            }


//        },
//        error: function (error) {
//            //alert("error");
//        }
//    });
//}


//function getid(e) {
//    var li = e.attr('id');
//    $.ajax({
//        type: "GET",
//        url: API_URL + "api/APInotificationlist/" + e.attr('id'),
//        dataType: "json",
//        success: function (data) {
//            if (data.responsetext == 0) {
//                $("#noticount").hide();
//                $('.noti-top-arrow').hide();
//                $('.noti-content').hide();
//                $("#notiContent").hide();
//            }
//            else {
//                $("#noticount").text(data.responsetext);
//                $("#" + e.attr('id')).hide();
//            }
//            $('.noti-top-arrow').hide();

//        },
//        error: function (error) {
//            //alert("error");
//        }
//    });
//}



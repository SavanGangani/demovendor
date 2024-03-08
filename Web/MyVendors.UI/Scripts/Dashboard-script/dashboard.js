//Start:25/2/2022, Added By : Dhrupti Thesiya , Description : Java script functions for reset password 

setTimeout(function () { $("#load_screen").hide(); }, 0);

$("select:not([multiple])").kendoDropDownList();

$("#UserRoleDropdown").kendoDropDownList({
    popup: {
        appendTo: $(".userprofile-menu")
    }
});

//Kendo File Upload
$(document).ready(function () {
    $("#fileProfileImage").kendoUpload({
        showFileList: false,
        multiple: false,
        localization: { select: 'Select File' }
    });

    $("#tabstrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    $("#borederTabstrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "anuja put here your api",
                dataType: "jsonp"
            }
        },
    });

    $("#thumbnail_grid").kendoListView({
        dataSource: dataSource,
        selectable: "multiple",
        template: kendo.template($("#thumnail_template").html())
    });

});



function resetEvent() {
    document.getElementById("txtOldPassword").value = "";
    document.getElementById("txtNewPassword").value = "";
    document.getElementById("txtConfirmPassword").value = "";
    document.getElementById("oldpasswordValidateMessage").innerHTML = "";
    document.getElementById("newpasswordValidateMessage").innerHTML = "";
    document.getElementById("copasswordValidateMessage").innerHTML = "";
    document.getElementById("txtOldPassword").style.borderColor = "lightgrey";
    document.getElementById("txtNewPassword").style.borderColor = "lightgrey";
    document.getElementById("txtConfirmPassword").style.borderColor = "lightgrey";
}

function resetEventProfile() {
    document.getElementById("txtfname").value = "";
    document.getElementById("txtlname").value = "";
    document.getElementById("txtmobile").value = "";
    document.getElementById("mobileValidateMessage").innerHTML = "";
    document.getElementById("lastNameValidateMessage").innerHTML = "";
    document.getElementById("firstNameValidateMessage").innerHTML = "";
    document.getElementById("txtfname").style.borderColor = "lightgrey";
    document.getElementById("txtlname").style.borderColor = "lightgrey";
    document.getElementById("txtmobile").style.borderColor = "lightgrey";
}


function pswd() {
    var isValid = false;
    var passwordold = document.getElementById("txtOldPassword").value;
    var passwordnew = document.getElementById("txtNewPassword").value;

    // Old Password Validation
    if (passwordold == "") {
        document.getElementById("oldpasswordValidateMessage").style.color = "red";
        document.getElementById("txtOldPassword").style.borderColor = "red";
        document.getElementById("oldpasswordValidateMessage").innerHTML = "Please enter a password.";
        isValid = false;
    }

    // New Password Validation
    if (passwordnew == "") {
        document.getElementById("newpasswordValidateMessage").style.color = "red";
        document.getElementById("txtNewPassword").style.borderColor = "red";
        document.getElementById("newpasswordValidateMessage").innerHTML = "Please enter a password.";
        isValid = false;
    }
    else if (/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/.test(passwordnew)) {
        document.getElementById("txtNewPassword").style.borderColor = "lightgrey";
        document.getElementById("newpasswordValidateMessage").innerHTML = "";
        isValid = true;
    }
    else {
        document.getElementById("newpasswordValidateMessage").style.color = "red";
        document.getElementById("txtNewPassword").style.borderColor = "red";
        document.getElementById("newpasswordValidateMessage").innerHTML = "Please enter a valid Password(Password should be of special characters, numbers, Capital letter and small letter. Length must be 8 to 15 characters).";
        isValid = false;
    }
    var cpassword = document.getElementById("txtConfirmPassword").value;

    if (cpassword == "") {
        document.getElementById("copasswordValidateMessage").style.color = "red";
        document.getElementById("txtConfirmPassword").style.borderColor = "red";
        document.getElementById("copasswordValidateMessage").innerHTML = "Please confirm the password";
        isValid = false;
    }
    else if (passwordnew == cpassword) {
        document.getElementById("copasswordValidateMessage").innerHTML = "";
        document.getElementById("txtConfirmPassword").style.borderColor = "lightgrey";
        isValid = true;
    }
    else {
        document.getElementById("copasswordValidateMessage").style.color = "red";
        document.getElementById("txtConfirmPassword").style.borderColor = "red";
        document.getElementById("copasswordValidateMessage").innerHTML = "Password does not match";
        isValid = false;
    }
    return isValid;
}
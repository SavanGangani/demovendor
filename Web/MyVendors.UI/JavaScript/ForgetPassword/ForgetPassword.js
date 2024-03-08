function newpswd() {
    var password = document.getElementById("password").value;
    if (password == "") {
        //document.getElementById("password").focus();
        document.getElementById("passwordValidateMessage").style.color = "red";
        document.getElementById("password").style.borderColor = "red";
        document.getElementById("passwordValidateMessage").innerHTML = "Please enter a password.";
        return false;
    }

    else if (/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/.test(password)) {
        document.getElementById("password").style.borderColor = "lightgrey";
        document.getElementById("passwordValidateMessage").innerHTML = "";
        return true;
    }
    else {
        //document.getElementById("password").focus();
        document.getElementById("passwordValidateMessage").style.color = "red";
        document.getElementById("password").style.borderColor = "red";
        document.getElementById("passwordValidateMessage").innerHTML = "Please enter a valid Password(Password should be of special characters, numbers, Capital letter and small letter. Length must be 8 to 15 characters).";
        return false;
    }
}

function confirmpasswod() {
    var cpassword = document.getElementById("confirmPassword").value;
    var password = document.getElementById("password").value;
    if (cpassword == "") {
        document.getElementById("confirmPasswordValidateMessage").style.color = "red";
        document.getElementById("confirmPassword").style.borderColor = "red";
        document.getElementById("confirmPasswordValidateMessage").innerHTML = "Please confirm the password";
        return false;
    }
    else if (password == cpassword) {
        document.getElementById("confirmPasswordValidateMessage").innerHTML = "";
        document.getElementById("confirmPassword").style.borderColor = "lightgrey";

        return true;
    }
    else {
        document.getElementById("confirmPassword").focus();
        document.getElementById("confirmPasswordValidateMessage").style.color = "red";
        document.getElementById("confirmPassword").style.borderColor = "red";
        document.getElementById("confirmPasswordValidateMessage").innerHTML = "Password does not match";
        return false;
    }
}


// ***************************************
// On Submit Validation
// ***************************************

function onsubmitValidation() {
    var chkpasword = newpswd();
    var chkcpassword = confirmpasswod();

    if (chkpasword && chkcpassword) {
        return true;
    }
    else {
        return false;
    }
}

// ******************************************************************************
// If all validations are true than Password Reset is successfull
// ******************************************************************************
function validateData() {
    if (onsubmitValidation() == true) {
        var url = window.location.href;
        UpdateForgotPassword(url.toString().replace(UI_URL + "Login/ForgotReset?Token=", ""));
    }
}

function functionsuccess() {
    document.getElementById("success-box").style.display = "block";

    $("#close-success").click(function (event) { document.getElementById("success-box").style.display = "none"; })
    id = "close-success"
}

function functionalert() {
    document.getElementById("alert-box").style.display = "block";

    $("#close-alert").click(function (event) { document.getElementById("alert-box").style.display = "none"; })
    id = "close-alert"
}

// ***************************************
// AJAX 
// ***************************************
function UpdateForgotPassword(tokenValue) {

    var arr = tokenValue.split('=');
  
    var UpdateForgotPassword = { UpdateForgotPassword: $('#password').val(), Token: arr[1] }

    var settings =
        {
            "url": API_URL + "api/ForgotPasswordApi/UpdatePassword",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(UpdateForgotPassword),
        };
    $.ajax(settings).done(function (response) {
        if (response == 1) {
            logger("Login", "Forgot Password")
            functionsuccess()
            window.location = "/Login/Login"
        } else {
            functionalert()
        }
    });

}

function logger(page, operation) {
    var Login = {
        Email: page,
        Password: operation
    };

    $.ajax({
        sync: true,
        type: 'POST',
        url: '/Logger/post',
        data: JSON.stringify(Login),
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8'
    });

}
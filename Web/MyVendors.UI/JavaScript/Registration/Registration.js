$(document).ready(function () {
    $("#btnagree").on('click', function () {
        $("#acceptTs").prop("checked", true);
        $('#btn_regi').prop('disabled', false);
    });

});

function firstname() {
    var fname = document.getElementById("firstName").value;
    var firstname = /^[a-zA-Z]+$/;
    //var firstname = /^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{1,10}$/;

    if (fname == "") {
        document.getElementById("firstNameValidateMessage").style.color = "red";
        document.getElementById("firstName").style.borderColor = "red";
        document.getElementById("firstNameValidateMessage").innerHTML = "Please enter a first name.";
        return false;
    }
    else if (firstname.test(fname) && fname.length >= 2 && fname.length <= 20) {
        document.getElementById("firstName").style.borderColor = "lightgrey";
        document.getElementById("firstNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("firstNameValidateMessage").style.color = "red";
        document.getElementById("firstName").style.borderColor = "red";
        document.getElementById("firstNameValidateMessage").innerHTML = "Please enter a valid first name(Alpha characters only and whitespace not allow).";
        return false;
    }

}

function lastname() {

    var lname = document.getElementById("lastName").value;
    var lastname = /^[a-zA-Z]+$/;
    //var lastname = /^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{1,10}$/;
    if (lname == "") {

        document.getElementById("lastNameValidateMessage").style.color = "red";
        document.getElementById("lastName").style.borderColor = "red";
        document.getElementById("lastNameValidateMessage").innerHTML = "Please enter a last name.";
        return false;

    }
    else if (lastname.test(lname) && lname.length >= 2 && lname.length <= 20) {
        document.getElementById("lastName").style.borderColor = "lightgrey";
        document.getElementById("lastNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("lastNameValidateMessage").style.color = "red";
        document.getElementById("lastName").style.borderColor = "red";
        document.getElementById("lastNameValidateMessage").innerHTML = "Please enter a valid last name(Alpha characters only and whitespace not allow).";
        return false;
    }

}

function email() {
    var mail = document.getElementById("emailId").value;
    var name = mail.replace(/@.*$/, "");
    var check = /^[0-9]*$/.test(name);

    //var emailExp = /^[a-zA-Z0-9][a-zA-Z0-9/.]+\@@(([a-zA-Z0-9])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var emailExp = /^[a-zA-Z0-9][a-zA-Z0-9/.]+\@((casepoint)+\.)+(in)+$/;
    var emailExp1 = /^[a-zA-Z0-9][a-zA-Z0-9/.]+\@((clarent)+\.)+(institute)+$/;
    if (mail == "") {
        //document.getElementById("emailId").focus();
        document.getElementById("emailIdValidateMessage").style.color = "red";
        document.getElementById("emailId").style.borderColor = "red";
        document.getElementById("emailIdValidateMessage").innerHTML = "Please enter a email address.";
        console.log("empty email");
        return false;
    }
    else if (emailExp.test(mail) && !check || emailExp1.test(mail) && !check) {
        // if (/^([a-zA-Z0-9])+.+\@@(([a-zA-Z])+\.)+([a-zA-Z0-9]{2,4})+$/.test(mail) && !check) {
        //if (/^[a-zA-Z0-9+_.]+@@[a-zA-Z0-9.-]+$/.test(mail) && !check) {
        document.getElementById("emailId").style.borderColor = "lightgrey";
        document.getElementById("emailIdValidateMessage").innerHTML = "";
        console.log("reguler wxp email");
        return true;
    }
    else {
        //document.getElementById("emailId").focus();
        document.getElementById("emailIdValidateMessage").style.color = "red";
        document.getElementById("emailId").style.borderColor = "red";
        document.getElementById("emailIdValidateMessage").innerHTML = "Please enter a valid email address(Example: example@casepoint.in OR example@clarent.institute).";
        console.log("else part");
        return false;

    }

}

function pswd() {
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


function checkCondition() {
    var check = document.getElementById("acceptTs");
    if (check.checked) {

        document.getElementById("checkValidateMessage").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("acceptTs").focus();
        document.getElementById("checkValidateMessage").style.color = "red";
        document.getElementById("acceptTs").style.border = "1px solid red";
        document.getElementById("checkValidateMessage").innerHTML = "<br>Please accept the Terms of Use and Privacy Policy.";
        return false;
    }
}


// button enable and disable
var checker = document.getElementById('acceptTs');
var sendbtn = document.getElementById('btn_regi');
checker.onchange = function () {
    if (this.checked)
        sendbtn.disabled = false;
    else
        sendbtn.disabled = true;
};



function validate() {
    var chkfname = firstname();
    var chklname = lastname();
    var chkemail = email();
    var chkpasword = pswd();
    var chkcpassword = confirmpasswod();
    var chkCheck = checkCondition();
    //var enablebtn=enablebutton();



    // validation for check box

    if (chkCheck && chkfname && chklname && chkemail && chkpasword && chkcpassword) {
        AddUser();

    }
    else {
        return false;
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



function AddUser() {
    var RegData = {
        Fname: $('#firstName').val(),
        Lname: $('#lastName').val(),
        Email: $('#emailId').val(),
        Password: $('#password').val()
    };
    $.ajax({
        async: true,
        type: 'POST',
        url: API_URL + 'api/Registration/',
        data: JSON.stringify(RegData),
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response.Success) {
                functionsuccess()
                document.getElementById("btn_regi").innerHTML = "Please Wait...";
                window.location = '/Login/Login'
            }
            else {
                functionalert()
            }
        }, error: function (error) {
        }
    });
}
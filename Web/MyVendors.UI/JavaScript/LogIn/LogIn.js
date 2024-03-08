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
        document.getElementById("emailIdValidateMessage").innerHTML = "*Please enter a email address.";
        //console.log("empty email");
        return false;
    }
    else if (emailExp.test(mail) && !check || emailExp1.test(mail) && !check) {
        // if (/^([a-zA-Z0-9])+.+\@@(([a-zA-Z])+\.)+([a-zA-Z0-9]{2,4})+$/.test(mail) && !check) {
        //if (/^[a-zA-Z0-9+_.]+@@[a-zA-Z0-9.-]+$/.test(mail) && !check) {
        document.getElementById("txtemailsend").value = document.getElementById("emailId").value;
        document.getElementById("emailIdValidateMessage").innerHTML = "";
        document.getElementById("emailId").style.borderColor = "#dcdfe5";
        //console.log("reguler wxp email");
        return true;
    }
    else {
        //document.getElementById("emailId").focus();
        document.getElementById("emailIdValidateMessage").style.color = "red";
        document.getElementById("emailId").style.borderColor = "red";
        document.getElementById("emailIdValidateMessage").innerHTML = "*Please enter a valid email address(Example: example@casepoint.in OR example@clarent.institute).";
        //console.log("else part");
        return false;

    }

}

function functionalert() {
    document.getElementById("alert-box").style.display = "block";

    $("#close-alert").click(function (event) { document.getElementById("alert-box").style.display = "none"; })
    id = "close-alert"
}

function functionalert_user() {
    document.getElementById("alert-box-user").style.display = "block";

    $("#close-alert-user").click(function (event) { document.getElementById("alert-box-user").style.display = "none"; })
    id = "close-alert-user"
}

function functionsuccess() {
    document.getElementById("success-box").style.display = "block";

    $("#close-success").click(function (event) { document.getElementById("success-box").style.display = "none"; })
    id = "close-success"
}

function loginsuccess() {
    document.getElementById("success-boxlogin").style.display = "block";

    $("#close-successlogin").click(function (event) { document.getElementById("success-boxlogin").style.display = "none"; })
    id = "close-successlogin"

}

function pswd() {
    var password = document.getElementById("password").value;
    if (password == "") {
        //document.getElementById("password").focus();
        document.getElementById("passwordValidateMessage").style.color = "red";
        document.getElementById("password").style.borderColor = "red";
        document.getElementById("passwordValidateMessage").innerHTML = "*Please enter a password.";
        return false;
    }
    else {
        document.getElementById("passwordValidateMessage").innerHTML = "";
        document.getElementById("ErrorMessage").innerHTML = "";
        document.getElementById("emailId").style.borderColor = "#dcdfe5";
        document.getElementById("password").style.borderColor = "#dcdfe5";
        return true;
    }

}
function error(msg) {
    document.getElementById("emailId").style.borderColor = "red";
    document.getElementById("password").style.borderColor = "red";
    document.getElementById("ErrorMessage").style.color = "red";
    document.getElementById("ErrorMessage").innerHTML = msg;
}

function rememberme() {
    var checkValue = document.getElementById("rememeberme");
    if (checkValue.checked) {
        checkValue.setAttribute("value", "true");
    }
    else {
        checkValue.setAttribute("value", "false");
    }

}

function checkCondition() {
    var check = document.getElementById("acceptTs");
    if (check.checked) {

        document.getElementById("checkValidateMessage").innerHTML = "";
        document.getElementById("acceptTs").setAttribute("style", "border-color: #dcdfe5")
        return true;
    }
    else {
        //document.getElementById("rememeberMe").focus();
        document.getElementById("checkValidateMessage").style.color = "red";
        document.getElementById("acceptTs").setAttribute("style", "border-color: red");
        document.getElementById("checkValidateMessage").innerHTML = "<br>*Please Accept the License agreement.";
        return false;
    }
}
function validate() {

    var chkemail = email();
    var chkpasword = pswd();
    var chkCheck = checkCondition();

    if (chkCheck && chkemail && chkpasword) {
        Login()
        return true;
    }
    else {

        return false;
    }
}


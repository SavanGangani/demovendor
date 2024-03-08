
//Start:25/2/2022, Added By : Dhrupti Thesiya , Description : Java script functions for user edit profile

function name() {
    var fname = document.getElementById("txtfname").value;
    var mobilenumber = document.getElementById("txtmobile").value;
    var firstname = /^[a-zA-Z]+$/;
    var isValid = false;

    if (fname == "") {
        document.getElementById("firstNameValidateMessage").style.color = "red";
        document.getElementById("txtfname").style.borderColor = "red";
        document.getElementById("firstNameValidateMessage").innerHTML = "Please enter a first name.";
        isValid = false;
    }
    else if (firstname.test(fname) && fname.length >= 2 && fname.length <= 20) {
        document.getElementById("txtfname").style.borderColor = "lightgrey";
        document.getElementById("firstNameValidateMessage").innerHTML = "";
        isValid = true;
    }
    else {
        document.getElementById("firstNameValidateMessage").style.color = "red";
        document.getElementById("txtfname").style.borderColor = "red";
        document.getElementById("firstNameValidateMessage").innerHTML = "Please enter a valid first name(Alpha characters only and whitespace not allow).";
        isValid = false;
    }

    //Validate Last Name
    var lname = document.getElementById("txtlname").value;
    var lastname = /^[a-zA-Z]+$/;

    if (lname == "") {
        document.getElementById("lastNameValidateMessage").style.color = "red";
        document.getElementById("txtlname").style.borderColor = "red";
        document.getElementById("lastNameValidateMessage").innerHTML = "Please enter a last name.";
        isValid = false;
    }
    else if (lastname.test(lname) && lname.length >= 2 && lname.length <= 20) {
        document.getElementById("txtlname").style.borderColor = "lightgrey";
        document.getElementById("lastNameValidateMessage").innerHTML = "";
        isValid = true;
    }
    else {
        document.getElementById("lastNameValidateMessage").style.color = "red";
        document.getElementById("txtlname").style.borderColor = "red";
        document.getElementById("lastNameValidateMessage").innerHTML = "Please enter a valid last name(Alpha characters only and whitespace not allow).";
        isValid = false;
    }


    // mobile number validation

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;
    if (mobilenumber != "") {
        if (regx.test(mobilenumber)) {
            console.log(mobilenumber);
            document.getElementById("txtmobile").style.borderColor = "lightgrey";
            document.getElementById("mobileValidateMessage").innerHTML = "";
            isValid = true;
        }
        else {
            document.getElementById("mobileValidateMessage").style.color = "red";
            document.getElementById("txtmobile").style.borderColor = "red";
            document.getElementById("mobileValidateMessage").innerHTML = "Please enter a valid mobile number.";
            isValid = false;
        }
    }
    return isValid;
}

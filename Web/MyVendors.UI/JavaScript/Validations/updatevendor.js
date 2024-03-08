//function upselectCategoryDropDown() {
//    var dropDownVal = document.getElementById("upddlcategory").value;
//    //console.log(dropDownVal);
//    if (dropDownVal == "") {
//        //console.log("false section");  
//        document.getElementById("upentercatnameValidateMessage").innerHTML = "Please select category name";
//        document.getElementById("upentercatnameValidateMessage").style.color = "red";
//        return false;
//    }
//    else {
//        //console.log("true section");
//        document.getElementById("upentercatnameValidateMessage").innerHTML = "";
//        //document.getElementById("upSelectSectionlist").style.borderColor = "#dcdfe5";
//        return true;
//    }

//}

//function upselectSubCategoryDropDown() {
//    var dropdown = document.getElementById("upddlsubcategory").value;

//    if (dropdown == "") {
//        document.getElementById("upentersubcatnameValidateMessage").innerHTML = "Please select Sub Category Name";
//        document.getElementById("upentersubcatnameValidateMessage").style.color = "red";
//        return false;
//    }
//    else {
//        document.getElementById("upentersubcatnameValidateMessage").innerHTML = "";
//        return true;
//    }
//}

function upvendornamevalidate() {
    var vname = document.getElementById("upentervendorname").value;
    var vendorname = /^[a-zA-Z .]+$/;
    if (vname == "") {
        document.getElementById("upvendorNameValidateMessage").style.color = "red";
        document.getElementById("upentervendorname").style.borderColor = "red";
        document.getElementById("upvendorNameValidateMessage").innerHTML = "Please enter Vendor Name.";
        return false;
    }
    else if (vendorname.test(vname) && vname.length >= 2 && vname.length <= 20) {
        document.getElementById("upentervendorname").style.borderColor = "lightgrey";
        document.getElementById("upvendorNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("upvendorNameValidateMessage").style.color = "red";
        document.getElementById("upentervendorname").style.borderColor = "red";
        document.getElementById("upvendorNameValidateMessage").innerHTML = "Please enter a valid Vendor Name(Alpha characters only).";
        return false;
    }
}

function upmobilevendorvalidate() {

    var phone = document.getElementById("uptxtmobileno").value;

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;

    if (phone == "") {
        document.getElementById("upmobileValidateMessage").style.color = "red";
        document.getElementById("uptxtmobileno").style.borderColor = "red";
        document.getElementById("upmobileValidateMessage").innerHTML = "Please enter Mobile No.";
        return false;
    }
    else if (regx.test(phone)) {
        document.getElementById("uptxtmobileno").style.borderColor = "lightgrey";
        document.getElementById("upmobileValidateMessage").innerHTML = " ";
        return true;
    }
    else {
        document.getElementById("upmobileValidateMessage").style.color = "red";
        document.getElementById("uptxtmobileno").style.borderColor = "red";
        document.getElementById("upmobileValidateMessage").innerHTML = "Please enter a valid mobile number.";
        return false;
    }
}

function upemailvendorvalidate() {
    var mail = document.getElementById("uptxtvemail").value;

    var name = mail.replace(/@.*$/, "");
    var check = /^[0-9]*$/.test(name);
    var emailExp = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/


    if (mail == "") {
        document.getElementById("uptxtvemail").focus();
        document.getElementById("upemailIdValidateMessage").style.color = "red";
        document.getElementById("uptxtvemail").style.borderColor = "red";
        document.getElementById("upemailIdValidateMessage").innerHTML = "Please enter email address.";
        //console.log("empty email");
        return false;
    }
    else if (emailExp.test(mail) && !check) {
        // if (/^([a-zA-Z0-9])+.+\@@(([a-zA-Z])+\.)+([a-zA-Z0-9]{2,4})+$/.test(mail) && !check) {
        //if (/^[a-zA-Z0-9+_.]+@@[a-zA-Z0-9.-]+$/.test(mail) && !check) {
        document.getElementById("uptxtvemail").style.borderColor = "lightgrey";
        document.getElementById("upemailIdValidateMessage").innerHTML = "";
        //console.log("reguler wxp email");
        return true;
    }
    else {

        document.getElementById("upemailIdValidateMessage").style.color = "red";
        document.getElementById("uptxtvemail").style.borderColor = "red";
        document.getElementById("upemailIdValidateMessage").innerHTML = "Please enter a valid email address";
        //(Example: example@casepoint.in OR example@clarent.institute).
        //console.log("else part");
        return false;

    }

}

function upaddressvendorvalidate() {
    var vadd = document.getElementById("uptxtaddress").value;
    var vaddress = /^[a-zA-Z0-9\s,'-]+$/;
    //var firstname = /^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{1,10}$/;

    if (vadd == "") {
        document.getElementById("upaddressValidateMessage").style.color = "red";
        document.getElementById("uptxtaddress").style.borderColor = "red";
        document.getElementById("upaddressValidateMessage").innerHTML = "Please enter address.";
        return false;
    }
    else if (vaddress.test(vadd) && vadd.length >= 2 && vadd.length <= 40) {
        document.getElementById("uptxtaddress").style.borderColor = "lightgrey";
        document.getElementById("upaddressValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("upaddressValidateMessage").style.color = "red";
        document.getElementById("uptxtaddress").style.borderColor = "red";
        document.getElementById("upaddressValidateMessage").innerHTML = "Please enter a valid Vendor Address(Alpha characters only and whitespace not allow).";
        return false;
    }
}

function upgstnovendorvalidate() {
    var gstno = document.getElementById("uptxtgstno").value;
    var gstnumber = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;

    if (gstno == "") {
        document.getElementById("upgstValidateMessage").style.color = "red";
        document.getElementById("uptxtgstno").style.borderColor = "red";
        document.getElementById("upgstValidateMessage").innerHTML = "Please enter valid GST No.";
        return false;
    }
    else if (gstnumber.test(gstno) && gstno.length >= 2 && gstno.length <= 15) {
        document.getElementById("uptxtgstno").style.borderColor = "lightgrey";
        document.getElementById("upgstValidateMessage").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("upgstValidateMessage").style.color = "red";
        document.getElementById("uptxtgstno").style.borderColor = "red";
        document.getElementById("upgstValidateMessage").innerHTML = "Please enter a valid GST No.(Alpha Characters and Numbers Only)";
        return false;
    }
}

function upaccountholdernamevalidate() {
    var holdername = document.getElementById("uptxtaholdername").value;
    var aholdername = /^[a-zA-Z .]+$/;

    if (holdername == "") {
        document.getElementById("upholderNameValidateMessage").style.color = "red";
        document.getElementById("uptxtaholdername").style.borderColor = "red";
        document.getElementById("upholderNameValidateMessage").innerHTML = "Please enter Account Holder Name.";
        return false;
    }
    else if (aholdername.test(holdername) && holdername.length >= 2 && holdername.length <= 20) {
        document.getElementById("uptxtaholdername").style.borderColor = "lightgrey";
        document.getElementById("upholderNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("upholderNameValidateMessage").style.color = "red";
        document.getElementById("uptxtaholdername").style.borderColor = "red";
        document.getElementById("upholderNameValidateMessage").innerHTML = "Please enter a valid Account Holder Name(Alpha characters only).";
        return false;
    }
}

function upaccountnovalidate() {
    var accno = document.getElementById("uptxtaccountnumber").value;
    var accountno = /^[0-9]{9,18}$/

    if (accno == "") {
        document.getElementById("upbankaccValidateMessage").style.color = "red";
        document.getElementById("uptxtaccountnumber").style.borderColor = "red";
        document.getElementById("upbankaccValidateMessage").innerHTML = "Please enter Account Number.";
        return false;
    }
    else if (accountno.test(accno)) {
        document.getElementById("uptxtaccountnumber").style.borderColor = "lightgrey";
        document.getElementById("upbankaccValidateMessage").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("upbankaccValidateMessage").style.color = "red";
        document.getElementById("uptxtaccountnumber").style.borderColor = "red";
        document.getElementById("upbankaccValidateMessage").innerHTML = "Please enter a valid Account Number";
        return false;
    }
}

function upbanknamevendorvalidate() {
    var bname = document.getElementById("uptxtbankname").value;
    var bankname = /^[a-zA-Z .]+$/;

    if (bname == "") {
        document.getElementById("upbankNameValidateMessage").style.color = "red";
        document.getElementById("uptxtbankname").style.borderColor = "red";
        document.getElementById("upbankNameValidateMessage").innerHTML = "Please enter Bank Name.";
        return false;
    }
    else if (bankname.test(bname) && bname.length >= 2 && bname.length <= 20) {
        document.getElementById("uptxtbankname").style.borderColor = "lightgrey";
        document.getElementById("upbankNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("upbankNameValidateMessage").style.color = "red";
        document.getElementById("uptxtbankname").style.borderColor = "red";
        document.getElementById("upbankNameValidateMessage").innerHTML = "Please enter a valid Bank Name(Alpha characters only).";
        return false;
    }
}

function upifsccodevendorvalidate() {
    var ifsccode = document.getElementById("uptxtifsccode").value;
    var IFSCCode = /^[A-Za-z]{4}\d{7}$/;

    if (ifsccode == "") {
        document.getElementById("upifscValidateMesage").style.color = "red";
        document.getElementById("uptxtifsccode").style.borderColor = "red";
        document.getElementById("upifscValidateMesage").innerHTML = "Please enter IFSC Code";
        return false;
    }
    else if (IFSCCode.test(ifsccode) && ifsccode.length >= 2 && ifsccode.length <= 20) {
        document.getElementById("uptxtifsccode").style.borderColor = "lightgrey";
        document.getElementById("upifscValidateMesage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("upifscValidateMesage").style.color = "red";
        document.getElementById("uptxtifsccode").style.borderColor = "red";
        document.getElementById("upifscValidateMesage").innerHTML = "Please enter a valid Bank Name(Alpha characters only).";
        return false;
    }
}


function validateUpdateVendorForm() {

    //var subcatdrop = selectSubCategoryDropDown();
    var name = upvendornamevalidate();
    var mobile = upmobilevendorvalidate();
    var email = upemailvendorvalidate();
    var address = upaddressvendorvalidate();
    var gst = upgstnovendorvalidate();
    var holdername = upaccountholdernamevalidate();
    var accountno = upaccountnovalidate();
    var bank_name = upbanknamevendorvalidate();
    var ifsccode = upifsccodevendorvalidate();
    //var file = upfileExtention();

    if (name && mobile && email && address && gst && holdername && accountno && bank_name && ifsccode) {
        updateVendors();
    }
    else {

        return false;
    }
}



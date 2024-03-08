$(document).ready(function () {
    $("#resetvendor").click(function () {
        //$("#ddlsubcategory").data("kendoDropDownList").value(-1);
        //$("#ddlcategory").data("kendoDropDownList").value(-1);
        document.getElementById("entervendorname").style.borderColor = "lightgrey";
        document.getElementById("vendorNameValidateMessage").innerHTML = "";
        document.getElementById("txtvemail").style.borderColor = "lightgrey";
        document.getElementById("emailIdValidateMessage").innerHTML = "";
        document.getElementById("txtmobileno").style.borderColor = "lightgrey";
        document.getElementById("mobileValidateMsg").innerHTML = " ";
        document.getElementById("txtaddress").style.borderColor = "lightgrey";
        document.getElementById("addressValidateMessage").innerHTML = "";
        document.getElementById("txtgstno").style.borderColor = "lightgrey";
        document.getElementById("gstValidateMessage").innerHTML = "";
        document.getElementById("txtaholdername").style.borderColor = "lightgrey";
        document.getElementById("holderNameValidateMessage").innerHTML = "";
        document.getElementById("txtaccountnumber").style.borderColor = "lightgrey";
        document.getElementById("bankaccValidateMessage").innerHTML = "";
        document.getElementById("txtbankname").style.borderColor = "lightgrey";
        document.getElementById("bankNameValidateMessage").innerHTML = "";
        document.getElementById("txtifsccode").style.borderColor = "lightgrey";
        document.getElementById("ifscValidateMesage").innerHTML = "";


    });
});





function selectCategoryDropDown() {
    //var dropDownVal = document.getElementById("ddlcategory").value;
    //if (dropDownVal == "") {;  
    //    document.getElementById("entercatnameValidateMessage").innerHTML = "Please select category name";
    //    document.getElementById("entercatnameValidateMessage").style.color = "red";
    //    return false;
    //}
    //else {
    //    document.getElementById("entercatnameValidateMessage").innerHTML = "";
    //    //document.getElementById("SelectSectionlist").style.borderColor = "#dcdfe5";
    return true;
    //}

}

function selectSubCategoryDropDown() {
    var dropdown = document.getElementById("ddlsubcategory").value;

    if (dropdown == "") {
        document.getElementById("entersubcatnameValidateMessage").innerHTML = "Please select Sub Category Name";
        document.getElementById("entersubcatnameValidateMessage").style.color = "red";
        return false;
    }
    else {
        document.getElementById("entersubcatnameValidateMessage").innerHTML = "";
        return true;
    }
}

function vendornamevalidate() {
    var vname = document.getElementById("entervendorname").value;
    var vendorname = /^[a-zA-Z .]+$/;
    if (vname == "") {
        document.getElementById("vendorNameValidateMessage").style.color = "red";
        document.getElementById("entervendorname").style.borderColor = "red";
        document.getElementById("vendorNameValidateMessage").innerHTML = "Please enter Vendor Name.";
        return false;
    }
    else if (vendorname.test(vname) && vname.length >= 2 && vname.length <= 20) {
        document.getElementById("entervendorname").style.borderColor = "grey";
        document.getElementById("vendorNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("vendorNameValidateMessage").style.color = "red";
        document.getElementById("entervendorname").style.borderColor = "red";
        document.getElementById("vendorNameValidateMessage").innerHTML = "Please enter a valid Vendor Name(Alpha characters only).";
        return false;
    }
}

function mobilevendorvalidate() {

    var phone = document.getElementById("txtmobileno").value;

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;

    if (phone == "") {
        document.getElementById("mobileValidateMsg").style.color = "red";
        document.getElementById("txtmobileno").style.borderColor = "red";
        document.getElementById("mobileValidateMsg").innerHTML = "Please enter Mobile No.";
        return false;
    }
    else if (regx.test(phone)) {
        document.getElementById("txtmobileno").style.borderColor = "grey";
        document.getElementById("mobileValidateMsg").innerHTML = " ";
        return true;
    }
    else {
        document.getElementById("mobileValidateMsg").style.color = "red";
        document.getElementById("txtmobileno").style.borderColor = "red";
        document.getElementById("mobileValidateMsg").innerHTML = "Please enter a valid mobile number.";
        return false;
    }
}

function emailvendorvalidate() {
    var mail = document.getElementById("txtvemail").value;

    var name = mail.replace(/@.*$/, "");
    var check = /^[0-9]*$/.test(name);
    var emailExp = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/


    if (mail == "") {
        document.getElementById("txtvemail").focus();
        document.getElementById("emailIdValidateMessage").style.color = "red";
        document.getElementById("txtvemail").style.borderColor = "red";
        document.getElementById("emailIdValidateMessage").innerHTML = "Please enter email address.";
        return false;
    }
    else if (emailExp.test(mail) && !check) {
        document.getElementById("txtvemail").style.borderColor = "grey";
        document.getElementById("emailIdValidateMessage").innerHTML = "";
        return true;
    }
    else {

        document.getElementById("emailIdValidateMessage").style.color = "red";
        document.getElementById("txtvemail").style.borderColor = "red";
        document.getElementById("emailIdValidateMessage").innerHTML = "Please enter a valid email address";
        return false;

    }

}

function addressvendorvalidate() {
    var vadd = document.getElementById("txtaddress").value;
    var vaddress = /^[a-zA-Z0-9\s,'-]+$/;


    if (vadd == "") {
        document.getElementById("addressValidateMessage").style.color = "red";
        document.getElementById("txtaddress").style.borderColor = "red";
        document.getElementById("addressValidateMessage").innerHTML = "Please enter address.";
        return false;
    }
    else if (vaddress.test(vadd) && vadd.length >= 2 && vadd.length <= 40) {
        document.getElementById("txtaddress").style.borderColor = "grey";
        document.getElementById("addressValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("addressValidateMessage").style.color = "red";
        document.getElementById("txtaddress").style.borderColor = "red";
        document.getElementById("addressValidateMessage").innerHTML = "Please enter a valid Vendor Address(Alpha characters only and whitespace not allow).";
        return false;
    }
}

function gstnovendorvalidate() {
    var gstno = document.getElementById("txtgstno").value;
    var gstnumber = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;

    if (gstno == "") {
        document.getElementById("gstValidateMessage").style.color = "red";
        document.getElementById("txtgstno").style.borderColor = "red";
        document.getElementById("gstValidateMessage").innerHTML = "Please enter valid GST No.";
        return false;
    }
    else if (gstnumber.test(gstno) && gstno.length >= 2 && gstno.length <= 15) {
        document.getElementById("txtgstno").style.borderColor = "grey";
        document.getElementById("gstValidateMessage").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("gstValidateMessage").style.color = "red";
        document.getElementById("txtgstno").style.borderColor = "red";
        document.getElementById("gstValidateMessage").innerHTML = "Please enter a valid GST No.(Alpha Characters and Numbers Only)";
        return false;
    }
}

function accountholdernamevalidate() {
    var holdername = document.getElementById("txtaholdername").value;
    var aholdername = /^[a-zA-Z .]+$/;

    if (holdername == "") {
        document.getElementById("holderNameValidateMessage").style.color = "red";
        document.getElementById("txtaholdername").style.borderColor = "red";
        document.getElementById("holderNameValidateMessage").innerHTML = "Please enter Account Holder Name.";
        return false;
    }
    else if (aholdername.test(holdername) && holdername.length >= 2 && holdername.length <= 20) {
        document.getElementById("txtaholdername").style.borderColor = "grey";
        document.getElementById("holderNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("holderNameValidateMessage").style.color = "red";
        document.getElementById("txtaholdername").style.borderColor = "red";
        document.getElementById("holderNameValidateMessage").innerHTML = "Please enter a valid Account Holder Name(Alpha characters only).";
        return false;
    }
}

function accountnovalidate() {
    var accno = document.getElementById("txtaccountnumber").value;
    var accountno = /^[0-9]{9,18}$/

    if (accno == "") {
        document.getElementById("bankaccValidateMessage").style.color = "red";
        document.getElementById("txtaccountnumber").style.borderColor = "red";
        document.getElementById("bankaccValidateMessage").innerHTML = "Please enter Account Number.";
        return false;
    }
    else if (accountno.test(accno)) {
        document.getElementById("txtaccountnumber").style.borderColor = "grey";
        document.getElementById("bankaccValidateMessage").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("bankaccValidateMessage").style.color = "red";
        document.getElementById("txtaccountnumber").style.borderColor = "red";
        document.getElementById("bankaccValidateMessage").innerHTML = "Please enter a valid Account Number";
        return false;
    }
}

function banknamevendorvalidate() {
    var bname = document.getElementById("txtbankname").value;
    var bankname = /^[a-zA-Z .]+$/;

    if (bname == "") {
        document.getElementById("bankNameValidateMessage").style.color = "red";
        document.getElementById("txtbankname").style.borderColor = "red";
        document.getElementById("bankNameValidateMessage").innerHTML = "Please enter Bank Name.";
        return false;
    }
    else if (bankname.test(bname) && bname.length >= 2 && bname.length <= 20) {
        document.getElementById("txtbankname").style.borderColor = "grey";
        document.getElementById("bankNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("bankNameValidateMessage").style.color = "red";
        document.getElementById("txtbankname").style.borderColor = "red";
        document.getElementById("bankNameValidateMessage").innerHTML = "Please enter a valid Bank Name(Alpha characters only).";
        return false;
    }
}

function ifsccodevendorvalidate() {
    var ifsccode = document.getElementById("txtifsccode").value;
    var IFSCCode = /^[A-Za-z]{4}\d{7}$/;

    if (ifsccode == "") {
        console.log("Anuja");
        document.getElementById("ifscValidateMesage").style.color = "red";
        document.getElementById("txtifsccode").style.borderColor = "red";
        document.getElementById("ifscValidateMesage").innerHTML = "Please enter IFSC Code";
        return false;
    }
    else if (IFSCCode.test(ifsccode) && ifsccode.length >= 2 && ifsccode.length <= 20) {
        document.getElementById("txtifsccode").style.borderColor = "grey";
        document.getElementById("ifscValidateMesage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("ifscValidateMesage").style.color = "red";
        document.getElementById("txtifsccode").style.borderColor = "red";
        document.getElementById("ifscValidateMesage").innerHTML = "Please enter a valid Bank Name(Alpha characters only).";
        return false;
    }
}


function validateVendorForm() {


    var catdrop = selectCategoryDropDown();
    //var subcatdrop = selectSubCategoryDropDown();
    var name = vendornamevalidate();
    var mobile = mobilevendorvalidate();
    var email = emailvendorvalidate();
    var address = addressvendorvalidate();
    var gst = gstnovendorvalidate();
    var holdername = accountholdernamevalidate();
    var accountno = accountnovalidate();
    var bank_name = banknamevendorvalidate();
    var ifsccode = ifsccodevendorvalidate();


    if (catdrop && name && mobile && email && address && gst && holdername && accountno && bank_name && ifsccode) {
        addVendors();
    }
    else {

        return false;
    }
}
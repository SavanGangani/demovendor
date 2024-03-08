function validateInvoiceData() {
    var gstno = invoicevalidate();
    var taxpreference = taxcpreferencevalidate();
    var noofservice = servicepro();
    var billamount = billamtvalidate();
    var bankname = banknamevalidate();
    var ifsccode = bankifcvalidate();
    var bankaccount = bankaccvalidate();
    var holdername = holdernamevalidate();
    if (gstno && taxpreference && noofservice && billamount && bankname && ifsccode && bankaccount && holdername)
        return true;
    else
        return false;
}

function invoicevalidate() {
    var gstno = document.getElementById("txtgstno").value;

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;
    if (phone != "") {
        if (regx.test(mobilenumber)) {
            console.log(phone);
            document.getElementById("txtgstno").style.borderColor = "lightgrey";
            document.getElementById("gstValidateMessage").innerHTML = "";
            return true;
        }
        else {
            return false;
        }
    }

    else {
        document.getElementById("gstValidateMessage").style.color = "red";
        document.getElementById("txtgstno").style.borderColor = "red";
        document.getElementById("gstValidateMessage").innerHTML = "Please enter a valid gst number.";
        return false;
    }


}

function taxcpreferencevalidate() {
    var txtno = document.getElementById("txttaxpre").value;
    var txt = /^[a-zA-Z]+$/;
    if (vname == "") {
        document.getElementById("taxValidateMessage").style.color = "red";
        document.getElementById("txttaxpre").style.borderColor = "red";
        document.getElementById("taxValidateMessage").innerHTML = "Please enter Taxt Preferences";
        return false;
    }
    else if (vendorname.test(vname) && vname.length >= 2 && vname.length <= 20) {
        document.getElementById("txttaxpre").style.borderColor = "lightgrey";
        document.getElementById("taxValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("taxValidateMessage").style.color = "red";
        document.getElementById("txttaxpre").style.borderColor = "red";
        document.getElementById("taxValidateMessage").innerHTML = "Please enter a valid Valid Name";
        return false;
    }
}

function servicepro() {


    function servicepro() {
        var service = document.getElementById("txtnoamc").value;

        var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;
        if (phone != "") {
            if (regx.test(mobilenumber)) {
                console.log(phone);
                document.getElementById("txtnoamc").style.borderColor = "lightgrey";
                document.getElementById("noofserviceMessage").innerHTML = "";
                return true;
            }
            else {
                return false;
            }
        }

        else {
            document.getElementById("noofserviceMessage").style.color = "red";
            document.getElementById("txtnoamc").style.borderColor = "red";
            document.getElementById("noofserviceMessage").innerHTML = "Please enter a proper service provider.";
            return false;
        }


    }



}


function billamtvalidate() {
    var bill = document.getElementById("txtbill").value;

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;
    if (billamt != "") {
        if (regx.test(billamount)) {
            console.log(bill);
            document.getElementById("txtbill").style.borderColor = "lightgrey";
            document.getElementById("billValidateMessage").innerHTML = "";
            return true;
        }
        else {
            return false;
        }
    }

    else {
        document.getElementById("billValidateMessage").style.color = "red";
        document.getElementById("txtbill").style.borderColor = "red";
        document.getElementById("billValidateMessage").innerHTML = "Please enter a valid mobile number.";
        return false;
    }


}


function banknamevalidate() {
    var bname = document.getElementById("txtbankname").value;
    var vendorname = /^[a-zA-Z]+$/;
    if (vname == "") {
        document.getElementById("bankNameValidateMessage").style.color = "red";
        document.getElementById("txtbankname").style.borderColor = "red";
        document.getElementById("bankNameValidateMessage").innerHTML = "Please enter bank Name.";
        return false;
    }
    else if (vendorname.test(vname) && vname.length >= 2 && vname.length <= 20) {
        document.getElementById("txtbankname").style.borderColor = "lightgrey";
        document.getElementById("bankNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("bankNameValidateMessage").style.color = "red";
        document.getElementById("txtbankname").style.borderColor = "red";
        document.getElementById("bankNameValidateMessage").innerHTML = "Please enter a valid Bank Name(Alpha characters only and whitespace not allow).";
        return false;
    }
}


function bankifcvalidate() {
    var bank = document.getElementById("txtifsccode").value;

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;
    if (bankn != "bankno") {
        if (regx.test(banknumber)) {
            console.log(bankn);
            document.getElementById("txtifsccode").style.borderColor = "lightgrey";
            document.getElementById("ifscValidateMesage").innerHTML = "";
            return true;
        }
        else {
            return false;
        }
    }

    else {
        document.getElementById("ifscValidateMesage").style.color = "red";
        document.getElementById("txtifsccode").style.borderColor = "red";
        document.getElementById("ifscValidateMesage").innerHTML = "Please enter bank number.";
        return false;
    }


}

function bankaccvalidate() {
    var bankacc = document.getElementById("txtaccountnumber").value;

    var regx = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/;
    if (bankacc != "") {
        if (regx.test(banknumber)) {
            console.log(bankacc);
            document.getElementById("txtaccountnumber").style.borderColor = "lightgrey";
            document.getElementById("bankaccValidateMessage").innerHTML = "";
            return true;
        }
        else {
            return false;
        }
    }

    else {
        document.getElementById("bankaccValidateMessage").style.color = "red";
        document.getElementById("txtaccountnumber").style.borderColor = "red";
        document.getElementById("bankaccValidateMessage").innerHTML = "Please enter a valid mobile number.";
        return false;
    }


}

function holdernamevalidate() {
    var hname = document.getElementById("txtholdername").value;
    var acchondname = /^[a-zA-Z]+$/;
    if (vname == "") {
        document.getElementById("holderNameValidateMessage").style.color = "red";
        document.getElementById("txtholdername").style.borderColor = "red";
        document.getElementById("holderNameValidateMessage").innerHTML = "Please enter Bank Holder Name.";
        return false;
    }
    else if (vendorname.test(vname) && vname.length >= 2 && vname.length <= 20) {
        document.getElementById("txtholdername").style.borderColor = "lightgrey";
        document.getElementById("holderNameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("holderNameValidateMessage").style.color = "red";
        document.getElementById("txtholdername").style.borderColor = "red";
        document.getElementById("holderNameValidateMessage").innerHTML = "Please enter a valid Bank Holder Name(Alpha characters only and whitespace not allow).";
        return false;
    }
}

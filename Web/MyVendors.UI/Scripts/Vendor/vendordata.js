function vendorname() {
    var vname = document.getElementById("entervendorname").value;
    var vendorname = /^[a-zA-Z]+$/;
    //var firstname = /^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{1,10}$/;

    if (vname == "") {
        document.getElementById("entervnameValidateMessage").style.color = "red";
        document.getElementById("entervendorname").style.borderColor = "red";
        document.getElementById("entervnameValidateMessage").innerHTML = "Please enter a vendor name.";
        return false;
    }
    else if (vendorname.test(vname) && vname.length >= 2 && vname.length <= 20) {
        document.getElementById("entervendorname").style.borderColor = "lightgrey";
        document.getElementById("entervnameValidateMessage").innerHTML = "";
        return true;
    }

    else {
        document.getElementById("entervnameValidateMessage").style.color = "red";
        document.getElementById("vname").style.borderColor = "red";
        document.getElementById("entervnameValidateMessage").innerHTML = "Please enter a vendor name(Alpha characters only and whitespace not allow).";
        return false;
    }

}

//insert validtion order

function selectdate() {
    var servicedate = document.getElementById("addDate").value;

    if (servicedate == "") {
        document.getElementById("servicedatevalid").style.color = "red";
        document.getElementById("addDate").style.borderColor = "red";
        document.getElementById("servicedatevalid").innerHTML = "Please select date";
        return false;
    }
    else {
        document.getElementById("servicedatevalid").style.color = "";
        document.getElementById("addDate").style.borderColor = "grey";
        document.getElementById("servicedatevalid").innerHTML = "";
        return true;
    }



}
function OrderSelectVendor() {
    var ordervendor = document.getElementById("OrderSelectVendorlist").value;


    if (ordervendor == "--Select Vendor--") {
        document.getElementById("transelectvname").style.color = "red";
        document.getElementById("OrderSelectVendorlist").style.borderColor = "red";
        document.getElementById("transelectvname").innerHTML = "Please Select VendorName";
        return false;
    }
    else {
        document.getElementById("transelectvname").style.color = "";
        document.getElementById("OrderSelectVendorlist").style.borderColor = "grey";
        document.getElementById("transelectvname").innerHTML = "";
        return true;
    }
}

function OrderId() {
    var orderid = document.getElementById("SelectOrderId").value;

    if (orderid == "") {

        document.getElementById("errormassaggeorderid").style.color = "red";
        document.getElementById("SelectOrderId").style.borderColor = "red";
        document.getElementById("errormassaggeorderid").innerHTML = "Please select OrderId";
        return false;
    }
    else {
        document.getElementById("errormassaggeorderid").style.color = "";
        document.getElementById("SelectOrderId").style.borderColor = "grey";
        document.getElementById("errormassaggeorderid").innerHTML = "";
        return true;
    }
}
//function receviditem()
//{


//    var orderid = document.getElementById("reciveditem").value;

//    if (orderid == "") {

//        document.getElementById("reciveditemtra").style.color = "red";
//        document.getElementById("reciveditem").style.borderColor = "red";
//        document.getElementById("reciveditemtra").innerHTML = "Please select OrderId";
//        return false;
//    }
//    else {
//        document.getElementById("reciveditemtra").style.color = "";
//        document.getElementById("reciveditem").style.borderColor = "grey";
//        document.getElementById("reciveditemtra").innerHTML = "";
//        return true;
//    }


//}


function orderbtn() {

    
    //var p = prc();
    var date = selectdate();
    var ovname = OrderSelectVendor();
    var oid = OrderId();
    //var ritem = receviditem();



    if (date && ovname && oid) {
        return true;
    }
    else {
        return false
    }
}

function remove() {
    //orderinsertdate
    document.getElementById("servicedatevalid").style.color = "";
    document.getElementById("addDate").style.borderColor = "grey";
    document.getElementById("servicedatevalid").innerHTML = "";


    //travnameorder

    document.getElementById("transelectvname").style.color = "";
    document.getElementById("OrderSelectVendorlist").style.borderColor = "grey";
    document.getElementById("transelectvname").innerHTML = "";


    //orderid

    document.getElementById("errormassaggeorderid").style.color = "";
    document.getElementById("SelectOrderId").style.borderColor = "grey";
    document.getElementById("errormassaggeorderid").innerHTML = "";
}

//update ordertra




//function Updateselectdate() {
//    var eservicedate = document.getElementById("editDate").value;

//    if (eservicedate == "") {
//        document.getElementById("editservicedatevalid").style.color = "red";
//        document.getElementById("editDate").style.borderColor = "red";
//        document.getElementById("editservicedatevalid").innerHTML = "Please select date";
//        return false;
//    }
//    else {
//        document.getElementById("editservicedatevalid").style.color = "";
//        document.getElementById("editDate").style.borderColor = "grey";
//        document.getElementById("editservicedatevalid").innerHTML = "";
//        return true;
//    }



//}
//function editOrderSelectVendor() {
//    var eordervendor = document.getElementById("editOrderSelectVendorlist").value;


//    if (eordervendor == " ") {
//        document.getElementById("edittranselectvname").style.color = "red";
//        document.getElementById("editOrderSelectVendorlist").style.borderColor = "red";
//        document.getElementById("edittranselectvname").innerHTML = "Please Select VendorName";
//        return false;
//    }
//    else {
//        document.getElementById("edittranselectvname").style.color = "";
//        document.getElementById("editOrderSelectVendorlist").style.borderColor = "grey";
//        document.getElementById("edittranselectvname").innerHTML = "";
//        return true;
//    }
//}
//function editOrderid() {
//    var eordervendor = document.getElementById("EditSelectOrderId").value;


//    if (eordervendor == " ") {
//        document.getElementById("editerrormassaggeorderid").style.color = "red";
//        document.getElementById("EditSelectOrderId").style.borderColor = "red";
//        document.getElementById("editerrormassaggeorderid").innerHTML = "Please Select VendorName";
//        return false;
//    }
//    else {
//        document.getElementById("editerrormassaggeorderid").style.color = "";
//        document.getElementById("EditSelectOrderId").style.borderColor = "grey";
//        document.getElementById("editerrormassaggeorderid").innerHTML = "";
//        return true;
//    }
//}
//function Updateorderbtn() {

//    var edate = Updateselectdate()
//    var eovname = editOrderSelectVendor()
//    var eoid = editOrderid()
//    if (edate && eovname && eoid) {
//        return true;
//    }
//    else
//        return false;
//}
//function removeedit() {

//    //date
//    document.getElementById("editservicedatevalid").style.color = "";
//    document.getElementById("editDate").style.borderColor = "grey";
//    document.getElementById("editservicedatevalid").innerHTML = "";

//    //vname
//    document.getElementById("edittranselectvname").style.color = "";
//    document.getElementById("editOrderSelectVendorlist").style.borderColor = "grey";
//    document.getElementById("edittranselectvname").innerHTML = "";

//    //id
//    document.getElementById("editerrormassaggeorderid").style.color = "";
//    document.getElementById("EditSelectOrderId").style.borderColor = "grey";
//    document.getElementById("editerrormassaggeorderid").innerHTML = "";


//}

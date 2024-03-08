function multiply() {
    debugger
    var textArr = new Array();
    var qtyArr = new Array();
    var itemTotalArr = new Array();
    var TotalPrice = 0;
    var validationCount = document.getElementById('forMultiplyFn').value
    console.log(validationCount);
    for (let i = 0; i < validationCount; i++) {
        textArr[i] = document.getElementById('itemPrice' + i).value;

        qtyArr[i] = document.getElementById('reciveditem' + i).value;

        itemTotalArr[i] = document.getElementById('itemTotalPrice' + i).value = textArr[i] * qtyArr[i];

        TotalPrice = TotalPrice + itemTotalArr[i];
    }

    //console.log(txtboxTotal);
    document.getElementById('totalPrice').value = TotalPrice;

}

function Sendinvoice(e) {
    var grid = $("#categoryGrid").data("kendoGrid");
    var row = e.select().closest("tr");
    var dataitem = grid.dataItem(row);
    debugger;
    $.ajax({
        type: 'Post',
        url: API_URL + "api/TransactionApiInvoice/" + dataitem.TransactionId,
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                success_popup('Inoice Sent!!')
                setTimeout(function () {
                    document.getElementById("success-box").style.display = "none";
                }, 2000)
            }
            else {
                alert_popup('Inoice Sent!!')
            }
        },
        error: function (e) {
        }
    });
}

//service trasaction
function multiplyServicePrice() {

    var textArr = new Array();
    var qtyArr = new Array();
    var itemTotalArr = new Array();
    var TotalPrice = 0;
    var validationCount = document.getElementById('forServiceTMultiplyFn').value
    console.log(validationCount);
    for (let i = 1; i <= validationCount; i++) {
        textArr[i] = document.getElementById('Sprice' + i).value;
        qtyArr[i] = 1;
        itemTotalArr[i] = document.getElementById('Stotal1' + i).value = textArr[i] * qtyArr[i];

        TotalPrice = TotalPrice + itemTotalArr[i];
    }

    //console.log(txtboxTotal);
    console.log(TotalPrice);
    document.getElementById('totalServicePrice').value = TotalPrice;
    var t1 = document.getElementById('totalServicePrice').value;
    var t2 = document.getElementById('totalRepairPrice').value;
    var tprice = Number(t1) + Number(t2);
    console.log("total  price " + tprice);
}
function multiplyOrderPrice() {

    var textArr = new Array();
    var qtyArr = new Array();
    var itemTotalArr = new Array();
    var TotalPrice = 0;
    var validationCount = document.getElementById('forRepairTMultiplyFn').value
    console.log(validationCount);
    for (let i = 1; i <= validationCount; i++) {
        textArr[i] = document.getElementById('Rprice' + i).value;
        qtyArr[i] = 1;
        itemTotalArr[i] = document.getElementById('Rtotal1' + i).value = textArr[i] * qtyArr[i];

        TotalPrice = TotalPrice + itemTotalArr[i];
    }

    //console.log(txtboxTotal);
    console.log(TotalPrice);
    document.getElementById('totalRepairPrice').value = TotalPrice;
    var t1 = document.getElementById('totalServicePrice').value;
    var t2 = document.getElementById('totalRepairPrice').value;
    var tprice = Number(t1) + Number(t2);
    console.log("total  price " + tprice);

}

//-----------------------------------------Service validations----------------------------------------
function servicedate() {
    var servicedate = document.getElementById("ServiceDate").value;

    if (servicedate == "") {
        document.getElementById("dateerrormassage").style.color = "red";
        document.getElementById("ServiceDate").style.borderColor = "red";
        document.getElementById("dateerrormassage").innerHTML = "Please select date";
        return false;
    }
    else {
        document.getElementById("dateerrormassage").style.color = "";
        document.getElementById("ServiceDate").style.borderColor = "grey";
        document.getElementById("dateerrormassage").innerHTML = "";
        return true;
    }
}
function ServiceSelectVendor() {
    var ordervendor = document.getElementById("ServiceSelectVendorlist").value;

    //if (servicedate == "--Select Vendor--")
    if (ordervendor == "--Select Vendor--") {
        document.getElementById("SelectVendorlisterror").style.color = "red";
        document.getElementById("ServiceSelectVendorlist").style.borderColor = "red";
        document.getElementById("SelectVendorlisterror").innerHTML = "Please select VendorName";
        return false;
    }
    else {
        document.getElementById("SelectVendorlisterror").style.color = "";
        document.getElementById("ServiceSelectVendorlist").style.borderColor = "grey";
        document.getElementById("SelectVendorlisterror").innerHTML = "";
        return true;
    }
}
function ServiceOrder() {
    var orderid = document.getElementById("SelectServiceId").value;

    if (orderid == "") {

        document.getElementById("errormassaggeserviceid").style.color = "red";
        document.getElementById("SelectServiceId").style.borderColor = "red";
        document.getElementById("errormassaggeserviceid").innerHTML = "Please select ServiceId";
        return false;
    }
    else {
        document.getElementById("errormassaggeserviceid").style.color = "";
        document.getElementById("SelectServiceId").style.borderColor = "grey";
        document.getElementById("errormassaggeserviceid").innerHTML = "";
        return true;
    }
}

function Servicebtn() {
    var sdate = servicedate();
    var svname = ServiceSelectVendor();
    var sid = ServiceOrder();


    if (sdate && svname && sid) {
        return true;
    }
    else {
        return false;
    }

}

function removes() {
    document.getElementById("dateerrormassage").style.color = "";
    document.getElementById("ServiceDate").style.borderColor = "grey";
    document.getElementById("dateerrormassage").innerHTML = "";


    document.getElementById("SelectVendorlisterror").style.color = "";
    document.getElementById("ServiceSelectVendorlist").style.borderColor = "grey";
    document.getElementById("SelectVendorlisterror").innerHTML = "";


    document.getElementById("errormassaggeserviceid").style.color = "";
    document.getElementById("SelectServiceId").style.borderColor = "grey";
    document.getElementById("errormassaggeserviceid").innerHTML = "";

}

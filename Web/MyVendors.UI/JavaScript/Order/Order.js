var VendarId;
var ReqOrder = [];
var Services = [];
var Orderdate;

function orderDateValid() {
    var orderdate = document.getElementById("order").value;
    var qauntity = document.getElementById("qtadd").value;

    if (orderdate == "") {
        document.getElementById("dateValidationMessage").style.color = "red";
        document.getElementById("order").style.borderColor = "red";
        document.getElementById("dateValidationMessage").innerHTML = "Please Select Date";
        return false;

    }
    else {
        document.getElementById("dateValidationMessage").style.color = "";
        document.getElementById("order").style.borderColor = "grey";
        document.getElementById("dateValidationMessage").innerHTML = "";
        return true;
    }

    if (qtadd == 0) {
        document.getElementById("qtValidationMessage").style.color = "red";
        document.getElementById("qtadd").style.borderColor = "red";
        document.getElementById("qtValidationMessage").innerHTML = "Please enter qauntity.";
        return false;
    }
    else {
        document.getElementById("qtValidationMessage").style.color = "";
        document.getElementById("qtadd").style.borderColor = "grey";
        document.getElementById("qtValidationMessage").innerHTML = "";
        return true;
    }

}

function updateOrderValid() {
    debugger
    var orderupdate = document.getElementById("date").value;
    var upqt = document.getElementById("qt").value;

    if (orderupdate == "") {
        document.getElementById("dateValidationMessageUpdate").style.color = "red";
        document.getElementById("date").style.borderColor = "red";
        document.getElementById("dateValidationMessageUpdate").innerHTML = "Please Select Date";
        return false;

    }
    else {
        document.getElementById("dateValidationMessageUpdate").style.color = "";
        document.getElementById("date").style.borderColor = "grey";
        document.getElementById("dateValidationMessageUpdate").innerHTML = "";
        return true;
    }

    if (qt == 0) {
        document.getElementById("upqtValidMessage").style.color = "red";
        document.getElementById("qt").style.borderColor = "red";
        document.getElementById("upqtValidMessage").innerHTML = "Please enter qauntity";
        return false;

    }
    else {
        document.getElementById("upqtValidMessage").style.color = "";
        document.getElementById("qt").style.borderColor = "grey";
        document.getElementById("upqtValidMessage").innerHTML = "";
        return true;
    }
}

function serviceValid() {

    var servicedate = document.getElementById("sdate").value;

    if (servicedate == "") {
        document.getElementById("servicedatevalid").style.color = "red";
        document.getElementById("sdate").style.borderColor = "red";
        document.getElementById("servicedatevalid").innerHTML = "Please select date";
        return false;
    }
    else {
        document.getElementById("servicedatevalid").style.color = "";
        document.getElementById("sdate").style.borderColor = "grey";
        document.getElementById("servicedatevalid").innerHTML = "";
        return true;
    }



}
function Order() {
    var valid = false;

    if (document.getElementById("repair").checked) {
        valid = true;
    }
    else if (document.getElementById("service").checked) {
        valid = true;
    }

    if (valid) {
        alert('succesful');
    }
    else {
        alert('Select one');
        return false;
    }
}



function successorder() {
    document.getElementById("success-box-order").style.display = "block";

    $("#close-success-order").click(function (event) { document.getElementById("success-box-order").style.display = "none"; })
    id = "close-success-order"
}

function alertorder() {
    document.getElementById("alert-box").style.display = "block";

    $("#close-alert").click(function (event) { document.getElementById("alert-box").style.display = "none"; })
    id = "close-alert"
}


function validatevname() {
    var namechose = document.getElementById("vendorselect").value;

    if (namechose == "") {
        document.getElementById("dropvname").style.color = "red";
        document.getElementById("dropvname").innerHTML = "Please Select Vendor.";
        return false;
    }
    else {
        document.getElementById("dropvname").style.color = "grey";
        document.getElementById("dropvname").innerHTML = "";
        return true;
    }

}


$(document).ready(function () {
    $("#vendorselect").change(function () {
        if (validatevname) {
            document.getElementById("dropvname").style.color = "grey";
            document.getElementById("dropvname").innerHTML = "";
        }
    });
});



function displayOrderList() {
    var d = new Date();
    var today = d.getDate();
    $("#categoryGrid").kendoGrid({
        dataSource: {
            //type: "odata",
            transport: {
                read: API_URL + "api/ApiOrder"
            },
            pageSize: 10
        },
        height: 500,
        pageable: true,
        sortable: true,
        reorderable: true,
        schema: {
            model: {
                id: "OrderId",
                fields: {
                    OrderId: { editable: false, nullable: true },

                }
            }
        },
        columns: [{ selectable: true, width: "40px" },
                    {

                        template: "<div class='grid-action'><a onclick='editorder($(this))' class='text-muted'><i class='icon-edit1'></i></a><a  onclick='deleteorder($(this))' class='text-danger delete-btn'><i class='icon-delete'></i></a><a onclick='ShowDetails($(this))' class='text-primary delete-btn'><i class='icon-show'></i></a></div>",
                        field: "Action",
                        title: "Action",
                        width: 80
                    },
                    {
                        field: "VendorName",
                        title: "Vendor Name",
                    },
                    {
                        field: "OrderType",
                        title: "OrderType",
                        template: "#if(OrderType == 'Active'){#<div><i class='icon-circle-filled' style='color:green'></i></div>#} else{#<div><i class='icon-circle-filled' style='color:red'></i></div>#}#",

                    },
                    {
                        field: "LastPaymentDate",
                        title: "LastPayment Date",
                        template: '#= kendo.toString(kendo.parseDate(LastPaymentDate), "dd/MM/yyyy")#'
                    },
                    {
                        field: "OrderDate",
                        title: "Order Date",
                        template: '#= kendo.toString(kendo.parseDate(OrderDate), "MM/dd/yyyy")#'
                    },
                    {
                        field: "PaymentStatus",
                        title: "Payment Status",
                    },
        ]
    });

}
function editorder(e) {
    // debugger;
    //Fetch Date From Grid
    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }


    var Bindorder = '';
    $('#orderedit').empty();

    $("#updateOrder").data("kendoWindow").center().open();
    var grid = $("#categoryGrid").data("kendoGrid");
    var row = e.select().closest("tr");
    var dataitem = grid.dataItem(row);

    $.ajax({
        sync: true,
        url: API_URL + "api/ApiOrder/" + dataitem.OrderId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //processData: false,
        type: 'GET',
        success: function (data) {
            console.log('Item data');
            console.log(data)
            document.getElementById('editvname').innerHTML = dataitem.VendorName;
            document.getElementById('editid').value = dataitem.OrderId;
            document.getElementById('date').value = formatDate(dataitem.OrderDate);

            if (data) {
                $.each(data,
                function (i, value) {
                    $("#upovemail").text(data[i].VendorEmail)
                    $("#upovno").text(data[i].VendorNo)
                    Bindorder += '<tr><td><span id="subname-' + i + '">' + value.SubCatName + '</span></td>' +
                    '<td><input type="number" min="0" value="' + value.Quantity + '" style="width:80px;text-align:end;" id="qt"></td>'
                    + '<span id="qtValidationMessage"></span>' +
                    '<td><input type="hidden" min="0" value="' + value.SubCatId + '"></td>' +
                    '</tr>';
                });
                $('#orderedit').append(Bindorder); //tbluser -> table id 6 
            }
        }

    });


}



function ShowDetails(e) {

    var grid = $("#categoryGrid").data("kendoGrid");
    var row = e.select().closest("tr");
    var dataitem = grid.dataItem(row);
    var BindViewOrder = '';
    $("#Vieworder").empty();
    openAddEventPopupViewOrder()

    $.ajax({
        sync: true,
        url: API_URL + "api/ApiOrder/" + dataitem.OrderId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //processData: false,
        type: 'GET',
        success: function (data) {
            if (data) {
                $("#WOrderdate").text(dataitem.OrderDate)
                $("#WOrderNo").text(dataitem.OrderId)
                $("#invname").text(dataitem.VendorName)
                $.each(data,
                function (i, value) {
                    $("#Vemail").text(data[i].VendorEmail)
                    $("#VNo").text(data[i].VendorNo)
                    $("#Ostatus").text(data[i].OrderType)

                    BindViewOrder += '<tr><td><span id="vsubname-' + i + '">' + data[i].SubCatName + '</span></td>' +
                    '<td><span style="width:80px;text-align:end;  id="vqt"">' + data[i].Quantity + '</span></td>'
                    '</tr>';
                });
                $('#Vieworder').append(BindViewOrder); //tbluser -> table id 6 
            }
        }

    });
}

function RequestOrder() {
    //debugger;

    $('#subcat tr').each(function () {
        //console.log("Start")

        var vname = $('#vendorNameO').text();
        var vemail = $("#vendoremailO").text()
        var vmobile = $("#vendornoO").text()
        var item;
        var quntity;

        this.cells[0].innerHTML
        Orderdate = document.getElementById("order").value;
        item = $(this).closest('tr').find("input").eq(1).val()
        quntity = $(this).closest('tr').find("input").eq(0).val();
        var subName = $(this).closest('tr').find("span").eq(0).text();

        var order = {
            OrderDate: Orderdate,
            VendorId: VendarId,
            VendorName: vname,
            SubCatId: item,
            Quantity: quntity,
            SubCatName: subName,
            VendorEmail: vemail,
            VendorNo: vmobile
        }
        ReqOrder.push(order);
    });

    if (orderDateValid()) {
        InsertOrderItems(ReqOrder);
    }

}

function InsertOrderItems(ReqOrder) {
    $.ajax({
        sync: true,
        url: API_URL + "api/ApiOrder/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(ReqOrder),
        //processData: false,
        type: 'POST',
        success: function (response) {
            if (response) {
                $("#newOrder").data("kendoWindow").close()
                $("#addcategory").data("kendoWindow").close()
                successorder();
                setTimeout(function () {
                    document.getElementById("success-box-order").style.display = "none";
                    displayOrderList()
                }, 2000);
                ReqOrder = [];
            }
            else {
                $("#newOrder").data("kendoWindow").close()
                $("#addcategory").data("kendoWindow").close()
                alert_popup('Order Failed !')
                setTimeout(function () {
                    document.getElementById("alert-box").style.display = "none";
                    displayOrderList()
                }, 2000);
                ReqOrder = [];
            }
        }
    });
}



$(document).ready(function () {
    $("#date").change(function () {

        if (updateOrderValid()) {
            document.getElementById("DateValidateMessage").innerHTML = "";
            document.getElementById("date").style.borderColor = "grey";
            document.getElementById("DateValidateMessage").style.color = "grey";
        }
    });

    $("#qt").change(function () {
        debugger
        if (updateOrderValid()) {
            document.getElementById("upqtValidMessage").innerHTML = "";
            document.getElementById("qt").style.borderColor = "grey";
            document.getElementById("upqtValidMessage").style.color = "grey";
        }
    });

    $("#order").change(function () {
        //debugger;
        if (orderDateValid()) {
            document.getElementById("DateValidateMessage").innerHTML = "";
            document.getElementById("order").style.borderColor = "grey";
            document.getElementById("DateValidateMessage").style.color = "grey";
        }
    });

    $("#qtadd").change(function () {
        if (orderDateValid()) {
            document.getElementById("qtValidationMessage").innerHTML = "";
            document.getElementById("qtadd").style.borderColor = "grey";
            document.getElementById("qtValidationMessage").style.color = "grey";
        }
    });

    $("#sdate").change(function () {
        if (serviceValid()) {
            document.getElementById("servicedatevalid").innerHTML = "";
            document.getElementById("sdate").style.borderColor = "grey";
            document.getElementById("servicedatevalid").style.color = "grey";
        }
    });
});



function widgetChange(е) {
    $("#lblvendoid").val(this.value());
    //alert(this.value())
};


$(document).ready(function () {
    //debugger;
    $.ajax({
        url: API_URL + "api/apiVendorDD/",
        method: "GET",
        success: function (data) {
            $("#vendorselect").kendoDropDownList({
                dataSource: data,
                dataTextField: "c_vname",
                dataValueField: "c_vid",
                optionLabel: "Select Vendor Name",
                change: onchange
                //select: onchange

            }).data('kendoDropDownList');
        }

    });
});

function onchange() {
    VendarId = $("#vendorselect").kendoDropDownList().data('kendoDropDownList').value()
    var ddnm = $("#vendorselect").data('kendoDropDownList').text();
    $("#vendorName").text(ddnm)
    $("#vendorNameO").text(ddnm)

}

function getSubCategory() {
    var BindUser = '';
    $('#subcat').empty();
    $.ajax({
        url: API_URL + "api/apiVendorDD/GetVendorOne?id=" + VendarId,
        dataType: "json",
        success: function (data) {
            // debugger
            if (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#vendoremailO").text(data[i].VendorEmail)
                    $("#vendornoO").text(data[i].VendorMobile)
                }

            }
        }
    });

    $.ajax({
        url: API_URL + "api/apiVendorDD/" + VendarId,
        method: "GET",
        success: function (data) {
            //console.log(data)
            if (data) {
                $.each(data,
                function (index, value) {
                    //console.log(value.c_SubName)
                    //id = value.id;
                    BindUser += '<tr><td><span>' + value.c_SubName + '</span></td>' +
                    '<td><input type="number" min="0" value="0" style="width:80px;text-align:end;" id="qtadd"></td>'
                    + '<span id="qtValidationMessage"></span>' +
                    '<td><input type="hidden" min="0" value="' + value.c_SubId + '"></td>' +
                    '</tr>';
                });
                $('#subcat').append(BindUser); //tbluser -> table id 6 
            }

        }

    });
}


//Add order data 
function AddSubCategory() {

    var id = $("#vid").val()
    var CourseData = {
        c_Id: $("#oid").val(),
        Name: $("#venname").val(),
    }


    //for new one
    if (id == '') {
        var settings = {
            "url": API_URL + "/api/DropdownVendorAPI/",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(CourseData),
        };
        $.ajax(settings).done(function (response) {
            if (response) {

                $("#addcategory").data("kendoWindow").close()
                subcatInsert();
                setTimeout(function () {
                    document.getElementById("success-box-subcat").style.display = "none";
                    displaySubCategory()
                }, 2000);
            }
            else {
                alert('Error Occured');
            }

        });


    }
    else {

        var settings = {

            "url": API_URL + "api/DropdownVendorAPI/?c_order_type=" + id,
            "method": "PUT",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(CourseData),
        };
        $.ajax(settings).done(function (response) {

            $("#addcategory").data("kendoWindow").close()
            subcatUpdate();
            setTimeout(function () {
                document.getElementById("success-box-subcat-update").style.display = "none";
                displaySubCategory()
            }, 2000);

        });
    }
}


//Edit Order
var Updateorder = [];
function UpdateOrder() {
    Updateorder = [];

    $('#orderedit tr').each(function () {
        var item;
        var quntity;
        var SubName;
        var vname = $("#editvname").text();
        var upvemail = $("#upovemail").text()
        var upvno = $("#upovno").text()

        this.cells[0].innerHTML
        subName = $(this).closest('tr').find("span").eq(0).text();
        quntity = $(this).closest('tr').find("input").eq(0).val();
        item = $(this).closest('tr').find("input").eq(1).val()

        var data = {
            OrderId: $('#editid').val(),
            OrderDate: $('#date').val(),
            Quantity: quntity,
            VendorName: vname,
            SubCatId: item,
            SubCatName: subName,
            VendorEmail: upvemail,
            VendorNo: upvno
        }

        Updateorder.push(data);


    });
    //if (updateOrderValid()) {

    UpdateOrderItems(Updateorder);
    //}
}

function UpdateOrderItems(Updateorder) {
    var settings = {

        "url": API_URL + "api/ApiOrder/",
        "method": "PUT",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(Updateorder),
    };
    $.ajax(settings).done(function (response) {
        if (response == 1) {
            success_popup('Order Update Successfully!')
            setTimeout(function () {
                document.getElementById("success-box").style.display = "none";
            }, 2000);
            // alert('update success');
            window.location.reload();
        }
    });

}

$(document).ready(function () {

});
// hide userprofile
$(document).click(function () {
    //window.top.$('body').find('.userprofile-menu').removeClass('open');
    //window.top.$('body').find('.userprofile').removeClass('active open');
});

// common select dropdown
$("select:not([multiple])").kendoDropDownList();


displayOrderList()

//Contextmenu popover left click for grid pager
var popup = $("#popup").kendoPopup({
    animation: false,
    anchor: $("#moreOption"),
    origin: "top right",
    position: "bottom right",
    collision: "fit",

}).data("kendoPopup");

$("#moreOption").click(function () {
    $("#popup").data("kendoPopup").open()
});

$("#pagerDropdown").kendoDropDownList({
    popup: {
        appendTo: 'body'
    }
});

$("#addcategory").kendoWindow({
    animation: false,
    width: '550px',
    height: '150px',
    visible: false,
    draggable: false,
    resizable: false,
    title: "Order",
    actions: [
        "Close"
    ],
    modal: true,

}).data("kendoWindow");

$("#newOrder").kendoWindow({
    animation: false,
    width: '600px',
    height: '350px',
    visible: false,
    draggable: false,
    resizable: false,
    title: "New Order",
    actions: [
        "Close"
    ],
    modal: true,

}).data("kendoWindow");

$("#updateOrder").kendoWindow({
    animation: false,
    width: '600px',
    height: '380px',
    visible: false,
    draggable: false,
    resizable: false,
    title: "Update Order",
    actions: [
        "Close"
    ],
    modal: true,

}).data("kendoWindow");


$("#vieworderdetail").kendoWindow({
    animation: false,
    width: '700px',
    height: '350px',
    visible: false,
    draggable: false,
    resizable: false,
    title: "Order Details",
    actions: [
        "Close"
    ],
    modal: true,

}).data("kendoWindow");

$("#service").kendoWindow({
    animation: false,
    width: '600px',
    height: '350px',
    visible: false,
    draggable: false,
    resizable: false,
    title: "Service",
    actions: [
        "Close"
    ],
    modal: true,

}).data("kendoWindow");

$(".datetimepicker").kendoDateTimePicker({
    value: new Date()
});


function openAddEventPopup() {
    $("#addcategory").data("kendoWindow").center().open();
}

function openAddEventPopup1() {
    if (validatevname()) {
        $("#newOrder").data("kendoWindow").center().open();
        getSubCategory()
    }

}

function openAddEventPopup2() {
    if (validatevname()) {
        $("#service").data("kendoWindow").center().open();
        getSubCategoryService()
    }
}

function openAddEventPopupViewOrder() {
    $("#vieworderdetail").data("kendoWindow").center().open();
}

$('#search').on('input', function () {
    var q = $("#search").val();
    var grid = $("#categoryGrid").data("kendoGrid");
    grid.dataSource.query({
        page: 1,
        pageSize: 10,
        filter: {
            logic: "or",
            filters: [
              { field: "VendorName", operator: "contains", value: q },
              { field: "OrderDate", operator: "contains", value: q },
              { field: "PaymentStatus", operator: "contains", value: q },
              { field: "OrderType", operator: "contains", value: q }
            ]
        }
    });
});


//---------service---///

function getSubCategoryService() {
    var BindUser = '';

    $('#subcatorder').empty()
    $.ajax({
        url: API_URL + "api/apiVendorDD/GetVendorOne?id=" + VendarId,
        dataType: "json",
        success: function (data) {

            if (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#vendoremailS").text(data[i].VendorEmail)
                    $("#vendornoS").text(data[i].VendorMobile)
                }
            }
        }
    });

    $.ajax({
        url: API_URL + "api/ApiVendorDD/" + VendarId,
        method: "GET",
        success: function (data) {
            //console.log(data)
            if (data) {
                debugger
                for (var i = 0; i < data.length; i++) {
                    BindUser += '<tr><td><span for="items">' + data[i].c_SubName + '</span></td>' +
                        '<td><input type="hidden" min="0" value="' + data[i].c_SubId + '" name="id"></td>' +
                        '<td><input type="checkbox" name="" id="repair' + i + '" class="k-checkbox">' +
                        '<label class="k-checkbox-label" for="repair' + i + '">Repair</label>' +
                        '<input type="checkbox" name="" id="service' + i + '" class="k-checkbox">' +
                        '<label class="k-checkbox-label" for="service' + i + '">Service</label><br /></td>' +
                        '<td><input type="text" class="form-control" id="notes-"' + i + '"" name="notes" ></td>'

                    '</tr>';
                }
                $('#subcatorder').append(BindUser); //tbluser -> table id 6 
            }
        }
    });
}

$(document).ready(function () {
    $("#showservice").click(function () {
        window.location.replace("/Order/Service");
    });
});

function Order() {

    var vname = $('#vendorNameO').text();
    var sdate = $("#sdate").val();
    var svemail = $("#vendoremailS").text()
    var svmobile = $("#vendornoS").text()
    $('#subcatorder tr').each(function () {
        //console.log("Start")
        var Repair;
        var Service;
        var orderdate;
        var notes;
        var subid;


        this.cells[0].innerHTML

        debugger
        Repair = $(this).closest('tr').find("input").eq(0).is(':checked')
        Service = $(this).closest('tr').find("input").eq(1).is(':checked')
        var OSubname = $(this).closest('tr').find("span").eq(0).text()
        servicedate = document.getElementById("sdate").value;
        notes = $(this).closest('tr').find("input[name='notes']").eq(0).val();
        subid = $(this).closest('tr').find("input[name='id']").eq(0).val();

        var Service = {
            ServiceDate: servicedate,
            VendorId: VendarId,
            VendorName: vname,
            Repair: Repair,
            Service: Service,
            SubCatName: OSubname,
            SubCatId: subid,
            VendorEmail: svemail,
            VendorMobile: svmobile,
            Notes: notes
        }

        Services.push(Service);
        //console.log(Service);
    });


    //if (serviceValid()) {
    // debugger
    OrderInsert(Services);

    // }

}

function OrderInsert(Services) {
    $.ajax({
        sync: true,
        url: API_URL + "api/ApiService/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(Services),
        //processData: false,
        type: 'POST',
        success: function (response) {
            if (response) {
                $("#service").data("kendoWindow").close()
                $("#addorderwindow").data("kendoWindow").close()
                success_popup('Service Added Successfully !')
                setTimeout(function () {
                    document.getElementById("success-box").style.display = "none";
                }, 2000);
                window.location.replace("/Order/Service");
            }
            else {
                $("#service").data("kendoWindow").close()
                $("#addorderwindow").data("kendoWindow").close()
                alert_popup('Service Failed !')
                setTimeout(function () {
                    document.getElementById("alert-box").style.display = "none";
                }, 2000);
                window.location.replace("/Order/Service");

            }
        }
    });
}








var UpdateService = [];
function updateServcie() {
    // debugger
    UpdateService = [];
    var vname = $('#vn').text();
    var sdate = $("#update").val();
    var upvemail = $("#upvemail").text()
    var upvno = $("#upvno").text()
    $('#editservice tr').each(function () {
        //console.log("Start")
        var Repair;
        var Service;
        var servicedate;
        var subid;
        var notesedit;


        this.cells[0].innerHTML

        //debugger
        Repair = $(this).closest('tr').find("input").eq(0).is(':checked')
        Service = $(this).closest('tr').find("input").eq(1).is(':checked')
        var SSubname = $(this).closest('tr').find("span").eq(0).text()
        servicedate = document.getElementById("update").value;
        subid = $(this).closest('tr').find("input[name='upsubid']").eq(0).val();
        notesedit = $(this).closest('tr').find("input[name='notesedit']").eq(0).val();


        var data = {
            ServiceDate: servicedate,
            ServiceId: $('#editid').val(),
            VendorName: vname,
            Repair: Repair,
            Service: Service,
            SubCatName: SSubname,
            SubCatId: subid,
            VendorEmail: upvemail,
            VendorMobile: upvno,
            Notes: notesedit
        }
        //console.log(Repair + " " + Service)

        if (Repair == true || Service == true) {
            UpdateService.push(data);
        }
        else {
            UpdateService = []
            alert_popup('Please fill service details !')
            setTimeout(function () {
                document.getElementById("alert-box").style.display = "none";
            }, 2000)
        }



        //UpdateService.push(data);
        //console.log(Service);
    });
    if (updateServiceValid()) {
        if (UpdateService.length == 0) {
            alert_popup('Please fill service details !')
            setTimeout(function () {
                document.getElementById("alert-box").style.display = "none";
            }, 2000);
        }
        else {
            UpdateServiceItems(UpdateService);
        }

    }
}

function UpdateServiceItems(UpdateService) {
    var settings = {

        "url": API_URL + "api/ApiService/",
        "method": "PUT",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(UpdateService),
    };
    $.ajax(settings).done(function (response) {
        if (response == 1) {
            $("#updateService").data("kendoWindow").close()
            success_popup('Service Updated Successfully !')
            setTimeout(function () {
                document.getElementById("success-box").style.display = "none";
                displayServiceList()
            }, 2000);
        }
    });
}



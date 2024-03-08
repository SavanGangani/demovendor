//Get data vendor
var updatecategory;
var updateSubcategory;

var updateSubcategoryvalue;
var updatecategoryvalue

$(document).ready(function () {
    LoadVendor();
    $("#Dashboard").removeClass("active");
    $("#Category").removeClass("active");
    $("#SubCategory").removeClass("active");
    $("#Vendors").addClass("active");
    // hide userprofile
    $(document).click(function () {
        //window.top.$('body').find('.userprofile-menu').removeClass('open');
        //window.top.$('body').find('.userprofile').removeClass('active open');
        //LoadVendor();
    });

    // common select dropdown
    $("select:not([multiple])").kendoDropDownList();




    $(".files").kendoUpload();

    $("#addVendors").kendoWindow({
        animation: false,
        width: '850px',
        visible: false,
        draggable: false,
        resizable: false,
        title: "Add Vendor Details",
        actions: [
            "Close"
        ],
        modal: true,

    });

    $("#editvendor").kendoWindow({
        animation: false,
        width: '800px',
        height: '520px',
        visible: false,
        draggable: false,
        resizable: false,
        title: "Edit Vendor Details",
        actions: [
            "Close"
        ],
        modal: true,

    }).data("kendoWindow");

    $("#ddcategory").kendoMultiSelect({
        dataSource: null,
        autoBind: false,
        //minLength: 3,
        placeholder: 'Select Category',
        dataTextField: "Name",
        dataTextValue: "Id",
        change: categoryChange,
        //delay: 200
    }).data("kendoMultiSelect");

    $("#multiselect").kendoMultiSelect({
        dataSource: null,
        dataTextField: "c_SubName",
        dataTextValue: "c_SubId",
        placeholder: 'Select SubCategory'
    }).data("kendoMultiSelect");


    $.ajax({
        url: API_URL + "api/APIvendorcategory",
        method: "GET",
        success: function (data) {
            $("#upddcategory").data("kendoMultiSelect").setDataSource(data);
        }
    });


    updatecategory = $("#upddcategory").kendoMultiSelect({
        dataSource: null,
        autoBind: false,
        //minLength: 3,
        placeholder: 'Select Category',
        //dataTextField: 'name',
        //dataValueField: 'value',
        dataTextField: "Name",
        dataValueField: "Id",
        change: upcategoryChange,
        //delay: 200
    }).data("kendoMultiSelect");

    updateSubcategory = $("#upmultiselect").kendoMultiSelect({
        dataSource: null,
        autoBind: false,
        dataTextField: "c_SubName",
        dataValueField: "c_SubId",
        placeholder: 'Select SubCategory',
        change: upsubcategoryChange,
    }).data("kendoMultiSelect");

}).data("kendoWindow");
function gridDataBound(e) {
    var grid = e.sender;
    if (grid.dataSource.total() == 0) {
        var colCount = grid.columns.length;
        $(e.sender.wrapper)
            .find('tbody')

        .append('<div style="width: 90vw;height: 50vh;display: flex;justify-content: center;"><div style="width: 400px; padding-top: 50px;"><img src="'+ UI_URL + '"Image/datanotfound.png" style="height: 250px"></div></div>');

    }
};
function LoadVendor() {
    $("#categoryGrid").kendoGrid({
        dataSource: {
            //type: "odata",
            transport: {
                read: API_URL + "api/APIvendoradd"
            },
            pageSize: 8
        },
        dataBound:gridDataBound,
        height: 500,
        pageable: true,
        sortable: true,
        reorderable: true,
        schema: {
            model: {
                id: "c_VendorId",
                fields: {
                    c_VendorId: { editable: false, nullable: true },
                }
            }
        },
        columns: [{ selectable: true, width: "40px" },
        {
            template: "<div class='grid-action'><a class='text-muted' onclick='updateVendor($(this))'><i class='icon-edit1'></i></a><a class='text-danger delete-btn' onclick='Delete($(this))'><i class='icon-delete'></i></a>",
            field: "c_VendorId",
            title: "Action",
            width: 80
        },
         {

             field: "c_VendorName",
             title: "Vendor Name",
         }, {

             field: "c_Mobile",
             title: "Mobile Number",
         }, {

             field: "c_Email",
             title: "Vendor Email",
         },
        {

            field: "c_Address",
            title: "Vendor Address",
        },
        {

            field: "c_Website",
            title: "Vendor Website",
            template: "#if(c_Website==''){#-#} else{# #: c_Website # #}#",
        },
        {

            field: "c_GstNo",
            title: "GST Number",

        },

        ]

    });



} 



function clear_data() {
    $("#ddcategory").data("kendoMultiSelect").value('');
    $("#multiselect").data("kendoMultiSelect").value('');
    $("#entervendorname").val('')
    $("#txtmobileno").val('')
    $("#txtvemail").val('')
    $("#txtaddress").val('')
    $("#txtwebsite").val('')
    $("#txtgstno").val('')
    $("#txtaholdername").val('')
    $("#txtaccountnumber").val('')
    $("#txtbankname").val('')
    $("#txtifsccode").val('')
    $("#resetvendor").trigger('click')
}

var subcatdata;
function openAddEventPopup() {
    clear_data()

    $("#addVendors").data("kendoWindow").center().open();


    $.ajax({
        url: API_URL + "api/APIvendorcategory",
        method: "GET",
        success: function (data) {
            $("#ddcategory").data("kendoMultiSelect").setDataSource(data);
        }

    });
}


function OpenEditVendor() {
    $("#editvendor").data("kendoWindow").center().open();
}

function updateVendor(e) {
    var grid = $("#categoryGrid").data("kendoGrid");
    var row = e.select().closest("tr");
    var dataitem = grid.dataItem(row);

    updatecategory.value(dataitem.c_IdArray);
    //updateSubcategory.value(dataitem.c_SubId[0]);

    updateSubcategoryvalue = dataitem.c_SubId;
    updatecategoryvalue = dataitem.c_IdArray
    OpenEditVendor()
    upcategoryChange()
    //updateSubcategoryvalue = updateSubcategory.value();


    $("#upVid").val(dataitem.c_VendorId)
    $("#upentervendorname").val(dataitem.c_VendorName)
    $("#uptxtmobileno").val(dataitem.c_Mobile)
    $("#uptxtvemail").val(dataitem.c_Email)
    $("#uptxtaddress").val(dataitem.c_Address)
    $("#uptxtwebsite").val(dataitem.c_Website)
    $("#uptxtgstno").val(dataitem.c_GstNo)
    $("#uptxtbankname").val(dataitem.c_BankName)
    $("#uptxtifsccode").val(dataitem.c_IFSCCode)
    $("#uptxtaccountnumber").val(dataitem.c_AccountNo)
    $("#uptxtaholdername").val(dataitem.c_AccountHolderName)
}

function upcategoryChange() {
    updatecategoryvalue = updatecategory.value()
    var upcategoryId = "";
    for (var i = 0; i < updatecategoryvalue.length; i++) {
        if (i == 0) {
            upcategoryId = updatecategoryvalue[i]
        }
        else {
            upcategoryId = upcategoryId + ',' + updatecategoryvalue[i]
        }
    }
    //updateSubcategoryvalue = updateSubcategory.value();


    //$("#uplblcategoryid").val(upcategoryId)


    if (upcategoryId == '') {
        var a = [{ c_SubId: null, c_SubName: '' }]
        $("#upmultiselect").data("kendoMultiSelect").setDataSource(a);
    }
    else {
        $.ajax({
            url: API_URL + "api/APIsubcategorydropdown/" + upcategoryId,
            method: "GET",
            success: function (data) {
                $("#upmultiselect").data("kendoMultiSelect").setDataSource(data);
                updateSubcategory.value(updateSubcategoryvalue);
                console.log("Ajax " + updateSubcategory.value())
                updateSubcategoryvalue = updateSubcategory.value();
            }

        });

    }

}
function upsubcategoryChange() {
    updateSubcategoryvalue = updateSubcategory.value();
}


function categoryChange(e) {

    var catselect = $("#ddcategory").data("kendoMultiSelect").value()
    var categoryId = "";
    for (var i = 0; i < catselect.length; i++) {
        if (i == 0) {
            categoryId = catselect[i].Id
        }
        else {
            categoryId = categoryId + ',' + catselect[i].Id
        }
    }
    $("#lblcategoryid").val(categoryId)

    if (categoryId == '') {
        var a = [{ c_SubId: null, c_SubName: '' }]
        $("#multiselect").data("kendoMultiSelect").setDataSource(a);
    }
    else {
        $.ajax({
            url: API_URL + "api/APIsubcategorydropdown/" + categoryId,
            method: "GET",
            success: function (data) {

                $("#multiselect").data("kendoMultiSelect").setDataSource(data);
            }

        });

    }

}

function openAddEventPopup1() {
    $("#addinvoice").data("kendoWindow").center().open();

    $.ajax({
        url: API_URL + "/api/APIdropdown/",
        method: "GET",
        success: function (data) {
            $("#ddlvendor").kendoDropDownList({
                dataSource: data,
                dataTextField: "c_VendorName",
                dataValueField: "c_VendorId",
                change: widgetChange,
                optionLabel: "Select Vendor Name "

            });
        }

    });

}

function widgetChange(е) {
    $("#lblvid").val(this.value());
};

function subcategoryChange(e) {
    $("#lblsubcategoryid").val(this.value());
}

function downloadpdf(e) {
    var grid = $("#categoryGrid").data("kendoGrid");
    var row = e.select().closest("tr");
    var dataitem = grid.dataItem(row);
    $.ajax({
        type: 'Get',
        url: API_URL + 'api/APIinvoice/' + dataitem.c_VendorId,
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response == false) {
                invoiceWarning();
            }
            else {
                invoicesuccess();
            }

        },
        error: function (e) {
            alert(e)
        }
    });
}

function sendrequest(e) {
    var grid = $("#categoryGrid").data("kendoGrid");
    var row = e.select().closest("tr");
    var dataitem = grid.dataItem(row);
    $.ajax({
        type: 'Post',
        url: API_URL + 'api/APIApproval/' + dataitem.c_VendorId,
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            functionsuccess_sendmail()
        },
        error: function (e) {

        }


    });
}

function functionsuccess_sendmail() {
    document.getElementById("success-box-email").style.display = "block";

    $("#close-success-email").click(function (event) { document.getElementById("success-box-email").style.display = "none"; })
    id = "close-success-email"
}

function categoryarray() {
    var catselect = $("#ddcategory").data("kendoMultiSelect").value()
    var categoryId = [];
    for (var i = 0; i < catselect.length; i++) {
        categoryId.push(catselect[i].Id)
    }
    return categoryId
}

function subcategoryarray() {
    var Subcatselect = $("#multiselect").data("kendoMultiSelect").value()

    var subcategoryId = [];
    for (var i = 0; i < Subcatselect.length; i++) {
        subcategoryId.push(Subcatselect[i].c_SubId)
    }
    return subcategoryId
}

function addVendors() {
    //var multiselect = $("#ddlsubcategory").data("kendoMultiSelect");

    //var selectedData = [];
    //var items = multiselect.value();


    //for (var i = 1; i < items.length; i++) {
    //    selectedData.push(items[i].c_SubId);
    //}
    //console.log(selectedData)


    var VendorData = {
        //c_Id: $("#lblcategoryid").val(),
        c_IdArray: categoryarray(),
        c_SubId: subcategoryarray(),
        c_VendorName: $("#entervendorname").val(),
        c_Mobile: $("#txtmobileno").val(),
        c_Email: $("#txtvemail").val(),
        c_Address: $("#txtaddress").val(),
        c_Website: $("#txtwebsite").val(),
        c_GstNo: $("#txtgstno").val(),
        c_BankName: $("#txtbankname").val(),
        c_IFSCCode: $("#txtifsccode").val(),
        c_AccountNo: $("#txtaccountnumber").val(),
        c_AccountHolderName: $("#txtaholdername").val()
    }
    $.ajax({
        sync: true,
        url: API_URL + 'api/APIvendoradd',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(VendorData),
        //processData: false,
        type: 'POST',
        success: function (response) {
            if (response) {
                logger("Vendor", "Insert")
                $("#addVendors").data("kendoWindow").close()
                success_popup('Insert Successfully !')
                setTimeout(function () {
                    document.getElementById("success-box").style.display = "none";
                    LoadVendor();
                }, 2000);
            }
            else {
                //$("#addVendors").data("kendoWindow").close()
                alert_popup('User already exists !')
                setTimeout(function () {
                    document.getElementById("alert-box").style.display = "none";
                    //LoadVendor();
                }, 2000);
            }

        },
        // End  Ajax success function

        // End Ajax Error function
    });
}

function logger(page, operation) {
    var Login = {
        Email: page,
        Password: operation
    };

    $.ajax({
        sync: true,
        type: 'POST',
        url: '/Logger/post',
        data: JSON.stringify(Login),
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8'
    });

}

//Update Vendor Details
function updateVendors() {

    var VendorData = {
        //c_Id: $("#lblcategoryid").val(),
        c_VendorId: $("#upVid").val(),
        c_IdArray: updatecategoryvalue,
        c_SubId: updateSubcategoryvalue,
        c_VendorName: $("#upentervendorname").val(),
        c_Mobile: $("#uptxtmobileno").val(),
        c_Email: $("#uptxtvemail").val(),
        c_Address: $("#uptxtaddress").val(),
        c_Website: $("#uptxtwebsite").val(),
        c_GstNo: $("#uptxtgstno").val(),
        c_BankName: $("#uptxtbankname").val(),
        c_IFSCCode: $("#uptxtifsccode").val(),
        c_AccountNo: $("#uptxtaccountnumber").val(),
        c_AccountHolderName: $("#uptxtaholdername").val()
    }
    $.ajax({
        sync: true,
        url: API_URL + 'api/APIvendoradd',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(VendorData),
        //processData: false,
        type: 'put',
        success: function (response) {
            if (response) {
                logger("Vendor", "Update")
                $("#editvendor").data("kendoWindow").close()
                success_popup('Update Successfully !')
                setTimeout(function () {
                    document.getElementById("success-box").style.display = "none";
                    LoadVendor();
                }, 2000);
            }
            else {
                $("#editvendor").data("kendoWindow").close()
                alert_popup('Update Failed !')
                setTimeout(function () {
                    document.getElementById("alert-box").style.display = "none";
                    LoadVendor();
                }, 2000);
            }
        },

    });
}



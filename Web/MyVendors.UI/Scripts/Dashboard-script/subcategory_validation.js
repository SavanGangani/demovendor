$(document).ready(function () {
    $("#resetsubcat").click(function () {
        if (SubCategoryName) {
            document.getElementById("subname").value = " ";
            $("#catname").data("kendoDropDownList").value(-1);
            document.getElementById("subname").style.borderColor = "lightgrey";
            document.getElementById("SubCategoryMsgForName").innerHTML = "";
            document.getElementById("SubCategoryMsgForSelect").innerHTML = "";
        }

    });


    $("#catname").change(function () {
        if (selectDropDown) {
            document.getElementById("SubCategoryMsgForSelect").innerHTML = "";
        }
    });

    $("#subname").keypress(function () {
        if (SubCategoryName) {
            document.getElementById("subname").style.borderColor = "lightgrey";
            document.getElementById("SubCategoryMsgForName").innerHTML = "";
        }
    });
});


function SubCategoryName() {
    var cname = document.getElementById("subname").value;
    var categoryname = /^[a-zA-Z]+$/;
    //var firstname = /^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{1,10}$/;
    if (cname == "") {
        document.getElementById("SubCategoryMsgForName").style.color = "red";
        document.getElementById("subname").style.borderColor = "red";
        document.getElementById("SubCategoryMsgForName").innerHTML = "Please enter subcategory name";
        return false;
    }
    else if (categoryname.test(cname) && cname.length >= 2 && cname.length <= 20) {
        document.getElementById("subname").style.borderColor = "lightgrey";
        document.getElementById("SubCategoryMsgForName").innerHTML = "";

        return true;
    }

    else {
        document.getElementById("SubCategoryMsgForName").style.color = "red";
        document.getElementById("subname").style.borderColor = "red";
        document.getElementById("SubCategoryMsgForName").innerHTML = "Please enter subcategory name.";
        return false;
    }
}
function selectDropDown() {
    var dropDownVal = document.getElementById("catname").value;
    //console.log(dropDownVal);
    if (dropDownVal == "") {
        //console.log("false section");  
        document.getElementById("SubCategoryMsgForSelect").innerHTML = "Please select category name";
        document.getElementById("SubCategoryMsgForSelect").style.color = "red";
        return false;
    }
    else {
        //console.log("true section");
        document.getElementById("SubCategoryMsgForSelect").innerHTML = "";
        //document.getElementById("SelectSectionlist").style.borderColor = "#dcdfe5";
        return true;
    }

}
function subCatValidate() {
    var sub = SubCategoryName();
    var drop = selectDropDown();
    if (sub && drop) {
        AddSubCategory()
        return true;
    } else {
        return false;
    }
}

function subcatInsert() {
    document.getElementById("success-box-subcat").style.display = "block";

    $("#close-success-subcat").click(function (event) { document.getElementById("success-box-subcat").style.display = "none"; })
    id = "close-success-subcat"
}

function subcatUpdate() {
    document.getElementById("success-box-subcat-update").style.display = "block";

    $("#close-success-subcat-update").click(function (event) { document.getElementById("success-box-subcat-update").style.display = "none"; })
    id = "close-success-subcat-update"
}


function dltsubcategory() {
    document.getElementById("success-dlt-subcat").style.display = "block";

    $("#close-success-dlt-subcat").click(function (event) { document.getElementById("success-dlt-subcat").style.display = "none"; })
    id = "close-success-dlt-subcat"
}

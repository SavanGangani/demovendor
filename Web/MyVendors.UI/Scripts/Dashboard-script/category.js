function CategoryName() {
    var cname = document.getElementById("addCat").value;
    var categoryname = /^[a-zA-Z ]+$/;


    if (cname == "") {
        document.getElementById("entercategoryValidateMessage").style.color = "red";
        document.getElementById("addCat").style.borderColor = "red";
        document.getElementById("entercategoryValidateMessage").innerHTML = "Please enter category name..";
        return false;
    }
    else if (categoryname.test(cname) && cname.length >= 2 && cname.length <= 20) {
        document.getElementById("addCat").style.borderColor = "lightgrey";
        document.getElementById("entercategoryValidateMessage").innerHTML = "";
        addcategory()
        return true;
    }

    else {
        document.getElementById("entercategoryValidateMessage").style.color = "red";
        document.getElementById("addCat").style.borderColor = "red";
        document.getElementById("entercategoryValidateMessage").innerHTML = "Please enter a valid Category name.";
        return false;
    }

}

$(document).ready(function () {
    $("#addCat").keyup(function () {
        if (CategoryName) {
            document.getElementById("addCat").style.borderColor = "lightgrey";
            document.getElementById("entercategoryValidateMessage").innerHTML = "";
        }
    });

    $("#upCat").keyup(function () {
        if (UpdateCategoryName) {
            document.getElementById("upCat").style.borderColor = "lightgrey";
            document.getElementById("upentercategoryValidateMessage").innerHTML = "";
        }
    })

    $("#reseteditcate").click(function () {
        document.getElementById("upCat").value = " ";
        document.getElementById("upCat").style.borderColor = "lightgrey";
        document.getElementById("upentercategoryValidateMessage").innerHTML = "";

    });
});

function UpdateCategoryName() {
    var cname = document.getElementById("upCat").value;
    var categoryname = /^[a-zA-Z ]+$/;


    if (cname == "") {
        document.getElementById("upentercategoryValidateMessage").style.color = "red";
        document.getElementById("upCat").style.borderColor = "red";
        document.getElementById("upentercategoryValidateMessage").innerHTML = "Please enter category name..";
        return false;
    }
    else if (categoryname.test(cname) && cname.length >= 2 && cname.length <= 20) {
        document.getElementById("upCat").style.borderColor = "lightgrey";
        document.getElementById("upentercategoryValidateMessage").innerHTML = "";
        updatecategory()
        return true;
    }

    else {
        document.getElementById("upentercategoryValidateMessage").style.color = "red";
        document.getElementById("upCat").style.borderColor = "red";
        document.getElementById("upentercategoryValidateMessage").innerHTML = "Please enter a valid Category name.";
        return false;

    }
}

function functionsuccess() {
    document.getElementById("success-box-cat").style.display = "block";

    $("#close-success-cat").click(function (event) { document.getElementById("success-box-cat").style.display = "none"; })
    id = "close-success-cat"
}

function functionsuccessdlt() {
    document.getElementById("success-dlt-cat").style.display = "block";

    $("#close-success-dlt-cat").click(function (event) { document.getElementById("success-dlt-cat").style.display = "none"; })
    id = "close-success-dlt-cat"
}

$(document).ready(function () {
    $("#resetcategory").click(function () {
        document.getElementById("addCat").value = " ";
        document.getElementById("addCat").style.borderColor = "lightgrey";
        document.getElementById("entercategoryValidateMessage").innerHTML = "";
    });

});

function AddSubCategory() {

    var id = $("#subid").val()
    var CourseData = {
        c_Id: $("#cid").val(),
        Name: $("#subname").val(),
    }
    //for new one
    if (id == '') {
        var settings = {
            "url": API_URL + "api/SubCategoryApi/",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(CourseData),
        };
        $.ajax(settings).done(function (response) {
            if (response) {
                logger("SubCategory", "Insert")
                $("#addcategory").data("kendoWindow").close()
                subcatInsert();
                setTimeout(function () {
                    document.getElementById("success-box-subcat").style.display = "none";
                    //displaySubCategory()
                    window.location.reload()
                }, 2000);
            }
            else {
                $("#addcategory").data("kendoWindow").close()
                alert_popup('Sub Category already exists !')
                setTimeout(function () {
                    document.getElementById("alert-box").style.display = "none";
                }, 2000);
            }

        });


    }
    else {

        var settings = {

            "url": API_URL + "api/SubCategoryApi/?c_sub_id=" + id,
            "method": "PUT",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(CourseData),
        };
        $.ajax(settings).done(function (response) {
            if (response) {
                logger("SubCategory", "Update")
                $("#addcategory").data("kendoWindow").close()
                subcatUpdate();
                setTimeout(function () {
                    document.getElementById("success-box-subcat-update").style.display = "none";
                    displaySubCategory()
                }, 2000);
            }
            else {
                $("#addcategory").data("kendoWindow").close()
                alert_popup('Sub Category already exists !')
                setTimeout(function () {
                    document.getElementById("alert-box").style.display = "none";
                }, 2000);
            }

        });
    }


    function subcatsuccess() {
        document.getElementById("subcat-success").style.display = "block";

        $("#close-subcat-success").click(function (event) {
            document.getElementById("subcat-success").style.display = "none";
        });
        id = "close-subcat-success"
    }


    function subwarning() {
        document.getElementById("subcat-warning").style.display = "block";

        $("#close-subcat-warning").click(function (event) {
            document.getElementById("subcat-warning").style.display = "none";
        });
        id = "close-subcat-warning"
    }



    function deletecourse(e) {

        //for multiple delete

        if (Array.isArray(e)) {
            var status = false;
            for (var i = 0; i < e.length; i++) {

                var settings = {
                    "url": API_URL + "api/SubCategoryApi/" + e[i],
                    "method": "DELETE",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json"
                    },
                    //"data": JSON.stringify(dataitem.id),
                };
                $.ajax(settings).done(function (response) {

                    status = true;


                });
            }

            // console.log(settings);

        }


            //for single delete
        else {
            var grid = $("#categoryGrid").data("kendoGrid");
            var row = e.select().closest("tr");
            var dataitem = grid.dataItem(row);

            $("#deletePopup").data("kendoWindow").center().open();
            $("#btnYesResponse").click(function () {
                // isDeleteDepartment();
                var settings = {
                    "url": API_URL + "api/SubCategoryApi?c_sub_id=" + dataitem.c_sub_Id,
                    "method": "DELETE",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json"
                    },
                    //"data": JSON.stringify(dataitem.id),
                };
                $.ajax(settings).done(function (response) {
                    if (response == true) {
                        window.location.reload();

                    }
                    else {
                    }
                });


            });
            $("#btnNoResponse").click(function () {

                $("#deletePopup").data("kendoWindow").close();
                window.location.reload();
                //Drop down
            });

            //cshtml

        }
    }
}

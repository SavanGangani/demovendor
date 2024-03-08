var ProfileName = '';
var User = {
    create: function () {
        FirstName: "";
        LastName: "";
        Contact: "";
        Id: "";

        return User;
    }
}

var ImageData = {
    create: function () {
        ImageName: "";
        Id: "";

        return ImageData;
    }
}
$(document).ready(function () {
    LoadContact();
});

//Set Profile Image Data
var tempImage;

function getImage() {

}
$(document).ready(function () {
    $("#txtOldpasword").change(function () {
        if (pswd()) {
            document.getElementById("txtOldpassword").style.borderColor = "lightgrey";
            document.getElementById("oldpasswordValidateMessage").innerHTML = "";
        }
    });
});
$(document).ready(function () {
    $("#txtNewPassword").change(function () {
        if (pswd()) {
            document.getElementById("txtNewPassword").style.borderColor = "lightgrey";
            document.getElementById("newpaswordValidateMessage").innerHTML = "";
        }
    });
});
$(document).ready(function () {
    $("#txtConfirmPassword").change(function () {
        if (pswd()) {
            document.getElementById("txtConfirmPassword").style.borderColor = "lightgrey";
            document.getElementById("copaswordValidateMessage").innerHTML = "";
        }
    });
});
$(document).ready(function () {
    $("input").change(function () {

        if (name()) {
            document.getElementById("txtfname").style.borderColor = "lightgrey";
            document.getElementById("txtlname").style.borderColor = "lightgrey";
            document.getElementById("lastNameValidateMessage").innerHTML = "";
            document.getElementById("firstNameValidateMessage").innerHTML = "";
            document.getElementById("txtmobile").style.borderColor = "lightgrey";
            document.getElementById("mobileValidateMessage").innerHTML = "";
        }

    });
});

var resetuserpass = {
    create: function () {
        oldpassword: "";
        newpassword: "";
        confirmpassword: "";

        return resetuserpass;
    }
}

function ResetPassword() {

    if (pswd()) {

        $(document).ready(function () {
            $("input").change(function () {

                document.getElementById("oldpasswordValidateMessage").innerHTML = "";
                document.getElementById("txtOldPassword").style.borderColor = "lightgrey";
                document.getElementById("copasswordValidateMessage").innerHTML = "";
                document.getElementById("txtNewPassword").style.borderColor = "lightgrey";
                document.getElementById("newpasswordValidateMessage").innerHTML = "";
                document.getElementById("txtConfirmPassword").style.borderColor = "lightgrey";

            });
        });


        var newuserpass = resetuserpass.create();
        newuserpass.oldpassword = $("#txtOldPassword").val();
        newuserpass.newpassword = $("#txtNewPassword").val();
        newuserpass.confirmpassword = $("#txtConfirmPassword").val();
        $.ajax({
            type: "Put",
            url: API_URL + "api/ResetPassAPI/" + $("#userid").val(),


            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(newuserpass),
            success: function (data) {

                sessionStorage.clear();
                //window.Location("Login/Login");
                // End foreach loop

                if (data.responseText == 0) {

                    document.getElementById("oldpasswordValidateMessage").style.color = "red";
                    document.getElementById("txtOldPassword").style.borderColor = "red";
                    document.getElementById("oldpasswordValidateMessage").innerHTML = "Current password doesn't match";
                }
                else if (data.responseText == 1) {
                    logger("Profile", "Reset Password")
                    window.location.href = "/UserRegister/SignIn";
                }

            },
            // End  Ajax success function

            failure: function (data) {
                console.log("There is an problem.");
            },
            // End Ajax failure function
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Request failed，info：" + textStatus + "  " + errorThrown);
            }
            // End Ajax Error function
        });
        //}
        return false;
    }
    else {
        return false;
    }
}



function LoadContact() {
    $.ajax({
        url: API_URL + "api/UserProfileApi/" + $("#userid").val(),
        method: "GET",
        success: function (data) {
            //console.log(data)

            if (data.ProfilePhoto == null) {
                var profile = data.Fname[0].toUpperCase() + data.Lname[0].toUpperCase();
                $(".ImgDiv").css("display", "none");
                $(".profileLetterDiv").css("display", "block");
                $(".ProfileImage").attr('src', '');
                ProfileName = profile;
                document.getElementById("imguser").innerHTML = profile;
                document.getElementById("imguser1").innerHTML = profile;
            }
            else {
                var profile = data.Fname[0].toUpperCase() + data.Lname[0].toUpperCase();
                document.getElementById("imguser").innerHTML = profile;
                document.getElementById("imguser1").innerHTML = profile;
                $(".profileLetterDiv").css("display", "none");

                $(".ImgDiv").css("display", "block");
                $("#ViewProfileImage").removeAttr("src").fadeIn("fast").attr('src', API_URL + 'Images/' + data.ProfilePhoto);
                $("#EditProfileImage").removeAttr("src").fadeIn("fast").attr('src', API_URL + 'Images/' + data.ProfilePhoto);
            }


            document.getElementById("lblemail1").innerText = data.Fname + "  " + data.Lname;
            document.getElementById("lbluname").innerText = data.Email;
            document.getElementById("lblfullname").innerText = data.Fname + "  " + data.Lname;
            document.getElementById("lblemail").innerText = data.Email;
            document.getElementById("lblmobile").innerHTML = data.MobileNo;


            document.getElementById("txtuname").value = data.Email;
            document.getElementById("txtfname").value = data.Fname;
            document.getElementById("txtlname").value = data.Lname;
            document.getElementById("txtemail").value = data.Email;
            document.getElementById("txtmobile").value = data.MobileNo;

        },
        error: function (err) {
            document.getElementById("alert-msg").innerHTML = "Something went wrong..";
            document.getElementById("alert-box").style.display = "block";
            setTimeout(function () {
                $("#alert-box").fadeOut(1000);
            }, 2000);

        }
    });
}
function UpdateProfile() {

    if (name()) {
        var UserData = User.create();
        UserData.Fname = $("#txtfname").val();
        UserData.Lname = $("#txtlname").val();
        UserData.MobileNo = $("#txtmobile").val();
        UserData.Cid = $("#userid").val();
        $.ajax({
            url: API_URL + "api/UserProfileApi/" + $("#userid").val(),
            method: "PUT",
            data: UserData,
            success: function (data) {
                console.log(data)
                
                if (data.responseText == 1) {
                    logger("Profile", "Update")
                    LoadContact();
                    $('.settings').removeClass('open');
                    $('.main-up').removeClass('close');
                    success_popup('Updated Successfully !')
                    setTimeout(function () {
                        document.getElementById("success-box").style.display = "none";
                    }, 2000);
                }
                else {
                    document.getElementById("alert-msg").innerHTML = "Something went wrong..";
                    document.getElementById("alert-box").style.display = "block";
                    setTimeout(function () {
                        $("#alert-box").fadeOut(1000);
                    }, 2000);
                }
            },
            error: function (err) {
                console.log(err)
            }
        });

        if (!(typeof (tempImage) === 'undefined')) {
            AddImage();
            localStorage.clear();
            location.reload();
        }

    }

}



function AddImage() {
    var fileData = new FormData();
    
    fileData.append("Profile", tempImage);
    $.ajax({
        url: API_URL + 'api/UserProfileApi/' + $("#userid").val(),
        type: 'POST',
        data: fileData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (fileName) {
        },

    });
    console.log(fileData)
    success_popup('Updated Successfully !')
    setTimeout(function () {
        document.getElementById("success-box").style.display = "none";
    }, 2000);
}

function ResetProfile() {
    $.ajax({
        url: API_URL + 'api/USerProfileApi/' + $("#userid").val(),
        type: 'DELETE',
        data: $("#userid").val(),
        success: function (data) {
            $(".ImgDiv").css("display", "none");
            $(".profileLetterDiv").css("display", "block");
            $(".ProfileImage").attr('src', '');

        },

    });

}


function SetImage() {
    var profilePic = document.getElementById('fileProfileImage');
    var profileUpload = profilePic.value;
    var Extension = profileUpload.substring(profileUpload.lastIndexOf('.') + 1).toLowerCase();

    if (Extension == "jpg" || Extension == "jpeg" || Extension == "png") {

        if (profilePic.files && profilePic.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#EditProfileImage').attr('src', e.target.result);
            }
            reader.readAsDataURL(profilePic.files[0]);

            tempImage = event.target.files[0];
            var filePath = $('#fileProfileImage').val();
            var tmppath = URL.createObjectURL(event.target.files[0]);
            $(".profileLetterDiv").css("display", "none");
            $(".ImgDiv").css("display", "block");
            $("#EditProfileImage").fadeIn("fast").attr('src', tmppath);
        }

    }
    else {
        alert_popup('Picture must be jpg , jpeg or png !')
        setTimeout(function () {
            document.getElementById("alert-box").style.display = "none";
        }, 2000);
    }

}

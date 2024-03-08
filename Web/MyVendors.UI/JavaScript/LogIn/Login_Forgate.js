function ExistEmail() {
    var ForgotByEmail = { ForgotByEmail: $('#txtemailsend').val() }

    console.log(ForgotByEmail);
    var mess = $("#username");

    var settings =
        {
            "url": API_URL + "api/ForgotPasswordApi",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(ForgotByEmail),
        };
    $.ajax(settings).done(function (response) {

        //console.log(response.c_id);  // if passed Bal.models.tblLogin();
        if (response != "") {

            ForgotByEmail = { ForgotByEmail: $('#txtemailsend').val(), fname: response } // setting response value i.e received firstname to variable fname
            var settings1 =
                 {
                     "url": API_URL + "api/ForgotPasswordAPI/SendEmail",
                     "method": "POST",
                     "timeout": 0,
                     "headers": {
                         "Content-Type": "application/json"
                     },
                     "data": JSON.stringify(ForgotByEmail),
                 };
            $.ajax(settings1).done(function (response) {

                // if (response == 1)
                functionsuccess()

            });
        } else {
            functionalert_user()
        }
    });
}




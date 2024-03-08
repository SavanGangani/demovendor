function success_popup(msg) {
    $("#success-msg").text(msg)
    document.getElementById("success-box").style.display = "block";

    $("#close-success").click(function (event) {
        document.getElementById("success-box").style.display = "none";
    });
    id = "close-success"
}


function alert_popup(msg) {
    $("#alert-msg").text(msg)
    document.getElementById("alert-box").style.display = "block";

    $("#close-alert").click(function (event) {
        document.getElementById("alert-box").style.display = "none";
    });
    id = "close-alert"
}
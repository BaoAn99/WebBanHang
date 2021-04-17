function confirmEmail(u,email,code) {
    $.ajax({
        type: 'POST',
        url: u,
        dataType: 'json',
        data: {
            email: email,
            code: code
        },
        success: function (response) {
            if (response.status) {
                toastr.success(response.mess);
                setTimeout(function () {
                    window.location.href = "/";
                }, 3000);

            } else {
                toastr.error(response.mess);
            }

        },
        // Display errors if any in the Bootstrap alert <div>
        error: function (jqXHR) {
            toastr.error(jqXHR.responseText);

        }
    });
}
$(document).ready(function () { 
    var url = "http://localhost:49918/Account/Confirm";
    $("#btnConfirm").click(function () {
        var code = $("#code").val();
        var email = $("#email").val();
        confirmEmail(url,email,code);
    });
   
});
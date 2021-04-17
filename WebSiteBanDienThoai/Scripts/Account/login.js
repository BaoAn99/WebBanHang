function login() {
    $.ajax({
        type: 'POST',
        url: 'http://localhost:1235/Account/CheckLogin',
        dataType: 'json',
        data: { username: $('#txtUsername').val(), password: $('#txtPassword').val(), },
        success: function (response) {
            if (response.status) {
                toastr.success(response.mess);
                setTimeout(function () {
                    window.location.href = "/";
                }, 3000);

            } else {
                if(response.mess.length<100)
                    toastr.error(response.mess);
                else
                    toastr.error("Tài khoản hoặc mật khẩu không chính xác!");
            }

        },
        // Display errors if any in the Bootstrap alert <div>
        error: function (jqXHR) {
            if (jqXHR.responseText.length<100)
                toastr.error(jqXHR.responseText);
            else
                toastr.error("Tài khoản hoặc mật khẩu không chính xác!");
        }
    });
};
$(document).ready(function () {

});
$(document).ready(function () {
    $("#frm-login").submit(function (e) {
        if ($("#username").val() == '') {
            $('#err_login').html('<div class="alert alert-danger">Username chưa nhập</div>');
            $("#username").focus();
            return false;
        }
        else if ($("#password").val() == '') {
            $('#err_login').html('<div class="alert alert-danger">Password chưa nhập</div>');
            $("#password").focus();
            return false;
        }
        else {
            var form_data_login = {
                username: $("#username").val(),
                password: $("#password").val()
            };
            $.ajax({
                url: '/Home/DangNhap',
                type: 'POST',
                async: true,
                data: form_data_login,
                success: function (msg_login) {
                    //alert(msg);
                    if (msg_login == 'false') {
                        $('#err_login').html('<div class="alert alert-danger">Tên hoặc mật khẩu không chính xác</div>');
                        $("#password").val("");
                        $("#password").focus();
                        return false;
                    }
                    else if (msg_login == 'er_block') {
                        $('#err_login').html('<div class="alert alert-danger">Tài khoản đang bị khóa</div>');
                    }
                    else {
                        $("#login_here").hide();
                        $(".modal-footer").hide();

                        $('#err_login').html('<div class="alert alert-success"><strong>Đăng nhập thành công</strong><span> Hệ thống tự chuyển sau vài giây ...</span></div>');
                        setTimeout(
                            //chuyển đến địa chỉ msg_login của controler gửi qua
                            function () {
                                window.location.href = '' + msg_login + '';
                            }, 2000);
                    }
                }
            });
            return false;
        }
    });
})
//Load wating bar
$(document).ajaxStart(function () {
    $("#waiting").show();
}).ajaxStop(function () {
    $("#waiting").hide();
});
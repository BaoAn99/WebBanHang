var notify = {
    methods: {
        info: {
            _saveLoad: function(mess, url) {
                var notify = jQuery.notify('<strong>Đang lưu</strong> Không được đóng trình duyện...', {
                    icon: 'fa fa-paw',
                    type: 'info',
                    allow_dismiss: false,
                    showProgressbar: true
                });
                setTimeout(function() {
                    notify.update('message', '<strong>Đang tải</strong> Dữ liệu.');
                }, 1000);

                setTimeout(function() {
                    notify.update('message', '<strong>Đang lưu</strong> Dữ liệu.');
                }, 2000);

                setTimeout(function() {
                    notify.update('message', '<strong>Đang kiểm tra</strong> Lỗi.');
                }, 3000);

                setTimeout(function() {
                    notify.update('message', '<strong>Thông báo:</strong> ' + mess + '.');
                }, 4000);
            },
            _showDialog: function(mess, url) {
                $.notify({
                    // options
                    title: '<strong>Xin lỗi</strong>',
                    message: mess,
                    icon: 'glyphicon glyphicon-info-sign',
                }, {
                    // settings
                    element: 'body',
                    position: null,
                    type: "info",
                    allow_dismiss: true,
                    newest_on_top: false,
                    showProgressbar: false,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    offset: 20,
                    spacing: 10,
                    z_index: 1031,
                    delay: 3300,
                    timer: 1000,
                    url_target: '_blank',
                    mouse_over: null,
                    animate: {
                        enter: 'animated bounceInDown',
                        exit: 'animated bounceOutUp'
                    },
                    onShow: null,
                    onShown: null,
                    onClose: null,
                    onClosed: null,
                    icon_type: 'class',
                });
            }
        },
        success: {
            _showDialog: function(mess, url) {
                $.notify({
                    // options
                    title: '<strong>Chúc mừng</strong>',
                    message: mess,
                    icon: 'glyphicon glyphicon-ok',
                    url: url,
                    target: '_blank'
                }, {
                    // settings
                    element: 'body',
                    //position: null,
                    type: "success",
                    //allow_dismiss: true,
                    //newest_on_top: false,
                    showProgressbar: false,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    offset: 20,
                    spacing: 10,
                    z_index: 1031,
                    delay: 3300,
                    timer: 1000,
                    url_target: '_blank',
                    mouse_over: null,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutRight'
                    },
                    onShow: null,
                    onShown: null,
                    onClose: null,
                    onClosed: null,
                    icon_type: 'class',
                });
            }
        },
        danger: {

        },
        warning: {
			_showDialog:function(mess,url){
				$.notify({
					// options
					title: '<strong>Cảnh báo</strong>',
					message: mess,
					icon: 'glyphicon glyphicon-warning-sign',
				}, {
					// settings
					element: 'body',
					position: null,
					type: "warning",
					allow_dismiss: true,
					newest_on_top: false,
					showProgressbar: false,
					placement: {
						from: "top",
						align: "right"
					},
					offset: 20,
					spacing: 10,
					z_index: 1031,
					delay: 3300,
					timer: 1000,
					url_target: '_blank',
					mouse_over: null,
					animate: {
						enter: 'animated bounceIn',
						exit: 'animated bounceOut'
					},
					onShow: null,
					onShown: null,
					onClose: null,
					onClosed: null,
					icon_type: 'class',
				});
			}
		},
        call_mess_1: function(mess, url) {
            jQuery.notify({
                // options
                icon: 'glyphicon glyphicon-warning-sign',
                title: 'Thông báo:',
                message: mess,
                url: url,
                target: '_blank'
            }, {
                // settings
                element: 'body',
                position: null,
                type: "success",
                allow_dismiss: true,
                newest_on_top: false,
                showProgressbar: false,
                placement: {
                    from: "top",
                    align: "right"
                },
                offset: 20,
                spacing: 10,
                z_index: 1031,
                delay: 5000,
                timer: 1000,
                url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                onShow: null,
                onShown: null,
                onClose: null,
                onClosed: null,
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
        },
        call_delete_1: function(mess, url) {
            var notify = jQuery.notify('<strong>Đang xóa</strong> Không được đóng trình duyện...', {
                icon: 'fa fa-paw',
                type: 'success',
                allow_dismiss: false,
                showProgressbar: true
            });
            setTimeout(function() {
                notify.update('message', '<strong>Đang xóa</strong> Không được đóng trình duyện...');
            }, 1000);

            setTimeout(function() {
                notify.update('message', '<strong>Đang xóa</strong> Không được đóng trình duyện...');
            }, 2000);

            setTimeout(function() {
                notify.update('message', '<strong>Đang xóa</strong> Không được đóng trình duyện...');
            }, 2000);

            setTimeout(function() {
                notify.update('message', '<strong>Đang kiểm tra Lỗi.</strong>');
            }, 3000);

            setTimeout(function() {
                notify.update('message', '<strong>Thông báo:</strong> ' + mess + '.');
            }, 4000);
        }
    }
}
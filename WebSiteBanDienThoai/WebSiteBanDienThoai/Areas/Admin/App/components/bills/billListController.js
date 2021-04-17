(function (app) {
    app.controller('billListController', function ($scope, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.GetData = GetData;
        $scope.cancel = cancel;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.lstData.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
        function getApprove(status) {
            if (status === 0) {
                return {
                    className: "warning",
                    text: "Đang giao"
                };
            } else if (status === 1) {
                return {
                    className: "success",
                    text: "Đã giao"
                };
            } else if (status === -1) {
                return {
                    className: "danger",
                    text: "Đã hủy"
                };
            }
        }
        function GetData() {
            var dt = commonService.Start();
            apiService.get('GetBills', null, function (result) {
                if (result.data.status) {
                    $scope.lstData = result.data.obj;
                    for (var i = 0; i < $scope.lstData.length; i++) {
                        $scope.lstData[i].DataStatus = getApprove($scope.lstData[i].Status);
                    }
                }
            }, function (error) {
                notificationService.displayError(error.data);
                });
             
            dt.finish();
        }
        function cancel(id) {
            bootbox.confirm("Hủy đơn hàng: " + id + " ?", function (result) {
                if (result) {
                    apiService.post("CancelBill?billId=" + id, null, function (result) {
                        notificationService.displaySuccess('Đơn hàng được hủy bỏ');
                        GetData();
                    }, function (error) {
                        notificationService.displayError('Có lỗi xảy ra.');
                    });
                }
            });

        }
        GetData();
    });
})(angular.module('AdmShopMobile.products'));
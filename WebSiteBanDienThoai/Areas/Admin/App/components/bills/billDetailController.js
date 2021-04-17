(function (app) {
    app.controller('billDetailController', function ($scope, $stateParams, commonService, apiService, notificationService, $state) {
        $scope.pageSize = 5;
        $scope.GetData = GetData;
        $scope.cancel = cancel;
        $scope.processed = processed;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.lstData.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });

        function cancel() {
            bootbox.confirm("Hủy đơn hàng: " + $stateParams.Id + " ?", function (result) {
                if (result) {
                    apiService.post("CancelBill?billId=" + $stateParams.Id, null, function (result) {
                        notificationService.displaySuccess('Đơn hàng được hủy bỏ');
                        $state.go("bills");
                    }, function (error) {
                        notificationService.displayError('Có lỗi xảy ra.');
                    });
                }
            });

        }

        function processed() {
            bootbox.confirm("Đã xử lý hóa đơn: " + $stateParams.Id + " ?", function (result) {
                if (result) {
                    apiService.post("ProcessedBill?billId=" + $stateParams.Id, null, function (result) {
                        notificationService.displaySuccess('Đơn hàng xác nhận được giao');
                        $state.go("bills");
                    }, function (error) {
                        notificationService.displayError('Có lỗi xảy ra.');
                    });
                }
            });

        }

        function GetData() {
            var dt = commonService.Start();
            apiService.get('GetBill?billId=' + $stateParams.Id, null, function (result) {
                if (result.data.status) {
                    $scope.lstData = result.data.obj;


                }
            }, function (error) {
                notificationService.displayError(error.data);
            });

            apiService.get("GetBillStatus?billId=" + $stateParams.Id,
                null,
                function (result) {
                    if (result.data.status) {
                        $scope.status = result.data.obj;
                    } else {
                        $scope.status = false;
                    }
                },
                function (error) {
                    notificationService.displayError(error.data);
                });

            dt.finish();
        }

        GetData();
    });
})(angular.module('AdmShopMobile.products'));
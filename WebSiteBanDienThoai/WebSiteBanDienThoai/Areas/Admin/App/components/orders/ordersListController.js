(function (app) {
    app.controller('ordersListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.getData = getData;
        $scope.Delete = Delete; 
        $scope.getPaymentName = getPaymentName;

        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.lstData.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
        function parseJsonDate(jsonDateString) {
            return new Date(parseInt(jsonDateString.replace('/Date(', '')));
        }

        function getPaymentName(id) {
            if (id !== null) {
                if ($scope.payments !== undefined) {
                    return $scope.payments.find(x => x.PaymentID === id).Name;
                } else {
                    return "";
                }
            } else {

                return "Chưa xét";
            }
        }
        function getApprove(status) {
            if (status === 0) {
                return { 
                    className: "warning",
                    text: "Chưa xác thực" 
                };
            } else if (status === 1) {
                return { 
                    className: "success",
                    text: "Đã xác thực"
                };
            } else if (status === -1) {
                return {
                    className: "danger",
                    text: "Đã hủy"
                };
            }
        }
        function getData() {
            var dt = commonService.Start();
            apiService.get('GetOrders', null, function (result) {
                if (result.data.status) {
                    $scope.lstData = result.data.obj;
                    for (var i = 0; i < $scope.lstData.length; i++) {
                        $scope.lstData[i].DataStatus = getApprove($scope.lstData[i].Status);
                    }
                }
            }, function (error) {
                notificationService.displayError(error.data);
                });

            apiService.get('GetPayments', null, function (result) {
                if (result.data.status) {
                    $scope.payments = result.data.obj; 
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa sản phẩm: " + name + " ?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.post('DelBill?id=' + id, null, function (result) {
                        notificationService.displaySuccess("Xóa thành công");
                        GetData();
                    }, function (error) {
                        notificationService.displayError(error.data);
                    });
                    dt.finish();
                }
            });
        }
        getData();
    });
})(angular.module('AdmShopMobile.products'));
(function (app) {
    app.controller('paymentListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.getData = getData;
        $scope.Delete = Delete;
        $scope.getType = getType;

        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.LstPost.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() === "") {
                $scope.pageSize = 5;
                return;
            }
        });

        function getData() {
            var dt = commonService.Start();
            apiService.get('GetPayments', null, function (result) {
                $scope.Lst = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();
        }

        function getType(type) {
            switch (type) {
                case 0:
                    return "NV Bán hàng";
                case 1:
                    return "NV Giao hàng";
                default:
                    return "Unknown";
            }
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa: <b>" + name + " </b>?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.post('DelPayment?id=' + id, null, function (result) {
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
(function (app) {
    app.controller('supplierListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.GetData = GetData;
        $scope.Delete = Delete;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.LstSupplier.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
        //xóa
        function GetData() {
            var dt = commonService.Start();
            apiService.get('GetSuppliers', null, function (result) {
                $scope.LstSupplier = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa : " + name + " ?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.post('DelSupplier?entity=' + id, null, function (result) {
                        notificationService.displaySuccess("Xóa thành công");
                        GetData();
                    }, function (error) {
                        notificationService.displayError(error.data);
                    });
                    dt.finish();
                }
            });
        }
        GetData();
    });
})(angular.module('AdmShopMobile.products'));
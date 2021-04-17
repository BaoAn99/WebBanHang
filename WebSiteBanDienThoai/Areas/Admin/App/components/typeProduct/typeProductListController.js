(function (app) {
    app.controller('typeProductListController', function ($scope,$state, $http, commonService, apiService, notificationService) {
        $scope.TypeProducts = [];
        $scope.pageSize = 5;
        $scope.Delete = Delete;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.LstSanPham.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
        //xóa
        function Init() {
            var dt = commonService.Start();
            apiService.get('getTypeProducts/', null, function (result) {
                $scope.TypeProducts = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data.mess);
            });
            dt.finish();
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa loại sản phẩm: " + name + " ?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.post('delTypeProduct?typeProductID=' + id, null, function (response) {
                        if(response.data.status)
                            notificationService.displaySuccess("Xóa thành công");
                        else
                            notificationService.displayError(response.data.mess);
                        $state.reload();
                    }, function (error) {
                        notificationService.displayError(error.data);
                    });
                    dt.finish();
                }
            });
        }
        Init();
    });
})(angular.module('AdmShopMobile.products'));
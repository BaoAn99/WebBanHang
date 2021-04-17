(function (app) {
    app.controller('productListController', function ($scope,$state, $http, commonService, apiService, notificationService) {
        //Declare Properties
        $scope.pageSize = 5;
        //Declare method
        $scope.Init = Init;
        $scope.Delete = Delete;
        //Method content
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.LstSanPham.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
        //Hàm thực hiện lấy dữ liệu của sản phẩm từ "ProductController" tại "Gets" action 
        function Init() {
            var dt = commonService.Start();
            apiService.get('getProducts', null, function (result) {
                if (result.data.status) {
                    $scope.LstSanPham = result.data.obj;
                }
            }, function (error) {
                notificationService.displayError(error.data.mess);
            });
            dt.finish();
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa sản phẩm: " + name + " ?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.post('delProduct?productID=' + id, null, function (result) {
                        if (result.data.status) {
                            notificationService.displaySuccess("Xóa thành công");
                            $state.reload();
                        }
                        else {
                            notificationService.displayError("Xóa Ko thành công");
                        }
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
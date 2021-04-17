(function (app) {
    app.controller('PostListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.GetData = GetData;
        $scope.Delete = Delete;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.LstPost.length;
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
            apiService.get('Articles/Get', null, function (result) {
                $scope.LstPost = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa bài viết: <b>" + name + " </b>?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.del('Articles/' + id, null, function (result) {
                        notificationService.displayError("Xóa thành công");
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
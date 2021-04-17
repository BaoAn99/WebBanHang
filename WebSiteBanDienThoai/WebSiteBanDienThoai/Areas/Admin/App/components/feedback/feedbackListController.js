(function (app) {
    app.controller('feedbackListController', function ($scope,$http, commonService, apiService, notificationService) {
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

        function GetData() {
            var dt = commonService.Start();
            apiService.get('GetComments', null, function (result) {
                $scope.Lst = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();
        }
        function Delete(id, name) {
            bootbox.confirm("Bạn chắc chắn muốn xóa: <b>" + name + " </b>?", function (result) {
                if (result) {
                    var dt = commonService.Start();
                    apiService.post('DelComment?Id=' + id, null, function (result) { 
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
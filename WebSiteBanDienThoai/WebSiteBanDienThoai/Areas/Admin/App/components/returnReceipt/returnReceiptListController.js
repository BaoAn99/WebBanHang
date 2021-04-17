(function (app) {
    app.controller('returnGuaranteeListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Danh sách",
            function: "Trả bảo hành",
            pageSize: 5,
            payList: []
        };
        $scope.methods = {
            _delete: function (id, name) {
                bootbox.confirm("Bạn chắc chắn muốn xóa "+$scope.properties.title.toLowerCase()+" : <b>" + name + "</b> ?", function (result) {
                    if (result) {
                        var dt = commonService.Start();
                        apiService.post('DelPay?id=' + id, null, function (result) {
                            if (result.data.status) {
                                notificationService.displaySuccess("Xóa thành công");
                                Init();
                            } else {
                                notificationService.displayError("Xóa không thành công");
                            }                            
                        }, function (error) {
                            notificationService.displayError(error.data);
                        });
                        dt.finish();
                    }
                });
            }
        };
        $scope.events = {
            _keypress: function () {
                    if ($("#input-search").val() != "") {
                        $scope.properties.pageSize = $scope.properties.payList.length;
                    } else {
                        $scope.properties.pageSize = 5;
                    }                
            }
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('GetPays', null, function (result) {
                $scope.properties.payList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
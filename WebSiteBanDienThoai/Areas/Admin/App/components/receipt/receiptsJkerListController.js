(function (app) {
    app.controller('guaranteeListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Danh sách",
            function: "Bảo hành",
            pageSize: 5,
            receiptList: []
        };
        $scope.methods = {
            _delete: function (id, name) {
                bootbox.confirm("Bạn chắc chắn muốn xóa "+$scope.properties.title.toLowerCase()+" : <b>" + name + "</b> ?", function (result) {
                    if (result) {
                        var dt = commonService.Start();
                        apiService.post('DelReceipt?id=' + id, null, function (result) {
                            if (result.data.status) {
                                notificationService.displaySuccess("Xóa thành công");
                            } else {
                                notificationService.displaySuccess("Xóa không thành công");
                            }
                            Init();
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
                        $scope.properties.pageSize = $scope.properties.receiptList.length;
                    } else {
                        $scope.properties.pageSize = 5;
                    }                
            }
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('GetReceipts', null, function (result) {
                $scope.properties.receiptList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
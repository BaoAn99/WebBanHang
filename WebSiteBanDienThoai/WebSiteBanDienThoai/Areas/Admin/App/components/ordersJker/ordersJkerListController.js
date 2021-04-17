(function (app) {
    app.controller('receiptListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Danh sách",
            function: "Hóa đơn nhập",
            pageSize: 5,
            orderList: []
        };
        $scope.methods = {
            _delete: function (id, name) {
                bootbox.confirm("Bạn chắc chắn muốn xóa "+$scope.properties.title.toLowerCase()+" : <b>" + name + "</b> ?", function (result) {
                    if (result) {
                        var dt = commonService.Start();
                        apiService.post('DelOrderJker?orderID=' + id, null, function (result) {
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
                $("#input-search").keypress(function () {
                    $scope.properties.pageSize = $scope.lstData.length;
                });
            },
            _focusout: function () {
                $("#input-search").focusout(function () {
                    if ($("#input-search").val() == "") {
                        $scope.properties.pageSize = 5;
                        return;
                    }
                });
            }
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('GetOrdersJker', null, function (result) {
                $scope.properties.orderList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            $scope.events._keypress();
            $scope.events._focusout();
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
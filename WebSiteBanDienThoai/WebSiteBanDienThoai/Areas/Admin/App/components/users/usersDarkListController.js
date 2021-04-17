(function (app) {
    app.controller('usersDarkListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Tài khoản BLock",
            function: "Danh sách",
            pageSize: 5,
            accList: []
        };
        $scope.title = "Tài khoản";
        $scope.methods = {
            _delete: function (id, name) {
                bootbox.confirm("Bạn chắc chắn muốn xóa : <b>" + name + "</b> ?", function (result) {
                    if (result) {
                        var dt = commonService.Start();
                        apiService.post('deleteAccount?userName=' + id, null, function (result) {
                            notificationService.displaySuccess("Xóa thành công");
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
                    $scope.pageSize = $scope.lstData.length;
                });
            },
            _focusout: function () {
                $("#input-search").focusout(function () {
                    if ($("#input-search").val() == "") {
                        $scope.pageSize = 5;
                        return;
                    }
                });
            }
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('getAccounts?isLocked=true', null, function (result) {
                $scope.properties.accList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            $scope.events._keypress();
            $scope.events._focusout();
            dt.finish();
        }
        //xóa        
        Init();
    });
})(angular.module('AdmShopMobile.products'));
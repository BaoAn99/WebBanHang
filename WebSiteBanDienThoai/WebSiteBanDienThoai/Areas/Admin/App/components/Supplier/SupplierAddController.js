(function (app) {
    app.controller('supplierAddController', function ($scope, $http, commonService, apiService, notificationService, $state) {


        $scope.elm = {};
        $scope.LstSupplier = [];
        $scope.exist = false;

        var action = {
            _add: function (elm, url, returnUrl) {
                apiService.post(url, elm, function (result) {
                    notificationService.displaySuccess('Thêm mới thành công');
                    $state.go(returnUrl);
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
            }
        };
        $scope._checkExist = function () {
            $scope.exist = false;
            $.each($scope.LstSupplier, function (index, elm) {
                if (elm.Name.toLowerCase() == $scope.elm.Name.trim().toLowerCase()) {
                    $scope.exist = true;
                }
            });
        }
        //buildEvent
        $scope.add = function () {
            action._add($scope.elm, 'AddSupplier', 'suppliers');
        };
        function Init() {
            apiService.get('GetSuppliers', null, function (result) {
                $scope.LstSupplier = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        Init();
    });
}(angular.module('AdmShopMobile.products')));
(function (app) {
    app.controller('supplierEditController',
        function ($scope, $http, $stateParams, commonService, apiService, notificationService, $state) {
            $scope.elm = {};
            $scope.LstSupplier = [];
            $scope.exist = false;
            var action = {
                _getData: function (url) {
                    apiService.get(url + '?Id=' + $stateParams.ID, null, function (result) {
                        if (result.data.status) {
                            $scope.elm = result.data.obj;
                            $scope.elm.Birthday = new Date($scope.elm.Birthday);
                        }

                    }, function (error) {
                        notificationService.displayError(error.data);
                    });
                },
                _edit: function (elm, url, returnUrl) {
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
            $scope.edit = function () {
                action._edit($scope.elm, 'EditSupplier', 'suppliers');
            };
            $scope.init = function () {
                action._getData("GetSupplier");
                apiService.get('GetSuppliers', null, function (result) {
                    $scope.LstSupplier = result.data.obj;
                }, function (error) {
                    notificationService.displayError(error.data);
                });
            };

            $scope.init();
        });
}(angular.module('AdmShopMobile.products')));
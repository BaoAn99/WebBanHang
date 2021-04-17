(function (app) {
    app.controller('employeeAddController', employeeAddController);
    function employeeAddController($scope, $http, apiService, commonService, notificationService, $state) {



        $scope.elm = {};

        var action = {
            _add: function (elm, url, returnUrl) {
                apiService.post(url, elm, function (result) {
                    if (result.data.status) {
                        notificationService.displaySuccess(result.data.mess);
                    } else {
                        notificationService.displayError(result.data.mess);
                    }
                    $state.go(returnUrl);
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
            }

        };
        $scope.prop = {
             employeeTypes: [
                {
                    value: 0,
                    text: "NV Bán hàng"
                }, {
                    value: 1,
                    text: "NV Giao Hàng"
                }]
        };
        //buildEvent
        $scope.add = function () {
            action._add($scope.elm, 'AddEmployee', 'employees');
        };
    }
})(angular.module('AdmShopMobile.products'));
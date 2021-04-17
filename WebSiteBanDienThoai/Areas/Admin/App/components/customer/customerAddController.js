(function (app) {
    app.controller('customerAddController', customerAddController);
    function customerAddController($scope, $http, apiService, commonService, notificationService, $state) {



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
        var prop = {
            genders: [{
                value: true,
                text: "Nam"
            }, {
                value: false,
                text: "Nữ"
            }]
        };
        //buildEvent
        $scope.add = function () {
            action._add($scope.elm, 'AddCustomer', 'customers');
        };
    }
})(angular.module('AdmShopMobile.products'));
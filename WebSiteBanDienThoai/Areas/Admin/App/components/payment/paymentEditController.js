(function (app) {
    app.controller('paymentEditController', paymentEditController);

    function paymentEditController($scope, $stateParams, $http, commonService, apiService, notificationService, $state) {
         
        $scope.elm = {};
        $scope.prop = {
            status: [
                {
                    value: true,
                    text: "Khả dụng"
                }, {
                    value: false,
                    text: "Vô hiệu hóa"
                }]
        };
        var action = {
            _getData: function (url) {
                apiService.get(url+'?Id=' + $stateParams.ID, null, function (result) {
                    if (result.data.status) {
                        $scope.elm = result.data.obj;
                    }

                }, function (error) {
                    notificationService.displayError(error.data);
                });
            },
            _edit: function (elm, url, returnUrl) {
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
        
        //buildEvent
        $scope.edit = function () {
            action._edit($scope.elm, 'EditPayment', 'payments');
        }; 
        $scope.init = function () {
            action._getData("GetPayment"); 
        }; 

         $scope.init();
    }
})(angular.module('AdmShopMobile.products'));
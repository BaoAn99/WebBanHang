(function (app) {
    app.controller('typeProductEditController', producerEditController);

    function producerEditController($scope, $stateParams, $http, $state, commonService, apiService, notificationService) {
        //init object Product
        $scope.TypeProduct = {};
        $scope.TypeProducts = [];

        //init event
        $scope.Put = Put;
        //
        function Put() {
            var dt = commonService.Start();
            apiService.post('putTypeProduct', $scope.TypeProduct,
                function (result) {
                    notificationService.displaySuccess(result.data.mess);
                    $state.go('typeproducts');
                }, function (error) {
                    notificationService.displayError(result.data.mess);
                });
            dt.finish();
        }
        function checkTypeProductName()
        {

            if($scope.TypeProducts.find(tp=>tp.TypeProductName === $scope.TypeProduct.TypeProductName)!=null)
            {

            }
        }
        //
        function Init() {
            var dt = commonService.Start();
            apiService.get('getTypeProduct?TypeProductID=' + $stateParams.TypeProductID, null,
                function (result) {
                    $scope.TypeProduct = result.data.obj;
                }, function (error) {

                });
            dt.finish();
        }
        Init();
    };
})(angular.module('AdmShopMobile.products'));
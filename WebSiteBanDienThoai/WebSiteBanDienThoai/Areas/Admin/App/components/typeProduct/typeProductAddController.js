(function (app) {
    app.controller('typeProductAddController', productCategoryAddController);
    function productCategoryAddController($scope, $http, commonService, apiService, notificationService, $state) {
        //init object Product
        $scope.TypeProduct = {};
        $scope.TypeProducts = [];

        //init event
        $scope.Post = Post;
        //
        function Post() {
            var dt = commonService.Start();
            apiService.post('postTypeProduct', $scope.TypeProduct,
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
            apiService.get('getTypeProducts',null,
                function (result) {
                    $scope.TypeProducts = result.data.obj;
                }, function (error) {

                });
            dt.finish();
        }
        Init();
    }
})(angular.module('AdmShopMobile.products'));
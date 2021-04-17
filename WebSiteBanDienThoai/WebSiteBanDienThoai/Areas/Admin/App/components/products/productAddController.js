(function (app) {
    app.controller('productAddController', productAddController);
    function productAddController($scope, $http, commonService, apiService, notificationService, $state) {
        var UrlGetDataforeign = "../Dashboard/getAllProducer";
        //init object Product
        $scope.pro = {
        };
        $scope.TypeProducts = [];
        $scope.Suppliers = [];
        //Auto create alias to Product Name
    
        //init object Ckfider
        $scope.ChooseImage = ChooseImage;
        //event add Product
        $scope.Post = Post;

    
       
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.pro.Images = fileUrl;
                })
            }
            finder.popup();
        }
        $scope._changeCheckBox = function () {
            if ($scope.pro.Status = true) {
                $("#pro-status").removeClass("active");
                $scope.pro.Status == false;
            } else {
                $("#pro-status").addClass("active");
                $scope.pro.Status = true;
            }
        }
      
        function Post() {
            var dt = commonService.Start();
            apiService.post('postProduct', $scope.pro,
                function (result) {
                    notificationService.displaySuccess(result.data.mess);
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError(result.data.mess);
                });
            dt.finish();
        }

        function Init()
        {
            var dt = commonService.Start();
            apiService.get('getTypeProducts', $scope.pro,
                function (result) {
                    $scope.TypeProducts = result.data.obj;
                }, function (error) {
                    
                });
            apiService.get('getSuppliers', $scope.pro,
                function (result) {
                    $scope.Suppliers = result.data.obj;
                }, function (error) {

                });
            dt.finish();
        }
        Init();

    }
})(angular.module('AdmShopMobile.products'));
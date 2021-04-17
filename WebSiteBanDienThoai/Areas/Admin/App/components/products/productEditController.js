(function (app) {
    app.controller('productEditController', productEditController);
    function productEditController($scope, $stateParams, $http, commonService, apiService, notificationService, $state) {
        var UrlGetDataforeign = "../Dashboard/getAllProducer";

        //init object Product
        $scope.pro = {
        };
        $scope.ChooseImage = ChooseProductImage;
        //event add Product
        $scope.Init = Init;

        $scope.Put = Put;


        function ChooseProductImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.pro.Images = fileUrl;
                })
            }
            finder.popup();
        }
        $scope._changeCheckBox = function () {
            if ($scope.pro.Status == true) {
                $("#pro-status").removeClass("active");
                $scope.pro.Status = false;
            } else {
                $("#pro-status").addClass("active");
                $scope.pro.Status = true;
            }
        }

        function Init() {
            var dt = commonService.Start();
            apiService.get('getProduct?productID=' + $stateParams.ProductID, null, function (result) {
                if (result.data.status) {
                    $scope.pro = result.data.obj;
                    if ($scope.pro.Status == false) {
                        $("#pro-status").removeClass("active");
                    } else {
                        $("#pro-status").addClass("active");
                    }
                }

            }, function (error) {
                notificationService.displayError(error.data);
            });
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
        function Put() {
            var dt = commonService.Start();
            apiService.post('putProduct', $scope.pro,
                function (result) {
                    notificationService.displaySuccess(result.data.mess);
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError(result.data.mess);
                });
            dt.finish();
        }

        //Run init

        Init();

    }
})(angular.module('AdmShopMobile.products'));
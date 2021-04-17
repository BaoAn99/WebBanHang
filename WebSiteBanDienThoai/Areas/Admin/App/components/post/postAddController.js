(function (app) {
    app.controller('postAddController', postAddController);
    function postAddController($scope, $http, apiService, commonService, notificationService,$state) {
        var UrlGetDataPost = "../Dashboard/getAllPost";
        var UrlAddPost = "localhost:6688/api/";

        //Khởi tạo post
        $scope.p = {};

        $scope.CkediterInit = function () {
            //CKEDITOR.replace("article-description");
            CKEDITOR.replace("Noi_dung");
        }
        // Chọn ảnh
        $scope.ChooseImage1 = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.p.ArticleImage = fileUrl;
                })
            }
            finder.popup();
            
        }

        $scope.CkediterInit();

        //Add post
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.p.Alias = commonService.getSeoTitle($scope.p.Title);
            $scope.p.MetaTitle = commonService.getSeoTitle($scope.p.Title);
        }

        $scope.reset = function () {
            $scope.p = null;
            $("#img1").attr("src", "#");
            CKEDITOR.instances['Noi_dung'].setData("");
        }
        $scope.AddPost = function () {
            $scope.p.ModifiedBy = "Long J";
            $scope.p.CreatedBy = "Long J";
            $scope.p.Detail = CKEDITOR.instances['Noi_dung'].getData();
            //$scope.p.Description = CKEDITOR.instances['article-description'].getData();
            apiService.post( 'articles/', $scope.p, function (result) {
                notificationService.displaySuccess($scope.p.Title + ' đã được thêm mới.');
                $state.go("post")
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
            
        }
    }
})(angular.module('AdmShopMobile.products'));
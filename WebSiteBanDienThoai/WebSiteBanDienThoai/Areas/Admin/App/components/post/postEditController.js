(function (app) {
    app.controller('postEditController', postEditController);

    function postEditController($scope, $stateParams, $http, commonService, apiService, notificationService, $state) {

            // Init Ckediter
            function CkeditorInit() {
                CKEDITOR.replace("article-detail");
                //CKEDITOR.replace("article-description");
            }
            $scope.p;
            // Init Data get
            $scope.InitData = function () {
                
                var getdata = apiService.get("articles/" + $stateParams.ID, null, function (result) {
                    $scope.p = result.data;
                    CkeditorInit();
                    CKEDITOR.instances["article-detail"].setData($scope.p.Detail);
                    //CKEDITOR.instances["article-description"].setData($scope.p.Description);
                }, function (error) {
                    notificationService.displayError(error.data);
                });
                
            }
            // call InitData run
            $scope.InitData();
            $scope.GetSeoTitle = GetSeoTitle;

            function GetSeoTitle() {
                $scope.p.Alias = commonService.getSeoTitle($scope.p.MetaTitle);
            }
            // function EditSP
            $scope.UpdatePost = function () {
                $scope.p.Detail = CKEDITOR.instances['article-detail'].getData();
                //$scope.p.Description = CKEDITOR.instances['article-description'].getData();
                $scope.p.ModifiedBy = "Long J";
                apiService.put("articles/" + $scope.p.ArticleID, $scope.p, function (result) {
                    notificationService.displaySuccess('Sửa thành công: ' + $scope.p.Title);
                    $state.go("post");
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
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
        };
})(angular.module('AdmShopMobile.products'));
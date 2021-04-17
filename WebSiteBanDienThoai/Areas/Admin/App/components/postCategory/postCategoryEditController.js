(function (app) {
    app.controller('editCategoryAddController',
    function ($scope, $http,$stateParams, commonService, apiService, notificationService, $state) {
        $scope.pc = {};
        $scope.UpdatePost = UpdatePost;
        function getData() {
            $scope.pc.CreatedBy = "Admin";
            $scope.pc.ModifiedBy = "Long JKer";
            apiService.get("ArticleCategories/" + $stateParams.ArticleCategoryID, null, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được lấy.');
                $scope.pc=result.data;
            }, function (error) {
                notificationService.displayError('Lấy thất bại.');
            });
        }
        getData();
        function UpdatePost() {
            $scope.pc.CreatedBy = "Admin";
            $scope.pc.ModifiedBy = "Long JKer";
            apiService.put("ArticleCategories/" + $stateParams.ArticleCategoryID, $scope.pc, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được sửa.');
                $state.go('postCategory');
            }, function (error) {
                notificationService.displayError('Sửa không thành công.');
            });
        }
    })
})(angular.module('AdmShopMobile.products'));
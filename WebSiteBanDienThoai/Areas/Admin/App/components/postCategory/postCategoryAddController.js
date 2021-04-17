(function (app) {
    app.controller('postCategoryAddController', 
    function ($scope, $http, commonService, apiService, notificationService, $state) {
        $scope.pc = {};
        $scope.AddPost = AddPost;
        function AddPost()
        {
            $scope.pc.CreatedBy = "Admin";
            $scope.pc.ModifiedBy = "Long JKer";
            apiService.post("ArticleCategories", $scope.pc, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('postCategory');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }
    })
})(angular.module('AdmShopMobile.products'));
(function (app) {
    var URl = '../Dashboard/GetDataThongKe';
    app.controller('ThongKeListController', ThongKeListController);
    function ThongKeListController($scope, $http) {
        
        $scope.GetDataThongKe = function () {
            var getdata = $http.get(URl);
            getdata.then(function (Statistics) {
                $scope.Lst = Statistics.data;
                $.niftyNoty({
                    type:  'success',
                    container : 'floating',
                    html: '<strong>Well done!</strong>' + 'Lấy được ' + Statistics.data.length + ' bản ghi ',
                    timer: 5000
                });
               // toastr.success('Lấy được ' + SanPham.data.length + ' bản ghi');
            }, function () {
                $.niftyNoty({
                    type: 'danger',
                    message: 'Lỗi',
                    container: 'floating',
                    timer: 3000
                });
            });
        }
        $scope.GetDataThongKe();
        $scope.pageSize = 5;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.Lst.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
        //xóa
       
    };
})(angular.module('AdmShopMobile.products'));
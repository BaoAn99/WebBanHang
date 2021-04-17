(function (app) {
    app.controller('BieuDo', BieuDo);
    var URl = '../Dashboard/GetDataThongKe';
    function BieuDo($http, $scope) {
        $scope.GetDataThongKe = function () {
            var getdata = $http.get(URl);
            getdata.then(function (Statistics) {
                $scope.Lst = Statistics.data;
                $.niftyNoty({
                    type: 'success',
                    container: 'floating',
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
        function InnitData() {
            var data = [];
            for (var i ; i < $scope.Lst; i++) {
                data[i] = {
                    period: $scope[i].Lst.Thang,
                    dl: $scope[i].Lst.SoLuong
                }
            }
            return data;
        }
        var datatable = InnitData();
        Morris.Area({
            element: 'statisticProduct',
            data: datatable,
            gridEnabled: false,
            gridLineColor: 'transparent',
            behaveLikeLine: true,
            xkey: 'period',
            ykeys: ['dl', 'up'],
            labels: ['Visitor', 'Pageview'],
            lineColors: ['#045d97'],
            pointSize: 0,
            pointStrokeColors: ['#045d97'],
            lineWidth: 0,
            resize: true,
            hideHover: 'auto',
            fillOpacity: 0.7,
            parseTime: false
        });

    }
})(angular.module('AdmShopMobile'));
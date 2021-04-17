(function (app) {
    app.controller('homeController', homeController);

    function homeController($scope, commonService, apiService, notificationService) {
        $scope.getData = getData;
        $scope.analytic = analytic;
        $scope.renderInventory = renderInventory;
        $scope.renderRevenue = renderRevenue;

        $scope.obj = [];
        function getData(year) {
            var dt = commonService.Start();
            apiService.get('GetAnalyticByYear?year=' + year, null, function (result) {
                if (result.data.status) {
                    $scope.obj = result.data.obj;
                    renderRevenue();
                    renderInventory();
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });

            dt.finish();

        }
        getData(2019);
        function renderRevenue() {
            if ($scope.obj.length > 0) {
                Morris.Area({
                    element: 'statisticProduct',
                    data: [{
                        period: 'Tháng 1',
                        dl: $scope.obj[0].TotalRevenue
                    }, {
                        period: 'Tháng 2',
                        dl: $scope.obj[1].TotalRevenue
                    }, {
                        period: 'Tháng 3',
                        dl: $scope.obj[2].TotalRevenue
                    }, {
                        period: 'Tháng 4',
                        dl: $scope.obj[3].TotalRevenue
                    }, {
                        period: 'Tháng 5',
                        dl: $scope.obj[4].TotalRevenue
                    }, {
                        period: 'Tháng 6',
                        dl: $scope.obj[5].TotalRevenue
                    }, {
                        period: 'Tháng 7',
                        dl: $scope.obj[6].TotalRevenue
                    }, {
                        period: 'Tháng 8',
                        dl: $scope.obj[7].TotalRevenue
                    }, {
                        period: 'Tháng 9',
                        dl: $scope.obj[8].TotalRevenue
                    }, {
                        period: 'Tháng 10',
                        dl: $scope.obj[9].TotalRevenue
                    }, {
                        period: 'Tháng 11',
                        dl: $scope.obj[10].TotalRevenue
                    }, {
                        period: 'Tháng 12',
                        dl: $scope.obj[11].TotalRevenue
                    }
                    ],
                    gridEnabled: false,
                    gridLineColor: 'transparent',
                    behaveLikeLine: true,
                    xkey: 'period',
                    ykeys: ['dl'],
                    labels: ['Doanh Thu'],
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
        }

        function renderInventory() {
            if ($scope.obj.length > 0) {
                Morris.Area({
                    element: 'userAnalytic',
                    data: [{
                        period: 'Tháng 1',
                        dl: $scope.obj[0].TotalSold,
                        up: $scope.obj[0].TotalInventory
                    }, {
                        period: 'Tháng 2',
                        dl: $scope.obj[1].TotalSold,
                        up: $scope.obj[1].TotalInventory
                    }, {
                        period: 'Tháng 3',
                        dl: $scope.obj[2].TotalSold,
                        up: $scope.obj[2].TotalInventory
                    }, {
                        period: 'Tháng 4',
                        dl: $scope.obj[3].TotalSold,
                        up: $scope.obj[3].TotalInventory
                    }, {
                        period: 'Tháng 5',
                        dl: $scope.obj[4].TotalSold,
                        up: $scope.obj[4].TotalInventory
                    }, {
                        period: 'Tháng 6',
                        dl: $scope.obj[5].TotalSold,
                        up: $scope.obj[5].TotalInventory
                    }, {
                        period: 'Tháng 7',
                        dl: $scope.obj[6].TotalSold,
                        up: $scope.obj[6].TotalInventory
                    }, {
                        period: 'Tháng 8',
                        dl: $scope.obj[7].TotalSold,
                        up: $scope.obj[7].TotalInventory
                    }, {
                        period: 'Tháng 9',
                        dl: $scope.obj[8].TotalSold,
                        up: $scope.obj[8].TotalInventory
                    }, {
                        period: 'Tháng 10',
                        dl: $scope.obj[9].TotalSold,
                        up: $scope.obj[9].TotalInventory
                    }, {
                        period: 'Tháng 11',
                        dl: $scope.obj[10].TotalSold,
                        up: $scope.obj[10].TotalInventory
                    }, {
                        period: 'Tháng 12',
                        dl: $scope.obj[11].TotalSold,
                        up: $scope.obj[11].TotalInventory
                    }],
                    gridEnabled: false,
                    gridLineColor: 'transparent',
                    behaveLikeLine: true,
                    xkey: 'period',
                    ykeys: ['dl', 'up'],
                    labels: ['Đã bán', 'Tồn kho'],
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
        }
        function analytic() { 
            $("#userAnalytic").empty();
            $("#statisticProduct").empty();
            var year = $("#year").val(); 
            getData(year);
        }

    }
})(angular.module('AdmShopMobile'));
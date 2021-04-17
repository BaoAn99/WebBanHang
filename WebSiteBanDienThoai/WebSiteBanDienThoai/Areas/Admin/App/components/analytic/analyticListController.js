(function (app) {
    app.controller('analyticListController', function ($scope, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.GetData = GetData; 
        $scope.analytic = analytic;  
        $scope.exportData = exportData; 
        

        $scope.lstData = []; $scope.check = true;
        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.lstData.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });
      
        function GetData(start,end) {
            var dt = commonService.Start();
            apiService.get('GetAnalyticByDate?StartTime=' + start + '&EndTime=' + end, null, function (result) {
                if (result.data.status) {
                    $.each(result.data.obj.GetAnalyticSaleByDates, function (index, elm) {
                        if (elm.Sold != 0) {
                            $scope.lstData.push(elm);
                        }
                    });
                    //$scope.lstData = result.data.obj.GetAnalyticSaleByDates;
                    $scope.obj = result.data.obj;
                    if ($scope.check) {
                        $scope.StartTime = $scope.obj.StartTime;
                        $scope.EndTime = $scope.obj.EndTime;
                        $scope.check = false;
                    }
                }
            }, function (error) {
                notificationService.displayError(error.data);
                });
             
            dt.finish();

            $('#demo-dp-range .input-daterange').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: "linked",
                autoclose: true,
                todayHighlight: true
            });
        }
       
        GetData();

        $('#datepicker').on('changeDate', function () {
            $scope.StartTime = $("input[name=start]").val();
            console.log($("input[name=start]").val());
            $scope.EndTime = $("input[name=end]").val();

        });

        function analytic() {
            var start = $("input[name=start]").val();
            var end = $("input[name=end]").val();
            GetData(start, end);
        }
        function exportData() {
            var start = $("input[name=start]").val();
            var end = $("input[name=end]").val();

            window.location.href = "/Dashboard/Export?StartTime=" + start + "&EndTime=" + end + "&Exits=false";
            $("#exportData").prop("disabled", true);
        }
        
    });
})(angular.module('AdmShopMobile.products'));
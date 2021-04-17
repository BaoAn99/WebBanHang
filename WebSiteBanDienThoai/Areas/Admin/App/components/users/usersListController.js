(function (app) {
    app.controller('userListController', function ($scope, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Tài khoản",
            function: "Danh sách",
            pageSize: 5,            
            accList:[]
        };
        $scope.Search = "";
        $scope.title = "Tài khoản";
        $scope.methods = {
            _delete: function (id, name) {
                bootbox.confirm("Bạn chắc chắn muốn xóa : <b>" + name + "</b> ?", function (result) {
                    if (result) {
                        var dt = commonService.Start();
                        apiService.post('deleteAccount?userName=' + id, null, function (result) {
                            notificationService.displaySuccess("Xóa thành công");
                            Init();
                        }, function (error) {
                            notificationService.displayError(error.data);
                        });
                        dt.finish();
                    }
                });
            }
        };
        $scope.events = {
            _keypress: function () {
                    if ($("#input-search").val() != "") {
                        $scope.properties.pageSize = $scope.properties.accList.length;
                    } else {
                        $scope.properties.pageSize = 5;
                    }                
            }
        };
        
        

        function Init() {
            var dt = commonService.Start();
            apiService.get('getAccounts', null, function (result) {
                $scope.properties.accList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });                        
            dt.finish();
        }
        //xóa        
        Init();        
    });
})(angular.module('AdmShopMobile.products'));
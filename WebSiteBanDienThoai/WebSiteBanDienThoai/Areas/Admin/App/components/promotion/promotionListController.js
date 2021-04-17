(function (app) {
    app.controller('promotionListController', function ($scope, $state, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Danh sách",
            function: "Khuyến mãi",
            pageSize: 5,
			promList:[],
            prodList: [],
            prodCategoryList: [],
        };
        $scope.methods = {
            _delete: function (id, name) {
                bootbox.confirm("Bạn chắc chắn muốn xóa mã giảm giá: <b>" + name + "</b> ?", function (result) {
                    if (result) {
                        var dt = commonService.Start();
                        apiService.post('DelPromotion?id=' + id, null, function (result) {
                            if (result.data.status) {
                                notificationService.displaySuccess("Xóa thành công");
                                $state.reload();
                            }
                            else
                                notificationService.displayError("Xoá không thành công!");
                        }, function (error) {
                            notificationService.displayError("Xoá không thành công! Mã khuyến mãi đã được sử dụng!");
                        });
                        dt.finish();
                    }
                });
			},
			_getNameTypeProduct:function(typeProductID){
				return $scope.properties.prodCategoryList.find( prodC => prodC.TypeProductID === typeProductID ).TypeProductName;
			},
			_getNameProduct:function(productID){
				return $scope.properties.prodList.find( prodC => prodC.ProductID === productID ).Name;
			}
		}
        $scope.events = {
            _keypress: function () {
                $("#input-search").keypress(function () {
                    $scope.pageSize = $scope.lstData.length;
                });
            },
            _focusout: function () {
                $("#input-search").focusout(function () {
                    if ($("#input-search").val() == "") {
                        $scope.pageSize = 5;
                        return;
                    }
                });
            }
        };
        function Init() {
            var dt = commonService.Start();
            apiService.get('GetPromotions', null, function (result) {
                $scope.properties.promList =  result.data.obj;                
            }, function (error) {
                notificationService.displayError(error.mess);
            });
			apiService.get('getProducts', null, function (result) {
                $scope.properties.prodList =  result.data.obj;                
            }, function (error) {
                notificationService.displayError(error.mess);
            });
			apiService.get('getTypeProducts', null, function (result) {
                $scope.properties.prodCategoryList =  result.data.obj;     						
            }, function (error) {
                notificationService.displayError(error.mess);
            });
			
			$scope.events._keypress();
			$scope.events._focusout();
            dt.finish();
        }
        //Khởi tạo        
        Init();
    });
})(angular.module('AdmShopMobile.products'));
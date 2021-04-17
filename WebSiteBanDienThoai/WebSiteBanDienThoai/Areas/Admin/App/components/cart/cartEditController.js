(function (app) {
    app.controller('promotionEditController', usersEditController);
    function usersEditController($scope, $stateParams, $http, commonService, apiService, notificationService, $state) {
        $scope.properties = {
            title: "Khuyến mãi",
            function: "Chỉnh sửa",
            pageSize: 5,
            prom:{},
            prodList: [],
            prodCategoryList: [],
			typePromotionList:[{Name:"Khuyến mãi theo loại",ID:"1"},{Name:"Khuyến mãi theo sản phẩm",ID:"2"}],
            index: 0
        };

        $scope.methods = {
            _update:function(){
				$scope.properties.prom.StartTime = $('#start-time').val();
				$scope.properties.prom.EndTime = $('#end-time').val();
				if($scope.properties.prom.TypePromotion==1&&$scope.properties.prom.TypeProductID==null){
					notificationService.displayError('Không thể thêm! Bạn chưa chọn loại sản phẩm.');
					return;
				}else{
					$scope.properties.prom.ProductID==null
				}
				if($scope.properties.prom.TypePromotion==2&&$scope.properties.prom.ProductID==null){
					notificationService.displayError('Không thể thêm! Bạn chưa chọn sản phẩm.');
					return;
				}else if($scope.properties.prom.TypePromotion==2){
					$scope.properties.prom.TypeProductID==null
				}
				apiService.post( 'EditPromotion/', $scope.properties.prom, function (result) {
					notificationService.displaySuccess($scope.properties.title+$scope.properties.prom.PromotionName + ' đã được sửa.');
					$state.go("promotions")
				}, function (error) {
					notificationService.displayError('Chỉnh sửa không thành công.');
				});	
			},
			_selectProduct:function(productId,productName){
				$scope.properties.prom.ProductID = productId;
				$scope.properties.prom.ProductName = productName;
			}
        };
        $scope.events = {
            _keypress: function () {
                $("#input-search").keypress(function () {
                    $scope.pageSize = $scope.lstData.length;
                });
            },
            _focusout: function () {
                $("#input-search").focusout(function () {
                    if ($("#input-search").val() === "") {
                        $scope.pageSize = 5;
                        return;
                    }
                });
            },
            _showSelectAccout: function (key) {
                switch (key) {
                    case 1: 
                        $('#select-employee').modal('show');
                        break;
                    case 2: 
                      
                        $('#select-customer').modal('show');
                        break; 
                }
            },
			_setDateTime:function(){
				$('#start-time').datepicker({ autoclose: true });
				$('#end-time').datepicker({ autoclose: true });
			}
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('GetPromotion?Id='+$stateParams.PromotionID , null, function (result) {
                $scope.properties.prom =  result.data.obj;                
            }, function (error) {
                notificationService.displayError(error.mess);
            });
			apiService.get('getProducts', null, function (result) {
                $scope.properties.prodList =  result.data.obj;
				$scope.properties.prom.ProductName = $scope.properties.prodList.find(x=>x.ProductID===$scope.properties.prom.ProductID).Name;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
			apiService.get('getTypeProducts', null, function (result) {
                $scope.properties.prodCategoryList =  result.data.obj;                
            }, function (error) {
                notificationService.displayError(error.mess);
            });
			$scope.events._setDateTime();
            dt.finish();
        }
        //Khởi tạo        
        Init();
    }
})(angular.module('AdmShopMobile.products'));
(function (app) {
    app.controller('receiptEditController', function ($scope,$state, $stateParams,$http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Chỉnh sửa",
            function: "Hóa đơn nhập",
            pageSize: 5,
            productList: [],
            supplierList:[],
            employeeList:[],
            obj:{
                OrderInvoiceDetails:[]
            },            
            recycle:{                
            },
            productListRecycle:[]
        };    
        $scope.methods = {
            _sleep:function (ms) {
                return new Promise(resolve => setTimeout(resolve, ms));
            },
            _empty: function () {
                if ($scope.properties.recycle.ProductID != undefined) {
                    var sp = $scope.properties.productListRecycle.find(x => x.ProductID === $scope.properties.recycle.ProductID);                    
                    productListTemp = $scope.properties.productListRecycle.filter(function (prod, index, arr) {
                        if (prod.ProductID != sp.ProductID)
                            return prod;
                    });
                    $scope.properties.productList.push(sp);
                    $scope.properties.recycle = null;
                }
            },
            _update: function () {
                var dt = commonService.Start();
                $scope.properties.obj.Employee = null;
                apiService.post('PutOrderJker', $scope.properties.obj,
                    function (result) {
                        if (result.data.status) {
                            notificationService.displaySuccess(result.data.mess);
                            $state.go('receipts');
                        }                        
                    else
                            notificationService.displayError(result.data.mess);
                    
                }, function (error) {
                    notificationService.displayError(result.data.mess);
                });
                dt.finish();
            },
            _selectProduct:async function(sp){
                    //await $scope.methods._sleep(500);
                    $scope.properties.recycle=sp;
                    $scope.properties.productListRecycle.push(sp);
                    productListTemp=$scope.properties.productList.filter(function(prod,index,arr){
                        if(prod.ProductID!=sp.ProductID)
                            return prod; 
                    }); 
                $scope.properties.productList = productListTemp;
                $('#select-product').modal('hide');
            },
            _firstCheck:function(){
                for(var key in $scope.properties.obj.OrderInvoiceDetails){
                    var obj = $scope.properties.obj.OrderInvoiceDetails[key]
                    $scope.properties.recycle=$scope.properties.productList.find(x=>x.ProductID===obj.ProductID);
                    obj.Name = $scope.properties.recycle.Name;
                    $scope.properties.productListRecycle.push($scope.properties.recycle);
                    productListTemp=$scope.properties.productList.filter(function(prod,index,arr){
                        if(prod.ProductID!=$scope.properties.recycle.ProductID)
                            return prod; 
                    }); 
                    $scope.properties.productList = productListTemp;
                }
            },
            _checkAmount:function(){
                if($scope.properties.recycle.AmountOk<1){
                    $scope.properties.recycle.AmountOk=1;
                }
            },
            _pushOrderDetail:function(){
                $scope.properties.obj.OrderInvoiceDetails.push({ 'ProductID': $scope.properties.recycle.ProductID, 'Name': $scope.properties.recycle.Name, 'Amount': $scope.properties.recycle.AmountOk, 'PriceProduct': $scope.properties.recycle.PriceProductOk })
                $scope.properties.recycle = null;
            },
            _popOrderDetail:function(Index){
                var orderDetail =  $scope.properties.obj.OrderInvoiceDetails[Index];
                var sp = $scope.properties.productListRecycle.find(x=>x.ProductID===orderDetail.ProductID);
                $scope.properties.obj.OrderInvoiceDetails.splice(Index, 1);
                productListTemp=$scope.properties.productListRecycle.filter(function(prod,index,arr){
                    if(prod.ProductID!=orderDetail.ProductID)
                        return prod; 
                });
                $scope.properties.productList.push(sp);
            }
        };
        $scope.events = {
            _keypress: function () {
                $("#input-search").keypress(function () {
                    $scope.properties.pageSize = $scope.lstData.length;
                });
            },
            _focusout: function () {
                $("#input-search").focusout(function () {
                    if ($("#input-search").val() == "") {
                        $scope.properties.pageSize = 5;
                        return;
                    }
                });
            },
            _setDateTime:function(){
                $('#order-date').datepicker({ autoclose: true });            
            }
        };



        async function Init() {
            var dt = commonService.Start();
            await apiService.get('getProducts', null, function (result) {
                $scope.properties.productList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            apiService.get('GetSuppliers', null, function (result) {
                $scope.properties.supplierList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            
            await apiService.get('GetOrderJker?orderID='+$stateParams.ID, null, function (result) {
                $scope.properties.obj = result.data.obj;
                apiService.get('GetCurrentUserJker', null, function (result) {
                    if (result.data.status) {
                        $scope.properties.obj.EmployeeID = result.data.obj.EmployeeId;
                        $scope.properties.FullName = result.data.obj.FullName;
                    }
                }, function (error) {
                    notificationService.displayError(error.mess);
                });
                $scope.methods._firstCheck();
            }, function (error) {
                notificationService.displayError(error.mess);
            });                
            $scope.events._keypress();
            $scope.events._focusout();
            $scope.events._setDateTime();            
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
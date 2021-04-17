(function (app) {
    app.controller('receiptAddController', function ($scope,$state, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Thêm mới",
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
            _insert: function (id, name) {
                var dt = commonService.Start();
                var date = new Date($scope.properties.obj.OrderDate);
                $scope.properties.obj.OrderDate = date.getDate() + "/" + (date.getMonth()+1) + "/" + date.getFullYear();
                apiService.post('PostOrderJker', $scope.properties.obj,
                function (result) {
                    notificationService.displaySuccess(result.data.mess);
                    $state.go('receipts');
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
                        if (prod.ProductID != sp.ProductID) { 
                            return prod; 
                        }
                    }); 
                $scope.properties.productList = productListTemp;
                $('#select-product').modal('hide');

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
            },
            _setDateTime:function(){
                $('#order-date').datepicker({ autoclose: true });            
            }
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('getProducts', null, function (result) {
                $scope.properties.productList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            apiService.get('GetSuppliers', null, function (result) {
                $scope.properties.supplierList = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            apiService.get('GetCurrentUserJker', null, function (result) {
                if (result.data.status) {
                    $scope.properties.obj.EmployeeID = result.data.obj.EmployeeId;
                    $scope.properties.FullName = result.data.obj.FullName;
                }
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            $scope.events._keypress();
            $scope.events._focusout();
            $scope.events._setDateTime();
            $scope.properties.obj.OrderDate = commonService.getDate();
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
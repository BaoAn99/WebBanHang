(function (app) {
    app.controller('guaranteeEditController', function ($scope,$state, $stateParams,$http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Thêm mới",
            function: "Bảo hành",
            pageSize: 5,
            billPageSize:5,
            productList: [],
            billList:[],
            employeeList:[],
            obj:{
                ReceiptDetails:[]
            },            
            recycle:{                
            },
            productListRecycle:[]
        };    
        $scope.methods = {
            _sleep:function (ms) {
                return new Promise(resolve => setTimeout(resolve, ms));
            },
            _edit: function (id, name) {
                var dt = commonService.Start();
                var date = new Date($("#order-date").val());
                $scope.properties.obj.Date = date.getDate() + "/" + (date.getMonth()+1) + "/" + date.getFullYear();
                apiService.post('EditReceipt', $scope.properties.obj,
                function (result) {
                    if(result.data.status){
                        notificationService.displaySuccess(result.data.mess);
                        $state.go('guarantees');
                    }
                    else{
                        notificationService.displayError(result.data.mess);    
                    }
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
            _selectBill:async function(bill){
                //await $scope.methods._sleep(500);
                $scope.properties.obj.BillID = bill.BillID;
                $('#select-bill').modal('hide');
                $scope.methods._getProductFollowBill();
            },
            _checkAmount:function(){
                if($scope.properties.recycle.AmountOk<1){
                    $scope.properties.recycle.AmountOk=1;
                }
            },
            _pushGuaranteeDetail:function(){
                $scope.properties.recycle.Describe = $("#describe").val();
                $scope.properties.obj.ReceiptDetails.push({ 'ProductID': $scope.properties.recycle.ProductID, 'Name': $scope.properties.recycle.Name, 'Amount': $scope.properties.recycle.AmountOk, 'Describe':$scope.properties.recycle.Describe,'ReceiptID':0})
                $scope.properties.recycle = null;
            },
            _popGuaranteeDetail:function(Index){
                var receiptDetail =  $scope.properties.obj.ReceiptDetails[Index];
                var sp = $scope.properties.productListRecycle.find(x=>x.ProductID===receiptDetail.ProductID);
                $scope.properties.obj.ReceiptDetails.splice(Index, 1);
                productListTemp=$scope.properties.productListRecycle.filter(function(prod,index,arr){
                    if(prod.ProductID!=receiptDetail.ProductID)
                        return prod; 
                });
                $scope.properties.productList.push(sp);
            },
            _getProductFollowBill: async function(){
                await apiService.get('GetProductFollowBills?id='+$scope.properties.obj.BillID, null, function (result) {
                $scope.properties.productList = result.data.obj;
                }, function (error) {
                    notificationService.displayError(error.mess);
                });
            }
        };
        $scope.events = {
            _keypress: function () {
                    if ($("#input-search").val() != "") {
                        $scope.properties.pageSize = $scope.properties.productList.length;
                    } else {
                        $scope.properties.pageSize = 5;
                    } 
                    if ($("#input-bill-search").val() != "") {
                        $scope.properties.billPageSize = $scope.properties.billList.length;
                    } else {
                        $scope.properties.billPageSize = 5;
                    }               
            },            
            _setDateTime:function(){
                $('#order-date').datepicker({ autoclose: true });            
            }
        };



        function Init() {
            var dt = commonService.Start();                    
            apiService.get('GetBillForReceipts', null, function (result) {
                $.each(result.data.obj,function(index,elm){                    
                    if(elm.Status==1)
                    {
                        $scope.properties.billList.push(elm);
                    }                    
                })
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
            apiService.get('GetReceipt?Id='+$stateParams.ID, null,async function (result) {
                $scope.properties.obj = result.data.obj;                
                await $scope.methods._getProductFollowBill();
                $.each($scope.properties.obj.ReceiptDetails,function(index,elm){

                    elm.Name = $scope.properties.productList.find(x=>x.ProductID===elm.ProductID).Name;
                    $scope.methods._selectProduct($scope.properties.productList.find(x=>x.ProductID===elm.ProductID));
                });
                $scope.properties.recycle = null;
            }, function (error) {
                notificationService.displayError(error.mess);
            });
            $scope.events._setDateTime();
            $scope.properties.obj.OrderDate = commonService.getDate();
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
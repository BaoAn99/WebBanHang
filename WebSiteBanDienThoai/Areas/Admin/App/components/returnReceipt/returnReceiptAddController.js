(function (app) {
    app.controller('returnGuaranteeAddController', function ($scope,$state, $http, commonService, apiService, notificationService) {
        $scope.properties = {
            title: "Thêm mới",
            function: "Trả bảo hành",
            pageSize: 5,
            billPageSize:5,
            productList: [],
            receiptList:[],
            employeeList:[],
            obj:{
                PayDetails:[]
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
                var date = new Date($("#order-date").val());
                $scope.properties.obj.Date = date.getDate() + "/" + (date.getMonth()+1) + "/" + date.getFullYear();
                apiService.post('AddPay', $scope.properties.obj,
                function (result) {
                    if(result.data.status){
                        notificationService.displaySuccess(result.data.mess);
                        $state.go('returnGuarantees');
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
            _selectReceipt:async function(bill){
                //await $scope.methods._sleep(500);
                $scope.properties.obj.ReceiptID = bill.ReceiptID;
                $('#select-receipt').modal('hide');
                $scope.methods._getDetailFollowReceipt();
            },
            _checkAmount:function(){
                if($scope.properties.recycle.AmountOk<1){
                    $scope.properties.recycle.AmountOk=1;
                }
            },
            _pushGuaranteeDetail:function(){
                $scope.properties.recycle.Describe = $("#describe").val();
                $scope.properties.obj.ReceiptDetails.push({ 'ProductID': $scope.properties.recycle.ProductID, 'Name': $scope.properties.recycle.Name, 'Amount': $scope.properties.recycle.AmountOk, 'Describe':$scope.properties.recycle.Describe})
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
            _getDetailFollowReceipt:function(){
                apiService.get('GetReceiptForView?Id='+$scope.properties.obj.ReceiptID, null, function (result) {
                $scope.properties.obj.PayDetails = result.data.obj.ReceiptDetails;
                $scope.properties.obj.TotalPrice = 0;
                }, function (error) {
                    notificationService.displayError(error.mess);
                });
            },
            _changePrice:function(){
                $scope.properties.obj.TotalPrice = $scope.properties.obj.PayDetails.reduce(function(sum,elm){
                    sum+= parseInt(elm.Price);
                    return sum;
                },0);
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
            apiService.get('GetReceipts', null, function (result) {
                $.each(result.data.obj,function(index,elm){
                    if(elm.Status==0)
                    {
                        $scope.properties.receiptList.push(elm);
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
            $scope.events._setDateTime();
            $scope.properties.obj.Date = commonService.getDate();
            dt.finish();
        }
        //Khởi tạo       
        Init();
    });
})(angular.module('AdmShopMobile.products'));
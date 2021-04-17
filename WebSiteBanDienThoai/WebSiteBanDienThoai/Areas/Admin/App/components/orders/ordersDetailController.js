(function (app) {
    app.controller('ordersDetailController', function ($scope, $stateParams, $state, commonService, apiService, notificationService) {
        $scope.pageSize = 5;
        $scope.getData = getData;
        $scope.cancel = cancel;
        $scope.initModalApprove = initModalApprove; 
        $scope.approveOrder = approveOrder;
        $scope.getPaymentName = getPaymentName;

        $("#input-search").keypress(function () {
            $scope.pageSize = $scope.lstData.length;
        });
        $("#input-search").focusout(function () {
            if ($("#input-search").val() == "") {
                $scope.pageSize = 5;
                return;
            }
        });

        $scope.approveOrderInput = {};

        var action = {
            _approveOrder: function (elm, url) {
                apiService.post(url, elm,
                    function (result) {
                        notificationService.displaySuccess('Đơn hàng được xử lý');
                        // disable Approve 
                        $state.go("bills");
                    },
                    function (error) {
                        notificationService.displayError('Có lỗi xảy ra.');
                    });
            },
            _getEmployeeSales: function (url) {
                apiService.get(url,
                    null,
                    function (result) {
                        if (result.data.status) {
                            $scope.prop.employeeSales = result.data.obj;
                        } else {
                            $scope.prop.employeeSales = [];
                        }
                    },
                    function (error) {
                        notificationService.displayError(error.data);
                    });
            },
            _getEmployeeDeliveries: function (url) {
                apiService.get(url,
                    null,
                    function (result) {
                        if (result.data.status) { 
                            $scope.prop.employeeDeliveries = result.data.obj;
                        } else {
                            $scope.prop.employeeDeliveries = [];
                        }
                    },
                    function (error) {
                        notificationService.displayError(error.data);
                    });
            },
            _checkApprove: function (url) {
                apiService.get(url,
                    null,
                    function (result) {
                        if (result.data.status) { 
                            $scope.prop.isApprove = result.data.obj;
                        } else {
                            $scope.prop.isApprove = false;
                        }
                    },
                    function (error) {
                        notificationService.displayError(error.data);
                    });
            },
            _getDataInit: function (url) {
                apiService.get(url,
                    null,
                    function (result) {
                        if (result.data.status) {
                            $scope.lstData = result.data.obj.OrderDetailView;
                            $scope.cart = result.data.obj.Cart;
                        } else {
                            $scope.lstData = [];
                        }
                    },
                    function (error) {
                        notificationService.displayError(error.data);
                    });

                apiService.get('GetPayments', null, function (result) {
                    if (result.data.status) {
                        $scope.payments = result.data.obj;
                    }
                }, function (error) {
                    notificationService.displayError(error.data);
                });

            },
            _cancel: function (url) {
                apiService.post(url,null, function (result) {
                    notificationService.displaySuccess('Đơn hàng được hủy bỏ');
                }, function (error) {
                        notificationService.displayError('Có lỗi xảy ra.');
                });
            }
        };

        function getPaymentName(id) {
            if (id !== null) {
                if ($scope.payments !== undefined) {
                    return $scope.payments.find(x => x.PaymentID === id).Name;
                } else {
                    return "";
                }
            } else {

                return "Chưa xét";
            }
        }
        function getData() {
            var dt = commonService.Start();
            action._getDataInit('GetOrder?orderId=' + $stateParams.BillID); 
            action._getEmployeeDeliveries("GetEmployeeType?type=1");
            action._getEmployeeSales("GetEmployeeType?type=0");
            action._checkApprove("CheckApproveOrder?orderId=" + $stateParams.BillID);
            dt.finish();
        }
        function cancel() {
            bootbox.confirm("Hủy đơn hàng: " + $stateParams.BillID + " ?", function (result) {
                if (result) {
                    action._cancel("CancelOrder?orderId=" + $stateParams.BillID);
                    $state.go("orders");
                }
            });

          
        }
        function approveOrder() {
            $('#select-employee').modal('hide');
            $scope.approveOrderInput.OrderId = $stateParams.BillID;
            action._approveOrder($scope.approveOrderInput, "ApproveOrder");
        }

        function initModalApprove() {
            $('#select-employee').modal('show');
        }
         
        getData();
        $scope.prop = {
            isApprove:null,
            employeeSales: [],
            employeeDeliveries: []
        };
    });
})(angular.module('AdmShopMobile.products'));
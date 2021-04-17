(function (app) {
    app.controller('usersRoleController', function ($scope, $http, commonService, apiService, notificationService, $state) {
        $scope.properties = {
            title: "Tài khoản",
            function: "Phân quyền",
            pageSize: 5,
            accList: [],
            roleList: [],
            employeeList: [],
            customerList: [],
            index: 0
        };
        $scope.methods = {
            _update: function (index) {
                var acc = $scope.properties.accList[index];

                ///Case Employee
                if (acc.RoleId === 2) {
                    $scope.properties.index = index;
                    $scope.events._showSelectAccout(1);
                } else if (acc.RoleId === 3) {
                    $scope.properties.index = index;
                    $scope.events._showSelectAccout(2,acc);
                } else {
                    apiService.post('updateRole?roleId=' + acc.RoleId + "&username=" + acc.Username + "&customerOrEmployeeId=0", null,
                        function (result) {
                            notificationService.displaySuccess(result.data.mess);
                            $state.go('users');
                        }, function (error) {
                            notificationService.displayError('Sửa không thành công.');
                        });
                }
              
            },
            _updateWithFK: function () {
                var acc = $scope.properties.accList[$scope.properties.index];
                apiService.post('updateRole?roleId=' + acc.RoleId + "&username=" + acc.Username + "&customerOrEmployeeId=" + $scope.acc.CustomerOrEmployeeId, null,
                    function (result) {
                        notificationService.displaySuccess(result.data.mess);
                        $state.go('users');
                    }, function (error) {
                        notificationService.displayError('Sửa không thành công.');
                    });
            },
            _getListEmployee: function () {
                apiService.get("GetEmployeeType?type=0", null, function (result) {
                    if (result.data.status) {
                        $scope.properties.employeeList = result.data.obj;
                    }

                }, function (error) {
                    notificationService.displayError(error.data);
                });
            },
            _getListCustomer: function () {
                apiService.get("getCustomers", null, function (result) {
                    if (result.data.status) {
                        $scope.properties.customerList = result.data.obj; 
                    } 
                }, function (error) {
                    notificationService.displayError(error.data);
                });
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
            }
        };



        function Init() {
            var dt = commonService.Start();
            apiService.get('getAccounts', null, function (result) {
                $scope.properties.accList = result.data.obj;
                $scope.properties.roleList = [
                 {
                    RoleId: 1,
                    Name: "Quản Trị"
                }, {
                    RoleId: 2,
                    Name: "Nhân viên"
                }, {
                    RoleId: 3,
                    Name: "Khách hàng"
                }];
            }, function (error) {
                notificationService.displayError(error.mess);
            });
          
            $scope.events._keypress();
            $scope.events._focusout(); 
            $scope.methods._getListCustomer();
            $scope.methods._getListEmployee();
            dt.finish();
        }
        //xóa        
        Init();
    });
})(angular.module('AdmShopMobile.products'));
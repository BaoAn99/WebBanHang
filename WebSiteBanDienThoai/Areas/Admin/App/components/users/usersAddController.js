(function (app) {
    app.controller('usersAddController', function ($scope, $http, commonService, apiService, notificationService, $state) {
        $scope.properties = {
            title: "Người dùng",
            function: "Thêm mới",
            pageSize: 5,
            acc: {
                EmailConfirmed: true
            },
            accCheck:{},
            roles: [],
            employees: {},
            customers: {}
        };
        $scope.initComponent = initComponent;
        $scope.checkUserName = checkUserName;
        $scope.checkMail = checkMail;
        //event
        $scope.initData = initData;
        $scope.Insert = Insert;
        $scope.CheckValid = CheckValid;

        function CheckValid() {
            $scope.validPass = angular.equals($scope.properties.acc.Password, $scope.properties.acc.rePassword);
            if ($scope.validPass) {
                $("#errpass").addClass("ng-hide");
                //$(':input[type="submit"]').prop('disabled', false);
            } else {
                $("#errpass").removeClass("ng-hide");
                //$(':input[type="submit"]').prop('disabled', true);
            }

        }
        function initComponent() {
            $scope.boolToStrGender = function (arg) { return arg ? 'Nam' : 'Nữ' };
            $scope.boolToStrPhoneConfirmed = function (arg) { return arg ? 'Đã xác thực' : 'Chưa xác thực' };
            $scope.boolToStrMailConfirmed = function (arg) { return arg ? 'Đã xác thực' : 'Chưa xác thực' };
            $scope.boolToStrStatus = function (arg) { return arg ? 'Kích hoạt' : 'Vô hiệu hóa' };
        }        
        function initData() {
            $scope.properties.accCheck.Username = true;
            $scope.properties.accCheck.Email = true;
            var dt = commonService.Start();
            $scope.properties.roles = [
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
            apiService.get('getEmployees?isAdd=true', null, function (result) {
                $scope.properties.employees = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
                });

            apiService.get('getCustomers?isAdd=true', null, function (result) {
                $scope.properties.customers = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();
            window.onload = function() {
                $scope.properties.acc.Username = "";
                $scope.properties.acc.Password = "";
            };
            //JQ 
            $("#select-employee").hide();
            $("#select-customer").hide(); 
            $("#select-role").change(function () {
                var selectRole = $("#select-role").val();
                if (selectRole === "2") {
                    $("#select-employee").show();
                    $("#select-customer").hide();
                    $("#select-customer").val("");
                } else if (selectRole === "3") {
                    $("#select-employee").hide();
                    $("#select-customer").show();
                    $("#select-employee").val("");
                }
            });
        }
        function Insert() {
            if (!$scope.properties.accCheck.Username) {
                notificationService.displayError('Không thể thêm! ' + $scope.properties.accCheck.UsernameMess);
                return;
            }
            if (!$scope.properties.accCheck.Email) {
                notificationService.displayError('Không thể thêm! ' + $scope.properties.accCheck.EmailMess);
                return;
            }
            var dt = commonService.Start();
            apiService.post('postAccount', $scope.properties.acc,
                function (result) {
                    notificationService.displaySuccess('Thêm thành công: ' + $scope.properties.acc.Name);
                    $state.go('users');
                }, function (error) {
                    notificationService.displayError('Thêm không thành công.');
                });
            dt.finish();
        }
        function checkUserName() {
            apiService.get('getAccount?Username=' + $scope.properties.acc.Username, null, function (result) {
                if (result.data.obj != null)
                {
                    $scope.properties.accCheck.Username = false;
                    $scope.properties.accCheck.UsernameMess = "Tên tài khoản đã tồn tại";
                }
                else
                {
                    $scope.properties.accCheck.Username = true;
                    $scope.properties.accCheck.UsernameMess = "";
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function checkMail() {
            apiService.get('checkMail?email=' + $scope.properties.acc.Email, null, function (result) {
                if (result.data.obj) {
                    $scope.properties.accCheck.Email = false;
                    $scope.properties.accCheck.EmailMess = "Email đã tồn tại";
                }
                else {
                    $scope.properties.accCheck.Email = true;
                    $scope.properties.accCheck.EmailMess = "";
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        //Run init
        initComponent();
        initData();
    });
})(angular.module('AdmShopMobile.products'));
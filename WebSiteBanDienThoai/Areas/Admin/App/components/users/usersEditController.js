(function (app) {
    app.controller('usersEditController', usersEditController);
    function usersEditController($scope, $stateParams, $http, commonService, apiService, notificationService, $state) {
        $scope.properties = {
            title: "Tài khoản",
            function: "Danh sách",
            pageSize: 5,
            acc: {
                EmailConfirmed: true
            },
            roles: {},
            employees: {},
            customers: {}
        };

        $scope.ChooseImage = ChooseImage;
        $scope.reset = reset;
        $scope.initComponent = initComponent;
        //event
        $scope.initData = initData;
        $scope.Update = Update;
        $scope.CheckValid = CheckValid;


        function CheckValid() {
            $scope.validPass = angular.equals($scope.properties.acc.Password, $scope.properties.acc.rePassword);
            if ($scope.validPass) {
                $("#errpass").addClass("ng-hide");
                $(':input[type="submit"]').prop('disabled', false);
            } else {
                $("#errpass").removeClass("ng-hide");
                $(':input[type="submit"]').prop('disabled', true);
            }

        }
        function initComponent() {
            $scope.boolToStrGender = function (arg) { return arg ? 'Nam' : 'Nữ' };
            $scope.boolToStrPhoneConfirmed = function (arg) { return arg ? 'Đã xác thực' : 'Chưa xác thực' };
            $scope.boolToStrMailConfirmed = function (arg) { return arg ? 'Đã xác thực' : 'Chưa xác thực' };
            $scope.boolToStrStatus = function (arg) { return arg ? 'Kích hoạt' : 'Vô hiệu hóa' };
        }

        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.u.LinkAvt = fileUrl;
                });
            };
            finder.popup();
        }

        function reset() {
            $("#img1").empty();
        }

        function initData() {
            var dt = commonService.Start();
            apiService.get('getAccount?Username=' + $stateParams.ID, null, function (result) {
                $scope.properties.roles = [
                         {
                        RoleId: Number(1),
                        Name: "Quản Trị"
                    }, {
                        RoleId: Number(2),
                        Name: "Nhân viên"
                    }, {
                        RoleId: Number(3) ,
                        Name: "Khách hàng"
                    }];
                $scope.properties.acc = result.data.obj;
                $scope.properties.acc.DateOfBirth = new Date($scope.properties.acc.DateOfBirth);
                $scope.properties.acc.rePassword = $scope.properties.acc.Password;
                if ($scope.properties.acc.EmployeeId != null)
                    $scope.properties.acc.EmployeeStatus = true;
                else
                    $scope.properties.acc.EmployeeStatus = false;
                console.log($scope.properties.acc);
                if ($scope.properties.acc.RoleId === 2) {
                    console.log($scope.properties.acc.RoleId);
                    $("#select-employee").show();
                    $("#select-customer").hide();
                } else if ($scope.properties.acc.RoleId === 3) {
                    $("#select-employee").hide();
                    $("#select-customer").show();
                } 

            }, function (error) {
                notificationService.displayError(error.data);
            });
            apiService.get('getEmployees', null, function (result) {
                $scope.properties.employees = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });

            apiService.get('getCustomers', null, function (result) {
                $scope.properties.customers = result.data.obj;
            }, function (error) {
                notificationService.displayError(error.data);
            });
            dt.finish();

            //JQ 
          
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
        function Update() {
            var dt = commonService.Start();
            apiService.post('putAccount', $scope.properties.acc,
                function (result) {
                    notificationService.displaySuccess('Sửa thành công: ' + $scope.properties.acc.Name);
                    $state.go('users');
                }, function (error) {
                    notificationService.displayError('Sửa không thành công.');
                });
            dt.finish();
        }
        //Run init
        initComponent();
        initData();
    }
})(angular.module('AdmShopMobile.products'));
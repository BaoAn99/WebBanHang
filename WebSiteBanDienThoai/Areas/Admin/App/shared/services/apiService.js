/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService', 'authenticationService', 'commonService'];
    function apiService($http, notificationService, authenticationService, commonService) {
        var Uri = "http://localhost:1235/Admin/Dashboard/";
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }
        function del(url, data, success, failure) {
            url = Uri + url;
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
               
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                    console.log(error);
                }
            });
        }
        function post(url, data, success, failure) {
            url = Uri + url;
            authenticationService.setHeader();
            $http.post(url, data).then(function (result) {
               
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                    console.log(error);
                }
            });
        }
        function put(url, data, success, failure) {
            url = Uri + url;
            authenticationService.setHeader();
            $http.put(url, data).then(function (result) {
               
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
        async function get(url, params, success, failure) {
            
            url = Uri + url;
            authenticationService.setHeader();
            await $http.get(url, params).then(function (result) {
                success(result);
                
            }, function (error) {
                failure(error);
               
            });
          
        }
    }
})(angular.module('AdmShopMobile.common'));
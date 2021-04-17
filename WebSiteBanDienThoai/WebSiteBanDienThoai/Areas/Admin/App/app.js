/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('AdmShopMobile', ['AdmShopMobile.products', 'AdmShopMobile.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProviders) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/Areas/Admin/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProviders.otherwise('/admin');
      
    }
})();
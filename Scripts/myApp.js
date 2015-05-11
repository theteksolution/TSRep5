var app = angular.module("myApp", ['ngRoute', 'Models', 'Controllers', 'Directives', 'angular-confirm']);

// configure our routes
app.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: 'Purchase.html',
            controller: 'purchaseCtrl'
        })
        .when('/purchase', {
             templateUrl: 'Purchase.html',
             controller: 'purchaseCtrl'
         })
        // route for the about page
        .when('/customer', {
            templateUrl: 'CustomerDetail.html',
            controller: 'customerDetailCtrl'
        })
        // route for the contact page
        .when('/balances', {
            templateUrl: 'Balances.html',
            controller: 'balancesCtrl'
        })
        .when('/transactions', {
            templateUrl: 'Transactions.html',
            controller: 'transactionsCtrl'
        })
        .when('/locations', {
            templateUrl: 'Locations.html',
            controller: 'locationsCtrl'
        });
});



app.controller("transactionsCtrl", function($scope,$http) {
       
        $http.get("api/Transaction")
        .success(function (response) {
            $scope.Transactions = response.TransactionList;
            
        })
        .error(function (data, status, headers, config) {
            alert('here');
        });

});

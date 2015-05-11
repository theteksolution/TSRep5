app.controller("balancesCtrl", function($scope,$http) {
       
        $http.get("api/Balance")
        .success(function (response) {
            $scope.Balances = response.BalanceList;
            
        })
        .error(function (data, status, headers, config) {
            alert('here');
        });


        //$scope.getLocationInfo = function () {

        //    //var data = $scope.formData.CustomerID
        //    $http.get("api/Location", { params: { id: $scope.formData.LocationID } })
        //    .success(function (response) {
                
        //        $scope.LocationName = response.LocationName;
        //        $scope.Street1 = response.Street1;
        //        $scope.Street2 = response.Street2;
        //        $scope.City = response.City;
        //        $scope.Zip = response.Zip;
        //        $scope.State = response.State;
               
        //    })
        //    .error(function (data, status, headers, config) {
        //        alert('herezzzzz');
        //    });
           
        //};

        //$scope.formData = {};
        //$scope.processForm = function () {
        //    var data = $scope.formData;
            
        //    $http.post('api/Purchase', data)            
        //    .success(function (data) {
        //        console.log(data);

        //        if (!data.success) {
        //            // if not successful, bind errors to error variables
        //            $scope.errorName = data.errors.name;
        //            $scope.errorSuperhero = data.errors.superheroAlias;
        //        } else {
        //            // if successful, bind success message to message
        //            $scope.message = data.message;
        //        }
        //    });
        //};

        //$scope.Customers = [
        //{ Text: '1', Value: '1' },
        //{ Text: '2', Value: '2' },
        //{ Text: '3', Value: '3' }
        //];
});

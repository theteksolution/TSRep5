app.controller("purchaseCtrl", function($scope,$http) {
       

        $http.get("api/Purchase")
        .success(function (response) {
            $scope.Customers = response.CustList;
        })
        .error(function (data, status, headers, config) {
            alert('here');
        });


        $scope.getCustomerInfo = function () {

            //var data = $scope.formData.CustomerID
            $http.get("api/Purchase", { params: { id: $scope.formData.CustomerID } })
            .success(function (response) {
                
                $scope.Name = response.Name;
                $scope.Address = response.Address;
                $scope.Balance = response.Balance;
            })
            .error(function (data, status, headers, config) {
                alert('herezzzzz');
            });
           
        };

        $scope.formData = {};
        $scope.processForm = function () {
            var data = $scope.formData;
            
            $http.post('api/Purchase', data)            
            .success(function (data) {
                console.log(data);

                if (!data.success) {
                    // if not successful, bind errors to error variables
                    $scope.errorName = data.errors.name;
                    $scope.errorSuperhero = data.errors.superheroAlias;
                } else {
                    // if successful, bind success message to message
                    $scope.message = data.message;
                }
            });
        };

        //$scope.Customers = [
        //{ Text: '1', Value: '1' },
        //{ Text: '2', Value: '2' },
        //{ Text: '3', Value: '3' }
        //];
});

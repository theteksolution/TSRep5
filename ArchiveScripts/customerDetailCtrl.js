app.controller("customerDetailCtrl", function($scope,$http) {
       

        $http.get("api/Customer")
        .success(function (response) {
            $scope.Customers = response.CustList;
           
        })
        .error(function (data, status, headers, config) {
            alert('here');
        });


        $scope.getCustomerDetailInfo = function () {

            //var data = $scope.formData.CustomerID
            $http.get("api/Customer", { params: { id: $scope.formData.CustomerID } })
            .success(function (response) {
                
                $scope.FirstName = response.FirstName;
                $scope.LastName = response.LastName;
                $scope.Street1 = response.Street1;
                $scope.Street2 = response.Street2;
                $scope.City = response.City;
                $scope.Zip = response.Zip;
                $scope.State = response.State;
                $scope.Phone = response.Phone;
                $scope.Email = response.Email;
               
            })
            .error(function (data, status, headers, config) {
                alert('herezzzzz');
            });
           
        };

        $scope.formData = {};
        $scope.processForm = function () {
            var data = $scope.formData;
            
            $http.post('api/Customer', data)            
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

});

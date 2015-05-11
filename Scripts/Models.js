angular.module('Models', [])
    //
    // Balances Model
    //
    .service("balancesService", function ($http) {

        this.all = function (sortOrder) {

            console.log('balaces');
            console.log(sortOrder);

            if (sortOrder === null || sortOrder === "") {
                sortOrder = "Date Descending";
            }
            console.log(sortOrder);
            return $http.get("api/ObtainBalances/" + sortOrder)
                .then(
                    function (result) {
                        return result.data;
                    }
                )
        };
    })
    //
    // Costomer Details Model
    //
    .service("customerDetailsService", function ($http) {

        this.all = function () {
            return $http.get("api/Customer")
                .then(
                    function (result) {
                        return result.data;
                    }
                )
        };

        this.customerDetailInfo = function (CustomerID) {
            return $http.get("api/Customer", { params: { id: CustomerID } })
               .then(
                    function (result) {
                        return result.data;
                    }
                )
        };

        this.updateCustomerDetail = function (data) {

            return $http.post("api/Customer", data)
               .then(
                    function (result) {
                        return result.data;
                    }
                )
        };

    })
    //
    // Purchases Model
    //
    .service("purchasesService", function ($http) {

        this.getCustomerPurchaseInfo = function (CustomerID) {
            
            return $http.get("api/Purchase", { params: { id: CustomerID } })
                .then(function (result) {
                    return result.data;
                    }
                )
        };

    })
    .service("locationsService", function ($http) {


        this.getLocationsList = function () {
            return $http.get("api/Location")
            .then(function (result) {
               return result.data;
               }
            )
        };

        this.getLocationInformation = function (LocationID) {

            //var data = $scope.formData.CustomerID
           return $http.get("api/Location", { params: { id: LocationID } })
                .then(function (response) {
                    return response.data;
                })
        };

        this.updateLocation = function (data) {

            return $http.post('api/Location', data)
                .then(function (response) {
                    return response.data;
                });
        };

    });

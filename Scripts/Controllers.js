angular.module('Controllers', ['ui.bootstrap'])

    //
    //
    // Main Controller
    //
    .controller("mainCtrl", function ($scope) {
    
    })


    //
    //
    // Balances Controller
    //
    .controller("balancesCtrl", function ($scope, balancesService) {

      
        $scope.GetBalances = function () {
            balancesService.all($scope.sortOrder)
               .then(function (result) {
                   $scope.Balances = result.BalanceList;
               });
        };

        $scope.GetBalances();
    })


    //
    //
    // Customer Details Controller
    //
    .controller("customerDetailCtrl", function ($scope, customerDetailsService) {

        var customerDetails = this;

        customerDetails.GetCustomerList = function (CustomerID) {
            customerDetailsService.all()
               .then(function (result) {
                   $scope.Customers = result.SelectItemsVMList;
                   $scope.formData.CustomerID = CustomerID;
               });
        };

        $scope.closeAlert = function () {
            $scope.isCollapsed = true;
        };

        customerDetails.GetCustomerList('0');
      

        $scope.getCustomerDetailInfo = function () {
                //var data = $scope.formData.CustomerID
            customerDetailsService.customerDetailInfo($scope.formData.CustomerID)
              .then(function (response) {
                  $scope.formData.FirstName = response.FirstName;
                  $scope.formData.LastName = response.LastName;
                  $scope.formData.Street1 = response.Street1;
                  $scope.formData.Street2 = response.Street2;
                  $scope.formData.City = response.City;
                  $scope.formData.Zip = response.Zip;
                  $scope.formData.State = response.State;
                  $scope.formData.Phone = response.Phone;
                  $scope.formData.Email = response.Email;
              });
        };


        $scope.formData = {};

        $scope.processForm = function () {
            var data = $scope.formData;
    
            customerDetailsService.updateCustomerDetail(data)
                .then(function (data) {
                    customerDetails.GetCustomerList(response);
                    $scope.isCollapsed = false;
                });
            };
    })


    //
    //
    // Locations Controller
    //
    .controller("locationsCtrl", function ($scope, locationsService) {

        var locations = this;

        locations.LoadLocationsList = function (LocationID) {
            locationsService.getLocationsList()
               .then(function (result) {
                   $scope.Locations = result.SelectItemsVMList;
                   $scope.formData.LocationID = LocationID;
                   //$scope.isCollapsed = true;
               });
        };


        locations.LoadLocationsList('0');

        $scope.closeAlert = function () {
            $scope.isCollapsed = true;
        };
       
        $scope.getLocationInfo = function () {
            locationsService.getLocationInformation($scope.formData.LocationID)
             .then(function (response) {
                 $scope.formData.LocationName = response.LocationName;
                 $scope.formData.Street1 = response.Street1;
                 $scope.formData.Street2 = response.Street2;
                 $scope.formData.City = response.City;
                 $scope.formData.Zip = response.Zip;
                 $scope.formData.State = response.State;
             });
        };

        $scope.formData = {};
        $scope.processForm = function () {
            var data = $scope.formData;

            locationsService.updateLocation(data)
            .then(function (response) {
                locations.LoadLocationsList(response);
                $scope.isCollapsed = false;
            });
        };
    })



    //
    //
    // Purchase Controller
    //
    .controller("purchaseCtrl", function ($scope, purchasesService, locationsService, $modal) {

      
        var purchaseDetails = this;

        purchaseDetails.GetCustomerList = function (CustomerID) {
            purchasesService.getCustomersList()
               .then(function (result) {
                   $scope.Customers = result.SelectItemsVMList;
                   $scope.formData.CustomerID = CustomerID;
               });
        };

        purchaseDetails.LoadLocationsList = function (LocationID) {
            locationsService.getLocationsList()
               .then(function (result) {
                   $scope.Locations = result.SelectItemsVMList;
                   $scope.formData.LocationID = LocationID;
               });
        };

        purchaseDetails.GetCustomerList('0');

        purchaseDetails.LoadLocationsList('0');

        $scope.closeAlert = function () {
            $scope.isCollapsed = true;
        };
       
        $scope.getCustomerDetailInfo = function () {
           
            purchasesService.getCustomerPurchaseInfo($scope.formData.CustomerID)
            .then(function (response) {

                $scope.FirstName = response.FirstName;
                $scope.LastName = response.LastName;
                $scope.Street1 = response.Street1;
                $scope.Street2 = response.Street2;
                $scope.City = response.City;
                $scope.State = response.State;
                $scope.Zip = response.Zip;
                $scope.Phone = response.Phone;
                $scope.Email = response.Email;
                $scope.Balance = response.Balance;
            });

        };

        $scope.formData = {};
        $scope.processForm = function (isValid) {

            var data = $scope.formData;

           purchasesService.updatePurchase(data)
          .then(function (response) {
              $scope.isCollapsed = false;
              $scope.formData.Amount = null;
              $scope.Balance = response;
              $scope.userForm.$setPristine();
          });
        };
    })


    //
    //
    // Transaction Controller
    //
    .controller("transactionsCtrl", function ($scope, transactionsService, locationsService) {

        var transactions = this;

        transactions.LoadLocationsList = function () {
            locationsService.getLocationsList()
               .then(function (result) {
                   $scope.Locations = result.SelectItemsVMList;
                   $scope.formData.LocationID = '0';
               });
        };

        $scope.formData = {};
        $scope.getTransactions = function () {

            transactionsService.getTransactions($scope.dtTo, $scope.dtFrom, $scope.formData.LocationID)
                .then(function (result) {
                    $scope.Transactions = result.CustomerTransactionList;
                });
        };

       
        $scope.getTransactions();
        transactions.LoadLocationsList();


        /*


        **********************************************************


        */


        $scope.today = function () {
            //$scope.dt = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.dt = null;
        };

        // Disable weekend selection
        $scope.disabled = function (date, mode) {
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        $scope.toggleMin = function () {
            $scope.minDate = $scope.minDate ? null : new Date();
        };

        $scope.toggleMin();

        $scope.openTo = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.openedTo = true;
        };

        $scope.openFrom = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.openedFrom = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
            showWeeks: false
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 2);
        $scope.events =
          [
            {
                date: tomorrow,
                status: 'full'
            },
            {
                date: afterTomorrow,
                status: 'partially'
            }
          ];

        $scope.getDayClass = function (date, mode) {
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        };

    });



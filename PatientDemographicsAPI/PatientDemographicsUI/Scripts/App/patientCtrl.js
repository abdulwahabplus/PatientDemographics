(function () {
    'use strict';

    var app = angular.module('app', []);//set and get the angular module  
    app.controller('patientController', ['$scope', '$http', patientController]);

    //angularjs controller method  
    function patientController($scope, $http) {
        

        $scope.patients = [];
        resetInputs();

        function resetInputs() {
            $scope.newpatient = {};
            $scope.newpatient.Gender = "Male";
            $scope.PhoneNumbers = [{ Type: 'Home', Number: '' }, { Type: 'Work', Number: '' }, { Type: 'Mobile', Number: '' }];
        }
                
        //declare variable for mainain ajax load and entry or edit mode  
        $scope.loading = true;
        $scope.addMode = false;

        //get all patient information  
        $http({
            method: 'GET',
            url: PATIENT_API_URL + '/api/patients/'
        }).then(function (response) {

            $scope.patients = response.data;
            $scope.loading = false;

        }, function (error) {

            $scope.error = "An Error has occurred while loading patients!";
            $scope.loading = false;

        });

        //by pressing toggleAdd button ng-click in html, this method will be hit  
        $scope.toggleAdd = function (form) {
            $scope.addMode = !$scope.addMode;
            resetInputs();
            form.$setPristine();
            form.$setUntouched();
        };

        //Insert patient  
        //get all patient information  
        $scope.add = function () {
            var patientData = $scope.newpatient;
            patientData.Phone = $scope.PhoneNumbers;
            $scope.loading = true;
            $scope.error = "";
            $http({
                method: 'POST',
                url: PATIENT_API_URL + '/api/patient/',
                data: patientData
            }).then(function (response) {

                alert("Added Successfully!!");
                $scope.addMode = false;
                if (response.data) {
                    if (!Array.isArray($scope.patients)) {
                        $scope.patients = [];
                    }
                    $scope.patients.push(response.data);
                }
                $scope.loading = false;

            }, function (error) {
                $scope.error = "An Error has occured while adding patient! " + error;
                $scope.loading = false;
            });
        }        
    }
})();
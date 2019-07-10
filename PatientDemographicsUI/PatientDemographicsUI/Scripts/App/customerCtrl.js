(function () {
    'use strict';

    var app = angular.module('app', []);//set and get the angular module  
    app.controller('patientController', ['$scope', '$http', patientController]);

    //angularjs controller method  
    function patientController($scope, $http) {

        $scope.patients = [];
        $scope.newpatient = {};
        $scope.PhoneNumbers = [{ Type: 'Home', Number: '' }, { Type: 'Work', Number: '' }, { Type: 'Mobile', Number: '' }];
        $scope.newpatient.Gender = "Male";
                
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

        //by pressing toggleEdit button ng-click in html, this method will be hit  
        $scope.toggleEdit = function () {
            this.patient.editMode = !this.patient.editMode;
        };

        //by pressing toggleAdd button ng-click in html, this method will be hit  
        $scope.toggleAdd = function () {
            $scope.addMode = !$scope.addMode;
        };

        //Insert patient  
        //get all patient information  
        $scope.add = function () {
            var patientData = $scope.newpatient;
            patientData.Phone = $scope.PhoneNumbers;
            $scope.loading = true;
            $http({
                method: 'POST',
                url: PATIENT_API_URL + '/api/patient/',
                data: patientData
            }).then(function (response) {

                alert("Added Successfully!!");
                $scope.addMode = false;
                if (response.data) {
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
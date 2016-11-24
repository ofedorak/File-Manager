var AnguarModule = angular.module('App', []);

AnguarModule.controller('DirectoryController', function ($scope, $http, apiCall) {

    apiCall.GetApiCall("ExploreApi", "Get").success(function (data) {
        $scope.FileDirecotoriesInfoApiModel = data;
    });


    $scope.GetDirectory = function (path) {
        $('#loader-content').show();

        apiCall.GetDirectory(path).success(function (data, status) {


            $scope.FileDirecotoriesInfoApiModel = data;
            $('#loader-content').hide();
        }).error(function (error) {
            alert(error.ExceptionMessage);
        });
    }

    $scope.GetDrivesDirectory = function (driveName) {
        $('#loader-content').show();
        apiCall.GetDrivesDirectory(driveName).success(function (data, status) {
            $scope.FileDirecotoriesInfoApiModel = data;
            $('#loader-content').hide();
        }).error(function (error) {
            alert(error.ExceptionMessage);
        });
    }

    $scope.$watch('FileDirecotoriesInfoApiModel.CurrentDirectory', function (newVal, oldVal) {
        if ($scope.FileDirecotoriesInfoApiModel != null &&
            $scope.FileDirecotoriesInfoApiModel.ParentDirectory == null) {
                apiCall.GetDrivesName($scope.FileDirecotoriesInfoApiModel.CurrentDirectory).success(function (data, status) {
                    $scope.DrivesName = data;
            }).error(function (error) {
                alert(error.ExceptionMessage);
            });
        }
    });
});



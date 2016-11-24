AnguarModule.service('apiCall', ['$http', function ($http) {

	this.GetApiCall = function (controllerName, methodName) {
	    var result = $http.get('api/' + controllerName + '/' + methodName).success(function (data, status) {
			result = (data);
		}).error(function () {
			alert("Something went wrong");
		});
		return result;
	};

    this.GetDirectory = function(path) {
        return $http.get('api/ExploreApi/GetDirectoryInfo?path=' + encodeURIComponent(path));
    }

    this.GetDrivesName = function (currentDirectory) {
        return $http.get('api/ExploreApi/GetDrivesName?currentDirectory=' + encodeURIComponent(currentDirectory));
    }

    this.GetDrivesDirectory = function (driveName) {
        return $http.get('api/ExploreApi/GetDrivesDirectory?driveName=' + encodeURIComponent(driveName));
    }
}]);


(function (app) {
    app.factory('apiservices', apiservices);

    apiservices.$inject = ['$http', '$rootScope', 'authenticationService'];

    function apiservices($http, $rootScope, authenticationService) {
        var baseUrl = $rootScope.apiUrl;
        return {
            get: get,
            post: post,
            put: put,
            del: del,
            getImage: getImage
        }
        function del(url, data, success, failure) {
            authenticationService.setHeader();
            $http.delete(baseUrl + url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    BaseService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function post(url, data, success, failure) {
            authenticationService.setHeader();
            console.log(authenticationService.setHeader());
            $http.post(baseUrl + url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    console.log('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
        function put(url, data, success, failure) {
            authenticationService.setHeader();
            $http.put(baseUrl + url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    console.log('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
        function get(url, params, success, failure) {

            authenticationService.setHeader();
            $http.get(baseUrl + url, params).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    console.log('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function getImage(url, params, success, failure) {

            authenticationService.setHeaderImage();

            $.ajax({
                headers: {
                    Authorization: "bearer d4495d92-9530-44a5-a88a-fc8410f9e321",
                },
                type: "GET",
                url: baseUrl + url,
                contentType: "image/png",
                mimeType: "text/plain; charset=x-user-defined",
                async: true,
                success: function (result) {
                    success(result);
                },
                error: function (e) {
                  //  failure(e);
                }
            });
        }
    }
})(angular.module('e-app'));
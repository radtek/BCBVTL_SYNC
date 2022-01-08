(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData', 'apiservices', '$rootScope', '$window',
        function ($http, $q, authenticationService, authData, apiService, $rootScope, $window) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
             
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post($rootScope.apiUrl + 'oauth/token', data, {
                    headers:
                    {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'Authorization': 'Basic ZXBwOmVwcDEyIw=='
                    },
                    
                }).then(function (response) {
                    authenticationService.setTokenInfo(response);
                    authenticationService.setHeader();
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.accessToken = response.data.access_token;
                    authData.authenticationData.expires_in = response.data.expires_in;
                    authData.authenticationData.token_type = response.data.token_type;
                }, function (err, status) {
                    console.log(err);
                })
                return deferred.promise;
            }
        }]);
})(angular.module('e-app'));
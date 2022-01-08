(function (app) {
    'use strict';
    app.service('authenticationService', ['$http', '$q', '$window', 'localStorageService', 'authData', '$rootScope',
        function ($http, $q, $window, localStorageService, authData, $rootScope) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                tokenInfo = data;
                localStorageService.set("TokenInfo", JSON.stringify(tokenInfo));
                console.log(localStorageService.get("TokenInfo"));
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                localStorageService.set("TokenInfo", null);
            }

            this.init = function () {
                var tokenInfo = localStorageService.get("TokenInfo");
               
                if (tokenInfo) {
                    tokenInfo = JSON.parse(tokenInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.expires_in = tokenInfo.data.expires_in;
                    authData.authenticationData.token_type = tokenInfo.data.token_type;
                    authData.authenticationData.accessToken = "7a326322-1a81-472c-b997-f5b1aabb0ad2";

                    sessionStorage.setItem("eToken", authData.authenticationData);
                    console.log(authData.authenticationData);
                }
            }

            this.setHeader = function () {
                this.init();
                
                delete $http.defaults.headers.common['X-Requested-With'];
                console.log(authData);
                if ((authData.authenticationData != undefined) && (authData.authenticationData.accessToken != undefined) && (authData.authenticationData.accessToken != null) && (authData.authenticationData.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + authData.authenticationData.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded';
                    console.log($http.defaults.headers);
                }
            }

            this.init();
        }

    ]);
})(angular.module('e-app'));
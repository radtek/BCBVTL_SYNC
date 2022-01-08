(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            expires_in: "",
            token_type: ""
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('e-app'));

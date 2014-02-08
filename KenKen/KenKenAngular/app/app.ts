'use strict';

var kenkenApp = angular.module('kenkenApp', [
    'kenkenControllers',
    'kenkenDirectives',
    'kenkenServices'
]);

kenkenApp.config([
    '$httpProvider', $httpProvider => {
        $httpProvider.defaults.useXDomain = true;
    }
]);
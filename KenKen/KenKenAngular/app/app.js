'use strict';

var kenkenApp = angular.module('kenkenApp', [
    'kenkenControllers',
    'kenkenDirectives'
]);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
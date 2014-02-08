'use strict';
var kenkenApp = angular.module('kenkenApp', [
    'kenkenControllers',
    'kenkenDirectives',
    'kenkenServices'
]);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
//# sourceMappingURL=app.js.map

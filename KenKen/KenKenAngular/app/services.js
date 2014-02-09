'use strict';
var kenkenServices = angular.module('kenkenServices', []);

kenkenServices.factory('puzzleService', [
    '$http', function ($http) {
        var baseUrl = 'http://localhost:63995/api/puzzle/';
        return {
            random: function (puzzleSize) {
                return $http.get(baseUrl + 'random', { params: { puzzleSize: puzzleSize } });
            },
            check: function (puzzle) {
                return $http.post('http://localhost:63995/api/puzzle/check', puzzle);
            }
        };
    }
]);
//# sourceMappingURL=services.js.map

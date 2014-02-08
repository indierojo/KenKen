'use strict';

var kenkenServices = angular.module('kenkenServices', []);

kenkenServices.factory('puzzleService', [
        '$http', $http=> {
            var baseUrl = 'http://localhost:63995/api/puzzle/';
            return {
                random: puzzleSize=> $http.get(baseUrl + 'random', { params: { puzzleSize: puzzleSize } }),
                check: puzzle=> $http.post('http://localhost:63995/api/puzzle/check', puzzle)
            };
        }
    ]
);

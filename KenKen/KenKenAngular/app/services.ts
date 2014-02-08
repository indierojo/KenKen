'use strict';

var kenkenServices = angular.module('kenkenServices', []);

kenkenServices.factory('puzzleService', [
        '$http', $http => {
            var baseUrl: String = 'http://localhost:63995/api/puzzle/';
            return {
                random: (puzzleSize:number) => $http.get(baseUrl + 'random', { params: { puzzleSize: puzzleSize } }),
                check: (puzzle:Puzzle)=> $http.post('http://localhost:63995/api/puzzle/check', puzzle)
            };
        }
    ]
);

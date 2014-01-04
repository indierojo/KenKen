var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.controller('kenkenApp', function($scope, $http) {
    $http.get('http://localhost:59519/puzzles/easy/1').success(function(data) {
        $scope.puzzle = data;
    });
});
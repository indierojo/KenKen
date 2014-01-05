var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.controller('kenkenApp', ['$scope', '$http', function ($scope, $http) {
    $http.get('http://localhost:63995/api/puzzle/').success(function (data) {
        $scope.puzzle = data;
    });
}]);
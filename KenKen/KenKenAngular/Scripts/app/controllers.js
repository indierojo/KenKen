var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
kenkenApp.controller('kenkenApp', ['$scope', '$http', function ($scope, $http) {
    $http.get('http://localhost:63995/api/puzzle/').success(function (data) {
        $scope.puzzle = data;
    });
}]);
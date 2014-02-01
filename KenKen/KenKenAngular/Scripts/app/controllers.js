var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
kenkenApp.controller('kenkenApp', ['$scope', '$http', function ($scope, $http) {
    $http.get('http://localhost:63995/api/puzzle/1').success(function (data) {
        populateCellBorderData(data);

        $scope.puzzle = data;
        $scope.getBorderClasses = function(cell) {
            return {
                topBorder: cell.topBorder,
                leftBorder: cell.leftBorder,
                rightBorder: cell.rightBorder,
                bottomBorder: cell.bottomBorder
            };
        };
        kenkenApp.Groups = data.Groups;
    });
}]);
kenkenApp.filter('groupSymbol', function() {
    return function (groupNumber) {
        var symbol = "";
        kenkenApp.Groups.forEach(function(group) {
            if (group.Group === groupNumber) {
                symbol = group.ExpectedTotal + group.Symbol;
                group.symbolShown = true;
                return;
            }
        });
        return symbol;
    };
});

var populateCellBorderData = function(data) {
    var gridSize = data.Grid.Cells.length;
    var seenGroups = [];

    var cells = data.Grid.Cells;

    for (var x = 0; x < gridSize; x++) {
        for (var y = 0; y < gridSize; y++) {
            var cell = cells[x][y];
            if (seenGroups.indexOf(cell.Group) === -1) {
                seenGroups.push(cell.Group);
                cell.showSymbol = true;
            }

            // cell.x = x;
            // cell.y = y;

            if (x > 0 && cells[x - 1][y].Group !== cell.Group) {
                cell.topBorder = true;
            }
            if (y > 0 && cells[x][y - 1].Group !== cell.Group) {
                cell.leftBorder = true;
            }
            if (y < gridSize - 1 && cells[x][y + 1].Group !== cell.Group) {
                cell.rightBorder = true;
            }
            if (x < gridSize - 1 && cells[x + 1][y].Group !== cell.Group) {
                cell.bottomBorder = true;
            }
        }
    }
}
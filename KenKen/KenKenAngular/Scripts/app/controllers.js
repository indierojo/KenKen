var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
kenkenApp.controller('kenkenApp', ['$scope', '$http', function ($scope, $http) {
    $http.get('http://localhost:63995/api/puzzle/1').success(function (data) {
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

                //   [0,0][0,1][0,2][0,3]
                //   [1,0][1,1][1,2][1,3]
                //   [2,0][2,1][2,2][2,3]
                //   [3,0][3,1][3,2][3,3]

                // cell.x = x;
                // cell.y = y;

                if (x === 0) {
                    cell.topOuterBorder = true;
                } else if (cells[x - 1][y].Group !== cell.Group) {
                    cell.topBorder = true;
                }
                if (y === 0) {
                    cell.leftOuterBorder = true;
                } else if (cells[x][y - 1].Group !== cell.Group) {
                    cell.leftBorder = true;
                }
                if (y === gridSize - 1) {
                    cell.rightOuterBorder = true;
                } else if (cells[x][y + 1].Group !== cell.Group) {
                    cell.rightBorder = true;
                }
                if (x === gridSize - 1) {
                    cell.bottomOuterBorder = true;
                } else if ( cells[x + 1][y].Group !== cell.Group) {
                    cell.bottomBorder = true;
                }
            }
        }

        $scope.puzzle = data;
        $scope.getBorderClasses = function(cell) {
            return {
                topOuterBorder: cell.topOuterBorder,
                topBorder: cell.topBorder,
                leftOuterBorder: cell.leftOuterBorder,
                leftBorder: cell.leftBorder,
                rightOuterBorder: cell.rightOuterBorder,
                rightBorder: cell.rightBorder,
                bottomOuterBorder: cell.bottomOuterBorder,
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
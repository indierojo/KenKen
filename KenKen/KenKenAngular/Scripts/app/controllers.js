var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
kenkenApp.controller('kenkenApp', ['$scope', '$http', function ($scope, $http) {
    $http.get('http://localhost:63995/api/puzzle/1').success(function(data) {
        populateCellBorderData(data);
        kenkenApp.Groups = data.Groups;

        $scope.puzzle = data;
    });
    
    $scope.selectCell = function (cell) {
        $scope.selectedCell = cell;
    };

    $scope.isSelected = function (cell) {
        return $scope.selectedCell === cell;
    };

    $scope.deselectAllCells = function() {
        return $scope.selectedCell = null;
    };

    $scope.resetPuzzle = function () {
        $scope.puzzle.Grid.Cells.forEach(function (row) {
            row.forEach(function(cell) {
                cell.Value = null;
            });
        });
    };

    $scope.checkSolution = function () {
        var isValid = checkPuzzleSolution($scope.puzzle);
        if (isValid) {
            $scope.isSolved = true;
        } else {
            $scope.hasErrors = true;
        }
    };
}]);
kenkenApp.directive('ngFocusWhenSelected', function ($timeout) {
    return function (scope, element, attrs) {
        // 1 ms timeout required or the text within is not selected
        $timeout(function() {
            element[0].select();
        }, 1);
    };
});
kenkenApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});
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
};

var checkPuzzleSolution = function(puzzle) {
//    var cellArray = puzzle.Grid.Cells;
    return false;
};
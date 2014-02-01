var kenkenApp = angular.module('kenkenApp', []);

kenkenApp.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
    }
]);
kenkenApp.controller('kenkenApp', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    var puzzleId = 1;
    if ($location.path()) {
        var parsedPuzzleId = parseInt($location.path().replace("/", ""));
        if (typeof parsedPuzzleId == 'number') {
            puzzleId = parsedPuzzleId;
        }
    }

    $scope.puzzleId = puzzleId;

    $http.get('http://localhost:63995/api/puzzle/' + puzzleId).success(function (data) {
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
        $scope.information = null;
        $scope.isSolved = null;
        $scope.hasErrors = null;
    };

    $scope.checkSolution = function () {
        $scope.information = "Checking puzzle solution...";
        $scope.isSolved = null;
        $scope.hasErrors = null;

        var cellArray = $scope.puzzle.Grid.Cells;
        
        $http.post('http://localhost:63995/api/puzzle/' + $scope.puzzleId + '/check', cellArray).success(function (data) {
            $scope.information = null;
            if (data === "true") {
                $scope.isSolved = true;
            } else {
                $scope.hasErrors = true;
            }
        });
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
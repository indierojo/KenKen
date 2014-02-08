'use strict';

var kenkenControllers = angular.module('kenkenControllers', []);

kenkenControllers.controller('kenkenController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    var puzzleSize = 3;
    if ($location.path()) {
        var parsedPuzzleSize = parseInt($location.path().replace("/", ""));
        if (typeof parsedPuzzleSize == 'number') {
            puzzleSize = parsedPuzzleSize;
        }
    }

    $scope.puzzleSize = puzzleSize;
    $scope.showErrors = false;

    $http.get('http://localhost:63995/api/puzzle/random/' + puzzleSize).success(function (data) {
        populateCellBorderData(data);
        kenkenControllers.Groups = data.Groups;

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
        $scope.puzzleErrors = null;
        $scope.information = null;
        $scope.isSolved = null;
        $scope.hasErrors = null;
    };

    $scope.checkSolution = function () {
        $scope.information = "Checking puzzle solution...";
        $scope.isSolved = null;
        $scope.hasErrors = null;
        $scope.puzzleErrors = null;

        $http.post('http://localhost:63995/api/puzzle/check', $scope.puzzle).success(function (data) {
            $scope.information = null;
            if (data.WasValid) {
                $scope.isSolved = true;
            } else {
                $scope.hasErrors = true;
                $scope.puzzleErrors = data.FailureReason.split('\n');
            }
        });
    };
}]);
kenkenControllers.filter('groupSymbol', function () {
    return function (groupNumber) {
        var symbol = "";
        kenkenControllers.Groups.forEach(function (group) {
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
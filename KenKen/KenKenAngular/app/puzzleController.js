'use strict';
var puzzleController = angular.module('puzzleController', []);

puzzleController.controller('puzzleController', [
    '$scope',
    function ($scope) {
        $scope.navigateUp = function (cell) {
            if (cell.X === 0) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X - 1, cell.Y));
        };
        $scope.navigateDown = function (cell) {
            if (cell.X === $scope.puzzleSize - 1) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X + 1, cell.Y));
        };
        $scope.navigateLeft = function (cell) {
            if (cell.Y === 0) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X, cell.Y - 1));
        };
        $scope.navigateRight = function (cell) {
            if (cell.Y === $scope.puzzleSize - 1) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X, cell.Y + 1));
        };

        var getCellAt = function (x, y) {
            var foundCell;
            $scope.puzzle.Grid.Cells.forEach(function (row) {
                row.forEach(function (cell) {
                    if (cell.X === x && cell.Y === y) {
                        foundCell = cell;
                    }
                });
            });

            return foundCell;
        };
    }
]);
//# sourceMappingURL=puzzleController.js.map

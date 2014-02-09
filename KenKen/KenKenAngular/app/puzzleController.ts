'use strict';

var puzzleController = angular.module('puzzleController', []);

puzzleController.controller('puzzleController', [
    '$scope',
    ($scope: PuzzleScope) => {

        $scope.navigateUp = (cell: Cell) => {
            if (cell.X === 0) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X - 1, cell.Y));
        };
        $scope.navigateDown = (cell: Cell) => {
            if (cell.X === $scope.puzzleSize - 1) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X + 1, cell.Y));
        };
        $scope.navigateLeft = (cell: Cell) => {
            if (cell.Y === 0) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X, cell.Y - 1));
        };
        $scope.navigateRight = (cell: Cell) => {
            if (cell.Y === $scope.puzzleSize - 1) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X, cell.Y + 1));
        };

        var getCellAt = (x: number, y: number): Cell => {
            var foundCell: Cell;
            $scope.puzzle.Grid.Cells.forEach((row: Cell[]) => {
                row.forEach((cell: Cell) => {
                    if (cell.X === x && cell.Y === y) {
                        foundCell = cell;
                    }
                });
            });

            return foundCell;
        };
    }
]); 
'use strict';
var appController = angular.module('appController', []);

appController.controller('appController', [
    '$scope', '$location', 'puzzleService',
    function ($scope, $location, puzzleService) {
        var puzzleSize = 3;
        if ($location.path()) {
            var parsedPuzzleSize = parseInt($location.path().replace("/", ""), 10);
            if (parsedPuzzleSize) {
                puzzleSize = parsedPuzzleSize;
            }
        }

        $scope.puzzleSize = puzzleSize;
        puzzleService.random(puzzleSize).success(function (puzzle) {
            populateCellBorderData(puzzle);
            populateSymbolData(puzzle);
            $scope.puzzle = puzzle;
        });
        $scope.showErrors = false;

        $scope.selectCell = function (cell) {
            $scope.selectedCell = cell;
        };
        $scope.isSelected = function (cell) {
            return $scope.selectedCell === cell;
        };

        $scope.sanitizeInput = function (cell) {
            $scope.deselectAllCells();
            var input = cell.Value;

            if (!input) {
                return;
            }
            var validValuesRegex = '^[1-' + $scope.puzzleSize + ']+$';
            var isValid = input.match(validValuesRegex);

            if (input.length === 1 && isValid) {
                cell.displayValue = input;
                cell.notes = null;
            } else {
                var existingNotes = cell.notes || {};
                if (isValid) {
                    existingNotes.possibles = input.split('');
                    cell.notes = existingNotes;
                } else if (input[0] === '^' && input.substr(1).match(validValuesRegex)) {
                    existingNotes.alternatives = input.substr(1).split('');
                    cell.notes = existingNotes;
                } else if (input.indexOf(',') !== -1 && input.replace(',', '').match(validValuesRegex)) {
                    var allValues = input.split(',');
                    existingNotes.possibles = allValues;
                    cell.notes = existingNotes;
                }
                cell.displayValue = null;
                cell.Value = null;
            }
        };

        $scope.deselectAllCells = function () {
            return $scope.selectedCell = null;
        };

        $scope.resetPuzzle = function () {
            $scope.puzzle.Grid.Cells.forEach(function (row) {
                row.forEach(function (cell) {
                    $scope.clearValues(cell);
                });
            });
            $scope.puzzleErrors = null;
            $scope.information = null;
            $scope.isSolved = null;
            $scope.hasErrors = null;
        };

        $scope.clearValues = function (cell) {
            cell.notes = null;
            cell.Value = null;
            cell.displayValue = null;
        };

        $scope.checkSolution = function () {
            $scope.information = "Checking puzzle solution...";
            $scope.isSolved = null;
            $scope.hasErrors = null;
            $scope.puzzleErrors = null;

            var puzzle = $scope.puzzle;
            puzzleService.check(puzzle).success(function (checkResult) {
                $scope.information = null;
                if (checkResult.WasValid) {
                    $scope.isSolved = true;
                } else {
                    $scope.hasErrors = true;
                    $scope.puzzleErrors = checkResult.FailureReason.split('\n');
                }
            });
        };

        var populateSymbolData = function (puzzle) {
            var groups = puzzle.Groups;
            var cellGrid = puzzle.Grid.Cells;

            cellGrid.forEach(function (row) {
                row.forEach(function (cell) {
                    groups.forEach(function (group) {
                        if (group.Group === cell.Group) {
                            cell.Symbol = group.ExpectedTotal + group.Symbol;
                            return;
                        }
                    });
                });
            });
        };

        var populateCellBorderData = function (puzzle) {
            var gridSize = puzzle.Grid.Cells.length;
            var seenGroups = [];

            var cells = puzzle.Grid.Cells;

            for (var x = 0; x < gridSize; x++) {
                for (var y = 0; y < gridSize; y++) {
                    var cell = cells[x][y];
                    if (seenGroups.indexOf(cell.Group) === -1) {
                        seenGroups.push(cell.Group);
                        cell.showSymbol = true;
                    }

                    cell.X = x;
                    cell.Y = y;

                    if (x > 0 && cells[x - 1][y].Group !== cell.Group) {
                        cell.isOnTopSide = true;
                    }
                    if (y > 0 && cells[x][y - 1].Group !== cell.Group) {
                        cell.isOnLeftSide = true;
                    }
                    if (y < gridSize - 1 && cells[x][y + 1].Group !== cell.Group) {
                        cell.isOnRightSide = true;
                    }
                    if (x < gridSize - 1 && cells[x + 1][y].Group !== cell.Group) {
                        cell.isOnBottomSide = true;
                    }
                }
            }
        };
    }
]);
//# sourceMappingURL=appController.js.map

'use strict';

var kenkenControllers = angular.module('kenkenControllers', []);

kenkenControllers.controller('kenkenController', [
    '$scope', '$location', 'puzzleService',
    ($scope: KenKenScope, $location: ng.ILocationService, puzzleService: PuzzleService)=> {

        var puzzleSize = 3;
        if ($location.path()) {
            var parsedPuzzleSize = parseInt($location.path().replace("/", ""), 10);
            if (parsedPuzzleSize) {
                puzzleSize = parsedPuzzleSize;
            }
        }

        $scope.puzzleSize = puzzleSize;
        puzzleService.random(puzzleSize).success((puzzle:Puzzle)=> {
            populateCellBorderData(puzzle);
            populateSymbolData(puzzle);
            $scope.puzzle = puzzle;
        });
        $scope.showErrors = false;

        $scope.selectCell = (cell:Cell) => {
            $scope.selectedCell = cell;
        };

        $scope.isSelected = (cell: Cell)=> $scope.selectedCell === cell;

        $scope.deselectAllCells = () => $scope.selectedCell = null;

        $scope.navigateUp = (cell: Cell) => {
            if (cell.X === 0) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X - 1, cell.Y));
        };
        $scope.navigateDown = (cell: Cell)=> {
            if (cell.X === puzzleSize - 1) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X + 1, cell.Y));
        };
        $scope.navigateLeft = (cell: Cell)=> {
            if (cell.Y === 0) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X, cell.Y - 1));
        };
        $scope.navigateRight = (cell: Cell)=> {
            if (cell.Y === puzzleSize - 1) {
                return;
            }

            $scope.selectCell(getCellAt(cell.X, cell.Y + 1));
        };

        $scope.resetPuzzle = ()=> {
            $scope.puzzle.Grid.Cells.forEach((row:Cell[])=> {
                row.forEach(cell=> {
                    cell.Value = null;
                });
            });
            $scope.puzzleErrors = null;
            $scope.information = null;
            $scope.isSolved = null;
            $scope.hasErrors = null;
        };

        $scope.checkSolution = ()=> {
            $scope.information = "Checking puzzle solution...";
            $scope.isSolved = null;
            $scope.hasErrors = null;
            $scope.puzzleErrors = null;

            var puzzle:Puzzle = $scope.puzzle;
            puzzleService.check(puzzle).success((checkResult: ValidationResult)=> {
                $scope.information = null;
                if (checkResult.WasValid) {
                    $scope.isSolved = true;
                } else {
                    $scope.hasErrors = true;
                    $scope.puzzleErrors = checkResult.FailureReason.split('\n');
                }
            });
        };

        var getCellAt = (x: number, y: number): Cell => {
            var foundCell: Cell;
            $scope.puzzle.Grid.Cells.forEach((row: Cell[])=> {
                row.forEach((cell: Cell)=> {
                    if (cell.X === x && cell.Y === y) {
                        foundCell = cell;
                    }
                });
            });

            return foundCell;
        };

        var populateSymbolData = (puzzle: Puzzle) => {
            var groups = puzzle.Groups;
            var cellGrid = puzzle.Grid.Cells;

            cellGrid.forEach(row=> {
                row.forEach(cell=> {
                    groups.forEach(group=> {
                        if (group.Group === cell.Group) {
                            cell.Symbol = group.ExpectedTotal + group.Symbol;
                            return;
                        }
                    });
                });
            });
        };

        var populateCellBorderData = (puzzle: Puzzle) => {
            var gridSize = puzzle.Grid.Cells.length;
            var seenGroups: number[] = [];

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
'use strict';

var kenkenControllers = angular.module('kenkenControllers', []);

kenkenControllers.controller('kenkenController', [
    '$scope', '$location', 'puzzleService',
    ($scope, $location, puzzleService)=> {

        var puzzleSize = 3;
        if ($location.path()) {
            var parsedPuzzleSize = parseInt($location.path().replace("/", ""));
            if (typeof parsedPuzzleSize == 'number') {
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

        $scope.deselectAllCells = ()=> $scope.selectedCell = null;

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
    }
]);

var populateSymbolData = (puzzle: Puzzle)=> {
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

var populateCellBorderData = (puzzle:Puzzle)=> {
    var gridSize = puzzle.Grid.Cells.length;
    var seenGroups:number[] =[];

    var cells = puzzle.Grid.Cells;

    for (var x = 0; x < gridSize; x++) {
        for (var y = 0; y < gridSize; y++) {
            var cell = cells[x][y];
            if (seenGroups.indexOf(cell.Group) === -1) {
                seenGroups.push(cell.Group);
                cell.showSymbol = true;
            }

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
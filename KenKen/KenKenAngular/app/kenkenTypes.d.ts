interface KenKenScope extends ng.IScope {
    puzzleSize: number;
    puzzle: Puzzle;
    showErrors: boolean;
    selectedCell: Cell;

    puzzleErrors: string[];
    information: string;
    hasErrors: boolean;
    isSolved: boolean;

    selectCell(cell: Cell);
    isSelected(cell: Cell): boolean;
    deselectAllCells();
    navigateUp(cell: Cell);
    navigateDown(cell: Cell);
    navigateLeft(cell: Cell);
    navigateRight(cell: Cell);
    resetPuzzle();
    checkSolution();
}

interface PuzzleService extends ng.IServiceProvider {
    random(puzzleSize: number):ng.IHttpPromise<Puzzle>;
    check(puzzle: Puzzle):ng.IHttpPromise<ValidationResult>;
}

interface Cell {
    X: number;
    Y: number;

    Symbol: string;
    Value: number;
    Group: number;
    showSymbol: boolean;
    isOnLeftSide: boolean;
    isOnRightSide: boolean;
    isOnTopSide: boolean;
    isOnBottomSide: boolean;
} 

interface GroupDefinition {
    Group: number;
    ExpectedTotal: number;
    Symbol: string;
}

interface Puzzle {
    Grid: {
        Cells: Cell[][]
    }
    Groups: GroupDefinition[]
}

interface ValidationResult {
    WasValid: Boolean;
    FailureReason: String;
}
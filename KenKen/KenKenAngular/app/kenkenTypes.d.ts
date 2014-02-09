interface AppScope extends ng.IScope {
    puzzleSize: number;
    puzzle: Puzzle;
    showErrors: boolean;
    selectedCell: Cell;

    onValueChange();
    sanitizeInput(cell: Cell);
    selectCell(cell: Cell);
    isSelected(cell: Cell): boolean;
    deselectAllCells();
    clearValues(cell: Cell);

    puzzleErrors: string[];
    information: string;
    hasErrors: boolean;
    isSolved: boolean;
    resetPuzzle();
    checkSolution();
}

interface PuzzleScope extends AppScope {
    navigateUp(cell: Cell);
    navigateDown(cell: Cell);
    navigateLeft(cell: Cell);
    navigateRight(cell: Cell);
}

interface NotesScope extends AppScope {
    notes: string;
    cell: Cell;
}

interface PuzzleService extends ng.IServiceProvider {
    random(puzzleSize: number):ng.IHttpPromise<Puzzle>;
    check(puzzle: Puzzle):ng.IHttpPromise<ValidationResult>;
}

interface Notes {
    possibles: string[];
    alternatives: string[];
}

interface Cell {
    X: number;
    Y: number;

    notes: Notes;

    Symbol: string;
    Value: string;
    displayValue: string;
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
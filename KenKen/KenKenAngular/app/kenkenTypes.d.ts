interface AppScope extends ng.IScope {
    puzzleSize: number;
    puzzle: Puzzle;
    showErrors: boolean;
    selectedCell: Cell;

    selectCell(cell: Cell);
    isSelected(cell: Cell): boolean;
    deselectAllCells();

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
}

interface PuzzleController extends ng.IModule {
    
}

interface NotesController extends ng.IModule {
    
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
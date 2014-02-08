interface Cell {
    Symbol: string;
    Value: number;
    Group: number;
    showSymbol: boolean;
    leftBorder: boolean;
    rightBorder: boolean;
    topBorder: boolean;
    bottomBorder: boolean;
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
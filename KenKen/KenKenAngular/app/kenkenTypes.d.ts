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
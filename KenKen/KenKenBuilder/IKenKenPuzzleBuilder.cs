using Domain;

namespace KenKenBuilder
{
    interface IKenKenPuzzleBuilder
    {
        Puzzle Build(DifficultyLevel difficultyLevel, GridSize gridSize);
    }
}

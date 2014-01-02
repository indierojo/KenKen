using Domain;

namespace KenKenBuilder
{
    interface IKenKenPuzzleBuilder
    {
        Cell[,] Build(DifficultyLevel difficultyLevel, GridSize gridSize);
    }
}

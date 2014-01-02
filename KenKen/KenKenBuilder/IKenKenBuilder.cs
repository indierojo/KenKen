using Domain;

namespace KenKenBuilder
{
    interface IKenKenBuilder
    {
        Cell[,] Build(DifficultyLevel difficultyLevel, GridSize gridSize);
    }
}

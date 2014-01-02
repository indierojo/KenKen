using Domain;

namespace KenKenBuilder
{
    public interface IKenKenPuzzleBuilder
    {
        Puzzle Build(DifficultyLevel difficultyLevel, GridSize gridSize);
    }
}

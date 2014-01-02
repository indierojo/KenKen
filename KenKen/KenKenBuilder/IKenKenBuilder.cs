using Domain;

namespace KenKenBuilder
{
    interface IKenKenBuilder
    {
        Grid Build(DifficultyLevel difficultyLevel, int gridSize);
    }
}

using Domain;

namespace PuzzleValidator
{
    public interface IPuzzleValidator
    {
        ValidationResult CheckForValidity(Puzzle puzzle);
    }
}

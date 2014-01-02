using Domain;

namespace KenKenWebapp.Models
{
    public class PuzzleModel
    {
        public PuzzleModel(Puzzle puzzle)
        {
            Puzzle = puzzle;
        }

        public Puzzle Puzzle { get; set; }
    }
}
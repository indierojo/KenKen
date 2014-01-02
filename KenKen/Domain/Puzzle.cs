namespace Domain
{
    public class Puzzle
    {
        public Puzzle(Cell[,] cells)
        {
            GridCells = cells;
        }

        public Cell[,] GridCells { get; set; }
    }
}

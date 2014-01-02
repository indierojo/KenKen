namespace Domain
{
    public class Puzzle
    {
        public Puzzle(Cell[,] cells)
        {
            GridCells = cells;
        }

        ushort GetGridDimensions()
        {
            return (ushort) GridCells.GetLength(0);
        }

        public Cell[,] GridCells { get; set; }
    }
}

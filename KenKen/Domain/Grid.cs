namespace Domain
{
    public class Grid
    {
        public Grid(Cell[,] cells)
        {
            GridCells = cells;
        }

        public Cell[,] GridCells { get; set; }
    }
}

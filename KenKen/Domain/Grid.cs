namespace Domain
{
    public class Grid
    {
        public Grid(int size)
        {
            GridCells = new Cell[size, size];
        }

        public Cell[,] GridCells { get; private set; }
    }
}

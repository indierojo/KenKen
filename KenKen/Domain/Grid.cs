using System;
using System.Runtime.Serialization;

namespace Domain
{
    [DataContract]
    public class Grid
    {
        public Grid(Cell[][] cells)
        {
            Cells = cells;
            GridSize = cells == null ? "Invalid" : GetGridSizeFor(cells.GetLength(0)).ToString();
        }

        private static GridSize GetGridSizeFor(int cellLength)
        {
            if (cellLength < 3 || cellLength > 7)
            {
                throw new ArgumentOutOfRangeException("cellLength", "Grid size must be between 3 and 7");
            }

            return (GridSize) cellLength;
        }

        [DataMember] public Cell[][] Cells { get; private set; }
        [DataMember] public string GridSize { get; private set; }
    }
}

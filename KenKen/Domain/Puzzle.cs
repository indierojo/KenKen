using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Puzzle
    {
        public readonly Dictionary<ushort, GroupDefinition> Groups;
        private ushort? _gridDimensions;

        public Puzzle(Cell[][] cells, IEnumerable<GroupDefinition> groups)
        {
            Groups = groups.ToDictionary(g => g.GroupNumber);
            GridCells = cells;
        }

        public ushort GetGridDimensions()
        {
            if (!_gridDimensions.HasValue)
            {
                _gridDimensions = (ushort) GridCells.GetLength(0);
            }

            return _gridDimensions.Value;
        }

        public Cell[][] GridCells { get; set; }
    }
}

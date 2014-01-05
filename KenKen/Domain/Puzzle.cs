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
            Grid = new Grid(cells);
        }

        public ushort GetGridDimensions()
        {
            if (!_gridDimensions.HasValue)
            {
                _gridDimensions = (ushort) Grid.Cells.GetLength(0);
            }

            return _gridDimensions.Value;
        }

        public Grid Grid { get; set; }
    }
}

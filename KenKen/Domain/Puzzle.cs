using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain
{
    [DataContract]
    public class Puzzle
    {
        [DataMember]
        public readonly IEnumerable<GroupDefinition> Groups;
        private ushort? _gridDimensions;

        public Puzzle(Cell[][] cells, IEnumerable<GroupDefinition> groups)
        {
            Groups = groups;
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

        [DataMember]
        public Grid Grid { get; set; }
    }
}

using System.Collections.Generic;
using Domain;
using Domain.Operations;

namespace KenKenBuilder
{
    public static class TinyTestPuzzleBuilder
    {
        public static Puzzle GetTinyPuzzle()
        {
            var groups = new List<GroupDefinition>
            {
                new GroupDefinition(1, new NoOp(), 1),
                new GroupDefinition(2, new NoOp(), 2),
                new GroupDefinition(3, new NoOp(), 3),
                new GroupDefinition(4, new NoOp(), 2),
                new GroupDefinition(5, new NoOp(), 3),
                new GroupDefinition(6, new NoOp(), 1),
                new GroupDefinition(7, new NoOp(), 3),
                new GroupDefinition(8, new NoOp(), 1),
                new GroupDefinition(9, new NoOp(), 2),
            };

            var cells = new[]
            {
                new[]{EmptyCell(1), EmptyCell(2), EmptyCell(3)},
                new[]{EmptyCell(4), EmptyCell(5), EmptyCell(6)},
                new[]{EmptyCell(7), EmptyCell(8), EmptyCell(9)}
            };
            
            // Solution
            // 1 2 3
            // 2 3 1
            // 3 1 2

            return new Puzzle(cells, groups);
        }

        private static Cell EmptyCell(ushort groupNumber)
        {
            return new Cell{Group = groupNumber};
        }
    }
}

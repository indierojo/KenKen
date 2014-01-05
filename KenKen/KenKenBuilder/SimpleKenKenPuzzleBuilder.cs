using System;
using System.Collections.Generic;
using Domain;
using Domain.Operations;

namespace KenKenBuilder
{
    public class SimpleKenKenPuzzleBuilder : IKenKenPuzzleBuilder
    {
        private const ushort NoOperationFourGroup = 1;
        private const ushort DivisionTwoGroup = 2;
        private const ushort MultiplicationNineGroup = 3;
        private const ushort MultiplicationSixGroup = 4;
        private const ushort AdditionThreeGroup = 5;
        private const ushort SubtractionThreeGroup = 6;
        private const ushort MultiplicationTwentyFourGroup = 7;

        public Puzzle Build(DifficultyLevel difficultyLevel, GridSize gridSize)
        {
            if (gridSize != GridSize.FourByFour)
            {
                throw new ArgumentOutOfRangeException("gridSize", "SimpleKenKenPuzzleBuilder only supports 4x4 grids.");
            }

            if (difficultyLevel != DifficultyLevel.Easy)
            {
                throw new ArgumentOutOfRangeException("gridSize", "SimpleKenKenPuzzleBuilder only supports easy puzzles.");
            }

            var groups = new List<GroupDefinition>
            {
                new GroupDefinition(NoOperationFourGroup, new NoOp(), 4),
                new GroupDefinition(DivisionTwoGroup, new Division(), 2),
                new GroupDefinition(MultiplicationNineGroup, new Multiplication(), 9),
                new GroupDefinition(MultiplicationSixGroup, new Multiplication(), 6),
                new GroupDefinition(AdditionThreeGroup, new Addition(), 3),
                new GroupDefinition(SubtractionThreeGroup, new Subtraction(), 3),
                new GroupDefinition(MultiplicationTwentyFourGroup, new Multiplication(), 24),
            };

            var cells = new[]
            {
                new[]{EmptyCell(NoOperationFourGroup), EmptyCell(DivisionTwoGroup), EmptyCell(MultiplicationNineGroup), EmptyCell(MultiplicationNineGroup)},
                new[]{EmptyCell(MultiplicationSixGroup), EmptyCell(DivisionTwoGroup), EmptyCell(MultiplicationNineGroup), EmptyCell(AdditionThreeGroup)},
                new[]{EmptyCell(MultiplicationSixGroup), EmptyCell(SubtractionThreeGroup), EmptyCell(SubtractionThreeGroup), EmptyCell(AdditionThreeGroup)},
                new[]{EmptyCell(MultiplicationSixGroup), EmptyCell(MultiplicationTwentyFourGroup), EmptyCell(MultiplicationTwentyFourGroup), EmptyCell(MultiplicationTwentyFourGroup)}
            };

            // Puzzle
            // 4.  2/  9x  9x
            // 6x  2/  9x  3+
            // 6x  3-  3-  3+
            // 6x  24x 24x 24x

            // Solution
            // 4 2 1 3
            // 2 4 3 1
            // 3 1 4 2
            // 1 3 2 4

            return new Puzzle(cells, groups);
        }

        private Cell EmptyCell(ushort groupNumber)
        {
            return new Cell(groupNumber);
        }
    }
}

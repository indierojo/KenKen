using System;
using Domain;

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
            var cells = new[,]
            {
                {EmptyCell(NoOperationFourGroup), EmptyCell(DivisionTwoGroup), EmptyCell(MultiplicationNineGroup), EmptyCell(MultiplicationNineGroup)},
                {EmptyCell(MultiplicationSixGroup), EmptyCell(DivisionTwoGroup), EmptyCell(MultiplicationNineGroup), EmptyCell(AdditionThreeGroup)},
                {EmptyCell(MultiplicationSixGroup), EmptyCell(SubtractionThreeGroup), EmptyCell(SubtractionThreeGroup), EmptyCell(AdditionThreeGroup)},
                {EmptyCell(MultiplicationSixGroup), EmptyCell(MultiplicationTwentyFourGroup), EmptyCell(MultiplicationTwentyFourGroup), EmptyCell(MultiplicationTwentyFourGroup)}
            };

            return new Puzzle(cells);
        }

        private Cell EmptyCell(ushort groupNumber)
        {
            return new Cell(groupNumber);
        }
    }
}

using System;
using Domain;

namespace KenKenBuilder
{
    public class SimpleKenKenBuilder : IKenKenBuilder
    {
        private const ushort NoOperationFourGroup = 1;
        private const ushort DivisionTwoGroup = 2;
        private const ushort MultiplicationNineGroup = 3;
        private const ushort MultiplicationSixGroup = 4;
        private const ushort AdditionThreeGroup = 5;
        private const ushort SubtractionThreeGroup = 6;
        private const ushort MultiplicationTwentyFourGroup = 7;

        public Cell[,] Build(DifficultyLevel difficultyLevel, int gridSize)
        {
            if (gridSize < 4 || gridSize > 4)
            {
                throw new ArgumentOutOfRangeException("gridSize", "SimpleKenKenBuilder only supports small grids.");
            }

            if (difficultyLevel != DifficultyLevel.Easy)
            {
                throw new ArgumentOutOfRangeException("gridSize", "SimpleKenKenBuilder only supports easy puzzles.");
            }

            return new[,]
            {
                {EmptyCell(NoOperationFourGroup), EmptyCell(DivisionTwoGroup), EmptyCell(MultiplicationNineGroup), EmptyCell(MultiplicationNineGroup)},
                {EmptyCell(MultiplicationSixGroup), EmptyCell(DivisionTwoGroup), EmptyCell(MultiplicationNineGroup), EmptyCell(AdditionThreeGroup)},
                {EmptyCell(MultiplicationSixGroup), EmptyCell(SubtractionThreeGroup), EmptyCell(SubtractionThreeGroup), EmptyCell(AdditionThreeGroup)},
                {EmptyCell(MultiplicationSixGroup), EmptyCell(MultiplicationTwentyFourGroup), EmptyCell(MultiplicationTwentyFourGroup), EmptyCell(MultiplicationTwentyFourGroup)}
            };
        }

        private Cell EmptyCell(ushort groupNumber)
        {
            return new Cell(groupNumber);
        }
    }
}

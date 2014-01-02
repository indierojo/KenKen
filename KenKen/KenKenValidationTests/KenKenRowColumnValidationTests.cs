using System;
using System.Collections.Generic;
using Domain;
using Domain.Operations;
using KenKenBuilder;
using NUnit.Framework;
using PuzzleValidator;

namespace KenKenValidationTests
{
    [TestFixture]
    public class KenKenRowColumnValidationTests
    {
        private static readonly IKenKenPuzzleBuilder Builder = new SimpleKenKenPuzzleBuilder();

        [Test]
        public void ValidationShouldFailForMissingValues()
        {
            var validPuzzle = Builder.Build(DifficultyLevel.Easy, GridSize.FourByFour);
            var validationResult = IsValidPuzzle(validPuzzle);
            Assert.False(validationResult.WasSuccess);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForDuplicateRowValues()
        {
            var validationResult = IsValidPuzzle(GetDupeRowPuzzle());
            Assert.False(validationResult.WasSuccess);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForDuplicateColumnValues()
        {
            var validationResult = IsValidPuzzle(GetDupeColumnPuzzle());
            Assert.False(validationResult.WasSuccess);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForLowValues()
        {
            var validationResult = IsValidPuzzle(GetLowNumberPuzzle());
            Assert.False(validationResult.WasSuccess);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForHighValues()
        {
            var validationResult = IsValidPuzzle(GetHighNumberPuzzle());
            Assert.False(validationResult.WasSuccess);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldSucceedForValidValues()
        {
            var solvedPuzzle = GetSolvedPuzzle();
            var validationResult = IsValidPuzzle(solvedPuzzle);
            Assert.True(validationResult.WasSuccess, validationResult.FailureReason ?? "");
        }

        private ValidationResult IsValidPuzzle(Puzzle puzzle)
        {
            var rowColumnValidator = new RowAndColumnValidator();
            return rowColumnValidator.CheckForValidity(puzzle);
        }

        private Puzzle GetSolvedPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[,]
            {
                {CellWithValue(1, 4), CellWithValue(2, 2), CellWithValue(3, 1), CellWithValue(3, 3)},
                {CellWithValue(4, 2), CellWithValue(2, 4), CellWithValue(3, 3), CellWithValue(5, 1)},
                {CellWithValue(4, 3), CellWithValue(6, 1), CellWithValue(6, 4), CellWithValue(5, 2)},
                {CellWithValue(4, 1), CellWithValue(7, 3), CellWithValue(7, 2), CellWithValue(7, 4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetDupeRowPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[,]
            {
                {CellWithValue(1, 4), CellWithValue(2, 4), CellWithValue(3, 1), CellWithValue(3, 3)},
                {CellWithValue(4, 2), CellWithValue(2, 4), CellWithValue(3, 3), CellWithValue(5, 1)},
                {CellWithValue(4, 3), CellWithValue(6, 1), CellWithValue(6, 4), CellWithValue(5, 2)},
                {CellWithValue(4, 1), CellWithValue(7, 3), CellWithValue(7, 2), CellWithValue(7, 4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetDupeColumnPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[,]
            {
                {CellWithValue(1, 4), CellWithValue(2, 2), CellWithValue(3, 1), CellWithValue(3, 3)},
                {CellWithValue(4, 2), CellWithValue(2, 4), CellWithValue(3, 3), CellWithValue(5, 1)},
                {CellWithValue(4, 3), CellWithValue(6, 2), CellWithValue(6, 4), CellWithValue(5, 2)},
                {CellWithValue(4, 1), CellWithValue(7, 3), CellWithValue(7, 2), CellWithValue(7, 4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetLowNumberPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[,]
            {

                {CellWithValue(1, 0), CellWithValue(2, 4), CellWithValue(3, 4), CellWithValue(3, 4)},
                {CellWithValue(4, 4), CellWithValue(2, 4), CellWithValue(3, 4), CellWithValue(5, 4)},
                {CellWithValue(4, 4), CellWithValue(6, 4), CellWithValue(6, 4), CellWithValue(5, 4)},
                {CellWithValue(4, 4), CellWithValue(7, 4), CellWithValue(7, 4), CellWithValue(7, 4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetHighNumberPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[,]
            {

                {CellWithValue(1, 100), CellWithValue(2, 4), CellWithValue(3, 4), CellWithValue(3, 4)},
                {CellWithValue(4, 4), CellWithValue(2, 4), CellWithValue(3, 4), CellWithValue(5, 4)},
                {CellWithValue(4, 4), CellWithValue(6, 4), CellWithValue(6, 4), CellWithValue(5, 4)},
                {CellWithValue(4, 4), CellWithValue(7, 4), CellWithValue(7, 4), CellWithValue(7, 4)}
            };

            return new Puzzle(cells, groups);
        }

        private Cell CellWithValue(ushort groupNumber, ushort value)
        {
            return new Cell(groupNumber, value);
        }

        private static IEnumerable<GroupDefinition> GetTestGroupDefinitions()
        {
            var groups = new List<GroupDefinition>
            {
                new GroupDefinition(1, new NoOp(), 4),
                new GroupDefinition(2, new Division(), 2),
                new GroupDefinition(3, new Multiplication(), 9),
                new GroupDefinition(4, new Multiplication(), 6),
                new GroupDefinition(5, new Addition(), 3),
                new GroupDefinition(6, new Subtraction(), 3),
                new GroupDefinition(7, new Multiplication(), 24),
            };
            return groups;
        }
    }
}

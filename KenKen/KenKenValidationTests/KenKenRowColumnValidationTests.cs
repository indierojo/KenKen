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
            Assert.False(validationResult.WasValid);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForDuplicateRowValues()
        {
            var validationResult = IsValidPuzzle(GetDupeRowPuzzle());
            Assert.False(validationResult.WasValid);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForDuplicateColumnValues()
        {
            var validationResult = IsValidPuzzle(GetDupeColumnPuzzle());
            Assert.False(validationResult.WasValid);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForLowValues()
        {
            var validationResult = IsValidPuzzle(GetLowNumberPuzzle());
            Assert.False(validationResult.WasValid);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldFailForHighValues()
        {
            var validationResult = IsValidPuzzle(GetHighNumberPuzzle());
            Assert.False(validationResult.WasValid);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldSucceedForValidValues()
        {
            var solvedPuzzle = GetSolvedPuzzle();
            var validationResult = IsValidPuzzle(solvedPuzzle);
            Assert.True(validationResult.WasValid, validationResult.FailureReason ?? "");
        }

        [Test]
        public void ValidationShouldFailForDuplicatedNumbersThatAddUpToExpectedTotals()
        {
            var validationResult = IsValidPuzzle(GetGoodTotalsWithDuplicatesPuzzle());
            Assert.False(validationResult.WasValid);
            Console.WriteLine(validationResult.FailureReason);
        }

        private ValidationResult IsValidPuzzle(Puzzle puzzle)
        {
            var rowColumnValidator = new RowAndColumnValidator();
            return rowColumnValidator.CheckForValidity(puzzle);
        }

        private Puzzle GetSolvedPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(4), CellWithValue(2), CellWithValue(1), CellWithValue(3)},
                new[]{CellWithValue(2), CellWithValue(4), CellWithValue(3), CellWithValue(1)},
                new[]{CellWithValue(3), CellWithValue(1), CellWithValue(4), CellWithValue(2)},
                new[]{CellWithValue(1), CellWithValue(3), CellWithValue(2), CellWithValue(4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetDupeRowPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(1), CellWithValue(3)},
                new[]{CellWithValue(2), CellWithValue(4), CellWithValue(3), CellWithValue(1)},
                new[]{CellWithValue(3), CellWithValue(1), CellWithValue(4), CellWithValue(2)},
                new[]{CellWithValue(1), CellWithValue(3), CellWithValue(2), CellWithValue(4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetDupeColumnPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(4), CellWithValue(2), CellWithValue(1), CellWithValue(3)},
                new[]{CellWithValue(2), CellWithValue(4), CellWithValue(3), CellWithValue(1)},
                new[]{CellWithValue(3), CellWithValue(2), CellWithValue(4), CellWithValue(2)},
                new[]{CellWithValue(1), CellWithValue(3), CellWithValue(2), CellWithValue(4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetLowNumberPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(0), CellWithValue(4), CellWithValue(4), CellWithValue(4)},
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(4), CellWithValue(4)},
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(4), CellWithValue(4)},
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(4), CellWithValue(4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetHighNumberPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(100), CellWithValue(4), CellWithValue(4), CellWithValue(4)},
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(4), CellWithValue(4)},
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(4), CellWithValue(4)},
                new[]{CellWithValue(4), CellWithValue(4), CellWithValue(4), CellWithValue(4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetGoodTotalsWithDuplicatesPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[] {CellWithValue(4), CellWithValue(2), CellWithValue(3), CellWithValue(1)},
                new[] {CellWithValue(4), CellWithValue(3), CellWithValue(1), CellWithValue(2)},
                new[] {CellWithValue(1), CellWithValue(4), CellWithValue(2), CellWithValue(3)},
                new[] {CellWithValue(1), CellWithValue(1), CellWithValue(4), CellWithValue(4)}
            };

            return new Puzzle(cells, groups);
        }

        private Cell CellWithValue(ushort value)
        {
            return new Cell(1, value);
        }

        private static IEnumerable<GroupDefinition> GetTestGroupDefinitions()
        {
            var groups = new List<GroupDefinition>
            {
                new GroupDefinition(1, OperationType.Addition, 40)
            };
            return groups;
        }

    }
}

using System;
using System.Collections.Generic;
using Domain;
using Domain.Operations;
using NUnit.Framework;
using PuzzleValidator;

namespace KenKenValidationTests
{
    [TestFixture]
    public class KenKenGroupValidationTests
    {
        [Test]
        public void ValidationShouldFailIfOperationDoesntHaveExpectedValue()
        {
            var validationResult = PuzzleRowColumnValidation(GetBadOperationPuzzle());
            Assert.True(validationResult.WasSuccess);
            validationResult = PuzzleGroupValidation(GetBadOperationPuzzle());

            Assert.False(validationResult.WasSuccess);
            Console.WriteLine(validationResult.FailureReason);
        }

        [Test]
        public void ValidationShouldSucceedForValidValues()
        {
            var solvedPuzzle = GetSolvedPuzzle();
            var validationResult = PuzzleGroupValidation(solvedPuzzle);
            Assert.True(validationResult.WasSuccess, validationResult.FailureReason ?? "");
        }

        private ValidationResult PuzzleRowColumnValidation(Puzzle puzzle)
        {
            var rowAndColumnValidator = new RowAndColumnValidator();
            return rowAndColumnValidator.CheckForValidity(puzzle);
        }

        private ValidationResult PuzzleGroupValidation(Puzzle puzzle)
        {
            var groupValidator = new GroupValidator();
            return groupValidator.CheckForValidity(puzzle);
        }

        private Puzzle GetSolvedPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(1, 4), CellWithValue(2, 2), CellWithValue(3, 1), CellWithValue(3, 3)},
                new[]{CellWithValue(4, 2), CellWithValue(2, 4), CellWithValue(3, 3), CellWithValue(5, 1)},
                new[]{CellWithValue(4, 3), CellWithValue(6, 1), CellWithValue(6, 4), CellWithValue(5, 2)},
                new[]{CellWithValue(4, 1), CellWithValue(7, 3), CellWithValue(7, 2), CellWithValue(7, 4)}
            };

            return new Puzzle(cells, groups);
        }

        private Puzzle GetBadOperationPuzzle()
        {
            var groups = GetTestGroupDefinitions();

            var cells = new[]
            {
                new[]{CellWithValue(1, 1), CellWithValue(2, 2), CellWithValue(3, 3), CellWithValue(3, 4)},
                new[]{CellWithValue(4, 2), CellWithValue(2, 3), CellWithValue(3, 4), CellWithValue(5, 1)},
                new[]{CellWithValue(4, 3), CellWithValue(6, 4), CellWithValue(6, 1), CellWithValue(5, 2)},
                new[]{CellWithValue(4, 4), CellWithValue(7, 1), CellWithValue(7, 2), CellWithValue(7, 3)}
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

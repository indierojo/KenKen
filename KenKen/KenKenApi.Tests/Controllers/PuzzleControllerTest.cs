using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KenKenApi.Controllers;

namespace KenKenApi.Tests.Controllers
{
    [TestClass]
    public class PuzzleControllerTest
    {
        private static readonly int SmallTestPuzzleId = -124;
        
        [TestMethod]
        public void GetById()
        {
            // Arrange
            var controller = new PuzzleController();

            // Act
            Puzzle result = controller.Get(SmallTestPuzzleId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.GetGridDimensions());
            Assert.AreEqual(9, result.Groups.Count());
        }

        [TestMethod]
        public void Check()
        {
            // Arrange
            var controller = new PuzzleController();

            var groups = new List<GroupDefinition>
            {
                new GroupDefinition(1, OperationType.NoOp, 1),
                new GroupDefinition(2, OperationType.NoOp, 2),
                new GroupDefinition(3, OperationType.NoOp, 3),
                new GroupDefinition(4, OperationType.NoOp, 2),
                new GroupDefinition(5, OperationType.NoOp, 3),
                new GroupDefinition(6, OperationType.NoOp, 1),
                new GroupDefinition(7, OperationType.NoOp, 3),
                new GroupDefinition(8, OperationType.NoOp, 1),
                new GroupDefinition(9, OperationType.NoOp, 2)
            };

            var correctSolution = new[]
            {
                new[] {new Cell(1, 1), new Cell(2, 2), new Cell(3, 3)},
                new[] {new Cell(4, 2), new Cell(5, 3), new Cell(6, 1)},
                new[] {new Cell(7, 3), new Cell(8, 1), new Cell(9, 2)}
            };
            var incorrectSolution = new[]
            {
                new[] {new Cell(1, 3), new Cell(2, 2), new Cell(3, 3)},
                new[] {new Cell(4, 2), new Cell(5, 3), new Cell(6, 1)},
                new[] {new Cell(7, 3), new Cell(8, 1), new Cell(9, 2)}
            };

            // Act
            var correctSolutionValidationResult = controller.Check(new Puzzle(correctSolution, groups));
            var incorrectSolutionValidationResult = controller.Check(new Puzzle(incorrectSolution, groups));

            // Assert
            Assert.AreEqual(true, correctSolutionValidationResult.WasValid);
            Assert.AreEqual(false, incorrectSolutionValidationResult.WasValid);
            Assert.IsNotNull(incorrectSolutionValidationResult.FailureReason);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Operations;
using MathNet.Numerics.Distributions;

namespace KenKenBuilder
{
    public class KenKenPuzzleBuilder : IKenKenPuzzleBuilder
    {
        private static Normal _normalDistribution;

        public Puzzle Build(DifficultyLevel difficultyLevel, GridSize gridSize)
        {
            _normalDistribution = CreateDistribution((int) gridSize);
            var board = GetInitialBoard(gridSize);
            Permute(board, gridSize);
            var groups = DrawGroups(gridSize, difficultyLevel);
            var grid = new Cell[(int) gridSize][];

            ushort groupNumber = 1;
            var groupDefinitions = groups.Select(cellGroup => BuildGroupDef(groupNumber++, difficultyLevel, gridSize, cellGroup, grid, board));

            return new Puzzle(grid, groupDefinitions);
        }

        private GroupDefinition BuildGroupDef(ushort groupNumber, DifficultyLevel difficultyLevel, GridSize gridSize, IEnumerable<Cell> cellGroup, Cell[][] grid,
            int[,] board)
        {
            var cellValues = new List<ushort>();
            foreach (var cell in cellGroup)
            {
                cell.Group = groupNumber;
                var row = grid[cell.X];
                if (row == null)
                {
                    grid[cell.X] = new Cell[(int) gridSize];
                }
                grid[cell.X][cell.Y] = cell;
                cellValues.Add( (ushort) board[cell.X,cell.Y]);
            }
            var operation = PickOperation(cellValues, board, difficultyLevel);
            var expectedTotal = operation.ApplyOperationTo(cellValues);
            return new GroupDefinition(groupNumber, operation.Type, (ushort) expectedTotal);
        }

        private IOperation PickOperation(List<ushort> cellValues, int[,] board, DifficultyLevel difficultyLevel)
        {
            var choices = GetPossibleOperations(cellValues, board);
            //TODO pick based on difficulty
            var randomizer = new ThreadSafeRandom();
            var randomizedChoices = choices.OrderBy(a => randomizer.Next());
            return randomizedChoices.First();
        }

        private IEnumerable<IOperation> GetPossibleOperations(List<ushort> cellValues, int[,] board)
        {
            if (cellValues.Count == 1)
            {
                return new List<IOperation> {new NoOp() };
            }

            var possibleOperations = new List<IOperation> { new Addition(), new Multiplication() };
            if (cellValues.Count == 2)
            {
                possibleOperations.Add(new Subtraction());
                if (AreEvenlyDivisible(cellValues[0], cellValues[1]))
                {
                    possibleOperations.Add(new Division());
                }
            }
            return possibleOperations;
        }

        private bool AreEvenlyDivisible(ushort value, ushort value1)
        {
            if (value > value1)
            {
                return value%value1 == 0;
            }

            return (value1%value) == 0;
        }

        private Normal CreateDistribution(int boardSize)
        {
            var size = (double)boardSize;
            var mean = Math.Ceiling(size/2);
            var stdDev = Math.Sqrt(Math.Floor(size/3));
            return new Normal(mean, stdDev);
        }

        private static IEnumerable<List<Cell>> DrawGroups(GridSize gridSize, DifficultyLevel difficultyLevel)
        {
            var board = new bool[(int) gridSize, (int) gridSize];
            var groups = new List<List<Cell>>();
            Cell cell;
            while ((cell = GetNextCell(board)) != null) 
            {
                var cellGroup = new List<Cell>();
                AddCellToGroup(cell, cellGroup, board);
                Cell nextCell;
                while (ShouldGrow(cellGroup) && ( nextCell = PickAdjacentOpenCell(cellGroup, board) ) != null)
                {
                    AddCellToGroup(nextCell, cellGroup, board);
                }
                groups.Add(cellGroup);
            }
            return groups;
        }

        private static void AddCellToGroup(Cell nextCell, ICollection<Cell> cellGroup, bool[,] board)
        {
            cellGroup.Add(nextCell);
            board[nextCell.X, nextCell.Y] = true;
        }

        private static Cell PickAdjacentOpenCell(IEnumerable<Cell> cellGroup, bool[,] board)
        {
            var adjacentCells = GetAdjacentCells(cellGroup, board.GetLength(0));
            var openCells = GetOpenCells(adjacentCells, board);
            var randomizer = new ThreadSafeRandom();
            var randomCells = openCells.OrderBy(a => randomizer.Next());
            return randomCells.FirstOrDefault();
        }

        private static IEnumerable<Cell> GetOpenCells(IEnumerable<Cell> adjacentCells, bool[,] board)
        {
            return adjacentCells.Where(a => OpenCell(board, a));
        }

        private static bool OpenCell(bool[,] board, Cell a)
        {
            return ! board[a.X, a.Y];
        }

        private static IEnumerable<Cell> GetAdjacentCells(IEnumerable<Cell> cellGroup, int getLength)
        {
// ReSharper disable PossibleMultipleEnumeration
            var cells = cellGroup.SelectMany(cell => new HashSet<Cell>
            {
                new Cell(cell.X - 1, cell.Y), new Cell(cell.X + 1, cell.Y), new Cell(cell.X, cell.Y - 1), new Cell(cell.X, cell.Y + 1)
            });     
            return cells.Where(cell => IsInBounds(cell, getLength) && ! cellGroup.Contains(cell));
// ReSharper restore PossibleMultipleEnumeration
        }

        private static bool IsInBounds(Cell cell, int boardSize)
        {
            return cell.X >= 0 && cell.Y >= 0 && cell.X < boardSize && cell.Y < boardSize;
        }

        private static bool ShouldGrow(IReadOnlyCollection<Cell> cellGroup)
        {
            if (cellGroup.Count == 1)
            {
                return true;
            }

            var random = _normalDistribution.Sample();
            if (random < 1)
            {
                random = 1;
            }
            return cellGroup.Count < random;
        }

        private static Cell GetNextCell(bool[,] board)
        {
            for (var x = 0; x < board.GetLength(0); x++)
            {
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    if (! board[x, y])
                    {
                        return new Cell(x,y);
                    }
                }
            }

            return null;
        }

        private static void Permute(int[,] board, GridSize gridSize)
        {
            var randomizer = new ThreadSafeRandom();
            var numOperations = randomizer.NextDouble()*100;

            for (var i = 0; i < numOperations; i++)
            {
                var flipX = randomizer.NextDouble() >= 0.5;
                var choice1 = randomizer.Next(0, (int) gridSize);
                var choice2 = choice1;
                while (choice2 == choice1)
                {
                    choice2 = randomizer.Next(0, (int) gridSize);
                }
                if (flipX)
                {
                    FlipX(choice1, choice2, board);
                }
                else
                {
                    FlipY(choice1, choice2, board);
                }
            }
        }

        private static void FlipX(int choice1, int choice2,  int[,] board)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                var val = board[choice1, i];
                board[choice1, i] = board[choice2, i];
                board[choice2, i] = val;
            }
        }

        private static void FlipY(int choice1, int choice2, int[,] board)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                var val = board[i,choice1];
                board[i, choice1] = board[i, choice2];
                board[i, choice2] = val;
            }
        }

        private static int[,] GetInitialBoard(GridSize gridSize)
        {
            var result = new int[(int) gridSize, (int) gridSize];
            for (var row = 0; row < (int) gridSize; row++)
            {
                for (var col = 0; col < (int) gridSize; col++)
                {
                    var i = ( (col + row )%(int) gridSize ) + 1;
                    Console.Write(i + " ");
                    result[row, col] = i;
                }
                Console.WriteLine();
            }
            return result;
        }
    }
}
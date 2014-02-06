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
        private static readonly Random RandomSeed = new Random();
        private static Normal _normalDistribution;

        public Puzzle Build(DifficultyLevel difficultyLevel, GridSize gridSize)
        {
            _normalDistribution = CreateDistribution((int) gridSize);
            var board = GetInitialBoard(gridSize);
            Permute(board, gridSize);
            var groups = DrawGroups(gridSize, difficultyLevel);
            var grid = new Cell[(int) gridSize][];

            ushort groupNumber = 1;
            var groupDefinitions = groups.Select(cellGroup =>
            {
                return BuildGroupDef(groupNumber++, difficultyLevel, gridSize, cellGroup, grid, board);
            } );

            return new Puzzle(grid, groupDefinitions);
        }

        private GroupDefinition BuildGroupDef(ushort groupNumber, DifficultyLevel difficultyLevel, GridSize gridSize, List<Cell> cellGroup, Cell[][] grid,
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
            var operation = PickOperation(cellGroup, board, difficultyLevel);
            var expectedTotal = operation.ApplyOperationTo(cellValues);
            return new GroupDefinition(groupNumber, operation.Type, (ushort) expectedTotal);
        }

        private IOperation PickOperation(List<Cell> cells, int[,] board, DifficultyLevel difficultyLevel)
        {
            var choices = GetPossibleOperations(cells, board);
            //TODO pick based on difficulty
            var randomizedChoices = choices.OrderBy(a => RandomSeed.Next());
            return randomizedChoices.First();
        }

        private IEnumerable<IOperation> GetPossibleOperations(List<Cell> cells, int[,] board)
        {
            if (cells.Count == 1)
            {
                return new List<IOperation> {new NoOp() };
            }

            var possibleOperations = new List<IOperation> { new Addition(), new Multiplication() };
            if (cells.Count == 2 )
            {
                possibleOperations.Add(new Subtraction());
                possibleOperations.Add(new Division());
            }
            return possibleOperations;
        }

        private Normal CreateDistribution(int boardSize)
        {
            // 3 => 2
            // 6 => 3
            // 9 => 4
            var mean = Math.Ceiling((double) (boardSize/2));
//            var mean = 4;
            
            // 3 => 1
            // 6 => 2
            // 9 => 3
            var stdDev = Math.Sqrt(Math.Floor((double)(boardSize / 3)));
//            var stdDev = 1;
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
            var randomCells = openCells.OrderBy(a => RandomSeed.Next());
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
            var numOperations = RandomSeed.NextDouble()*100;

            for (var i = 0; i < numOperations; i++)
            {
                var flipX = RandomSeed.NextDouble() >= 0.5;
                var choice1 = RandomSeed.Next(0, (int) gridSize);
                var choice2 = choice1;
                while (choice2 == choice1)
                {
                    choice2 = RandomSeed.Next(0, (int) gridSize);
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
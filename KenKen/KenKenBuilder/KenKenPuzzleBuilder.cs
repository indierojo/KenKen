using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
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
//            var board = GetInitialBoard(gridSize);
//            Permute(board, gridSize);
            var groups = DrawGroups(gridSize, difficultyLevel);
            var grid = new Cell[(int) gridSize][];
            //pick group operators
            var groupDefinitions = groups.Select(cellGroup =>
            {
                var groupDef = new GroupDefinition();
                foreach (var cell in cellGroup)
                {
                    cell.Group = groupDef.Group;
                    var row = grid[cell.X];
                    if (row == null)
                    {
                        grid[cell.X] = new Cell[(int) gridSize];
                    }
                    grid[cell.X][cell.Y] = cell;
                }
                return groupDef;
            } );

            return new Puzzle(grid, groupDefinitions);
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

        private static void Permute(object board, GridSize gridSize)
        {
            throw new System.NotImplementedException();
        }

        private static object GetInitialBoard(GridSize gridSize)
        {
            throw new System.NotImplementedException();
        }
    }
}
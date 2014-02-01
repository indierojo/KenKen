﻿using System.Linq;
using Domain;

namespace PuzzleValidator
{
    public class GroupValidator : IPuzzleValidator
    {
        public ValidationResult CheckForValidity(Puzzle puzzle)
        {
            var allCells = puzzle.Grid.Cells.SelectMany(x => x).ToList();

            var groupedCells = allCells.GroupBy(c => c.Group);
            var groupLookup = puzzle.Groups.ToDictionary(g => g.Group);
            foreach (var cellsInGroup in groupedCells)
            {
                var group = groupLookup[cellsInGroup.Key];

                // ReSharper disable once PossibleInvalidOperationException -- If any is null, should have returned false by now.
                var allCellValuesInGroup = cellsInGroup.Select(x => x.Value.Value);

                var groupValueAfterOperation = group.Operation.DoOperationOn(allCellValuesInGroup);
                var expectedTotal = group.ExpectedTotal;
                if (groupValueAfterOperation != expectedTotal)
                {
                    return ValidationResult.Invalid(string.Format("Expected group {0} total value of {1}, but got {2}", group, expectedTotal, groupValueAfterOperation));
                }
            }
            return ValidationResult.Valid();
        }
    }
}

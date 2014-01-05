using System.Globalization;
using Domain;

namespace PuzzleValidator
{
    public class RowAndColumnValidator : IPuzzleValidator
    {
        private const uint MinValue = 1;

        public ValidationResult CheckForValidity(Puzzle puzzle)
        {
            var gridDimensions = puzzle.GetGridDimensions();

            // Expected total for both rows and columns is the sum of all expected values: IE: 1+2+3+4 = 10
            var expectedTotal = GetExpectedRowAndColumnTotal(gridDimensions);

            for (var x = 0u; x < gridDimensions; x++)
            {
                var rowTotal = 0;
                var columnTotal = 0;
                var cells = puzzle.Grid.Cells;

                for (var y = 0u; y < gridDimensions; y++)
                {
                    var rowCell = cells[x][y].Value;
                    var columnCell = cells[y][x].Value;
                    if (!IsValidValue(rowCell, gridDimensions))
                    {
                        return ValidationResult.Invalid(string.Format("Cell at {0},{1} has an invalid value: {2}", x + 1, y + 1, rowCell.HasValue ? rowCell.Value.ToString(CultureInfo.InvariantCulture) : "MISSING"));
                    }
                    if (!IsValidValue(columnCell, gridDimensions))
                    {
                        return ValidationResult.Invalid(string.Format("Cell at {0},{1} has an invalid value: {2}", y + 1, x + 1, columnCell.HasValue ? columnCell.Value.ToString(CultureInfo.InvariantCulture) : "MISSING"));
                    }

                    // ReSharper disable once PossibleInvalidOperationException
                    rowTotal += rowCell.Value;
                    // ReSharper disable once PossibleInvalidOperationException
                    columnTotal += columnCell.Value;
                }

                if (rowTotal != expectedTotal)
                {
                    return ValidationResult.Invalid(string.Format("Row {0} has a duplicate number", x + 1));
                }

                if (columnTotal != expectedTotal)
                {
                    return ValidationResult.Invalid(string.Format("Column {0} has a duplicate number", x + 1));
                }
            }
            return ValidationResult.Valid();
        }

        private bool IsValidValue(ushort? value, ushort gridDimensions)
        {
            return value.HasValue && value.Value >= MinValue && value.Value <= gridDimensions;
        }
    
        private uint GetExpectedRowAndColumnTotal(ushort gridDimensions)
        {
            var total = 0u;

            var number = gridDimensions;
            while (number > 0)
            {
                total += number;
                number--;
            }

            return total;
        }
    }
}

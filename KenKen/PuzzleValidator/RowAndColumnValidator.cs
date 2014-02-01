using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain;

namespace PuzzleValidator
{
    public class RowAndColumnValidator : IPuzzleValidator
    {
        private const uint MinValue = 1;

        public ValidationResult CheckForValidity2(Puzzle puzzle)
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

        public ValidationResult CheckForValidity(Puzzle puzzle)
        {
            var gridDimensions = puzzle.GetGridDimensions();

//            var colCheck = new bool[gridDimensions, gridDimensions];
//            var rowCheck = new bool[gridDimensions, gridDimensions];
//
//            var cells = puzzle.Grid.Cells;
//            for (var x = 0u; x < gridDimensions; x++)
//            {
//                var row = cells[x];
//                var column = cells.Select(r => r[x]);
//
//                var rowValues = row.Select(c => c.Value).ToList();
//                var columnValues = column.Select(c => c.Value).ToList();
//
//                var getInvalidValues = new Func<List<ushort?>, List<ushort?>>(array => array.Where(v => !v.HasValue || v < 1 || v > gridDimensions).ToList());
//                if(getInvalidValues(rowValues).Any() || getInvalidValues(columnValues).Any())
//                {
//                    return ValidationResult.Invalid(string.Format("Puzzle has missing or invalid values!"));
//                }
//
//                var getNumberOfUniqueValues = new Func<List<ushort?>, int>(valueList => valueList.Select(v => v.Value).Distinct().Count());
//                if (getNumberOfUniqueValues(rowValues) < gridDimensions || getNumberOfUniqueValues(columnValues) < gridDimensions)
//                {
//                    return ValidationResult.Invalid("Puzzle has duplicates.");
//                }
//            }

            var expectedValues = Enumerable.Range(1, gridDimensions).OrderBy(x => x);
            var cells = puzzle.Grid.Cells;

            var errors = new List<String>();
            for (var x = 0u; x < gridDimensions; x++)
            {
                var rowColumnNumber = x + 1;
                var rowCells = cells[x];
                var columnCells = cells.Select(r => r[rowColumnNumber - 1]);

                var getNormalizedList = new Func<IEnumerable<Cell>, IOrderedEnumerable<int>>(cellList => cellList.Select(c => (int) (c.Value ?? ushort.MaxValue)).ToList().OrderBy(v => v));
                var rowValues = getNormalizedList(rowCells);
                var columnValues = getNormalizedList(columnCells);

                var listValuesEqualExpected = new Func<IOrderedEnumerable<int>, bool>(listValues =>
                {
                    if (listValues.Count() != expectedValues.Count())
                    {
                        return false;
                    }

                    return listValues.Aggregate("", (a, b) => a + " " + b).Equals(expectedValues.Aggregate("", (a, b) => a + " " + b));
                });

                if (listValuesEqualExpected(rowValues) && listValuesEqualExpected(columnValues))
                {
                    continue;
                }

                var missingRowValues = expectedValues.Except(rowValues).ToList();
                var missingColumnValues = expectedValues.Except(columnValues).ToList();

                var invalidRowValues = rowValues.Except(expectedValues).ToList();
                var invalidColumnValues = columnValues.Except(expectedValues).ToList();

                if (missingRowValues.Any())
                {
                    errors.Add(string.Format("Missing {0} from row {1}", String.Join(", ", missingRowValues), rowColumnNumber));
                }
                if (missingColumnValues.Any())
                {
                    errors.Add(string.Format("Missing {0} from column {1}", String.Join(", ", missingColumnValues), rowColumnNumber));
                }
                if (invalidRowValues.Any())
                {
                    var invalidValuesExceptMissing = invalidRowValues.Where(v => !v.Equals(ushort.MaxValue)).ToList();
                    var numMissing = rowValues.Count(v => v.Equals(ushort.MaxValue));

                    if (numMissing > 0)
                    {
                        errors.Add(string.Format("Row {0} is missing {1} value(s)", rowColumnNumber, numMissing));
                    }
                    if (invalidValuesExceptMissing.Any())
                    {
                        errors.Add(string.Format("Row {0} has invalid value(s): {1}", rowColumnNumber, String.Join(", ", invalidValuesExceptMissing)));
                    }
                }
                if (invalidColumnValues.Any())
                {
                    var invalidValuesExceptMissing = invalidColumnValues.Where(v => !v.Equals(ushort.MaxValue)).ToList();
                    var numMissing = columnValues.Count(v => v.Equals(ushort.MaxValue));

                    if (numMissing > 0)
                    {
                        errors.Add(string.Format("Column {0} is missing {1} value(s)", rowColumnNumber, numMissing));
                    }
                    if (invalidValuesExceptMissing.Any())
                    {
                        errors.Add(string.Format("Column {0} has invalid value(s): {1}", rowColumnNumber, String.Join(", ", invalidValuesExceptMissing)));
                    }
                }
            }

            if (errors.Any())
            {
                return ValidationResult.Invalid(string.Join(",\n", errors));
            }

//            var allValues = puzzle.Grid.Cells.SelectMany(x => x).Select(x => x.Value ?? ushort.MaxValue).ToList();
//            var expectedValues = Enumerable.Range(1, gridDimensions).OrderBy(x => x);
//            var errors = new List<String>();
//            foreach (var expectedValue in expectedValues)
//            {
//                var numberOfValue = allValues.Count(v => ((int)v).Equals(expectedValue));
//                if (numberOfValue != gridDimensions)
//                {
//                    errors.Add(string.Format("Expected {0} {1}s but found {2}", gridDimensions, expectedValue, numberOfValue));
//                }
//            }
//            var numberOfMissingValues = allValues.Count(x => x.Equals(ushort.MaxValue));
//            if (numberOfMissingValues > 0)
//            {
//                errors.Add(string.Format("Found {0} missing value(s).", numberOfMissingValues));
//            }
//            var invalidValues = allValues.Where(v => !v.Equals(ushort.MaxValue) && !expectedValues.Contains(v)).ToList();
//            if (invalidValues.Any())
//            {
//                errors.Add(string.Format("Found {0} invalid value(s): {1}", invalidValues.Count, invalidValues.Select(x => x.ToString(CultureInfo.InvariantCulture)).Aggregate((a, b) => a + ", " + b)));
//            }
//
//            if (errors.Any())
//            {
//                return ValidationResult.Invalid(string.Join(",\n", errors));
//            }

//            for (var x = 0u; x < gridDimensions; x++)
//            {
//                for (var y = 0u; y < gridDimensions; y++)
//                {
//                    var cellValue = cells[x][y].Value;
//                    if (!cellValue.HasValue)
//                    {
//                        return
//                            ValidationResult.Invalid(string.Format("Row {0}, col {1} is missing a value", y + 1, x + 1));
//                    }
//                    var cell = cellValue.Value;
//
//                    if (cell > gridDimensions || cell < 1)
//                    {
//                        return
//                            ValidationResult.Invalid(string.Format("Row {0}, col {1} has an illegal value", y + 1, x + 1));
//                    }
//
//                    var cellIndex = cell - 1;
//
//                    rowCheck.SetValue(true, x, cellIndex);
//                    colCheck.SetValue(true, y, cellIndex);
//                }
//            }
//
//            for (var rowCol = 0u; rowCol < gridDimensions; rowCol++)
//            {
//                for (var valueIndex = 0u; valueIndex < gridDimensions; valueIndex++)
//                {
//                    if (! (rowCheck[rowCol,valueIndex] && colCheck[rowCol,valueIndex]))
//                    {
//                        return
//                            ValidationResult.Invalid(string.Format("Missing the number {0} in row or column {1}", valueIndex + 1,
//                                rowCol + 1));
//                    }  
//                }
//            }
 
            return ValidationResult.Valid();
        }
    }
}

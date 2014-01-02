using System.Collections.Generic;
using Domain;

namespace PuzzleValidator
{
    public static class PuzzleValidationService
    {
        private static readonly List<IPuzzleValidator> Validators = new List<IPuzzleValidator> {new RowAndColumnValidator(), new GroupValidator()};

        public static ValidationResult CheckForValidity(Puzzle puzzle)
        {
            foreach (var puzzleValidator in Validators)
            {
                var validationResult = puzzleValidator.CheckForValidity(puzzle);
                if (!validationResult.WasSuccess)
                {
                    return validationResult;
                }
            }

            return ValidationResult.Valid();
        }
    }
}

namespace PuzzleValidator
{
    public class ValidationResult
    {
        private ValidationResult(bool wasSuccess, string failureReason = null)
        {
            WasSuccess = wasSuccess;
            FailureReason = failureReason;
        }

        public static ValidationResult Valid()
        {
            return new ValidationResult(true);
        }

        public static ValidationResult Invalid(string reason)
        {
            return new ValidationResult(false, reason);
        }

        public bool WasSuccess { get; private set; }
        public string FailureReason { get; private set; }
    }
}

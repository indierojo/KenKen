namespace PuzzleValidator
{
    public class ValidationResult
    {
        private ValidationResult(bool wasValid, string failureReason = null)
        {
            WasValid = wasValid;
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

        public bool WasValid { get; private set; }
        public string FailureReason { get; private set; }
    }
}

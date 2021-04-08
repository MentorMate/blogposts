namespace ValidationExample.ValidationPattern.Validation
{
    /// <summary>The validation result.</summary>
    public class ValidationResult : ValidationError
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
        public ValidationResult(ValidationError error)
            : base(error?.Code, error?.Message)
        {
            IsValid = error == null;
        }

        /// <summary>Gets a value indicating whether this <see cref="ValidationResult"/> is valid.</summary>
        public bool IsValid { get; }
    }
}

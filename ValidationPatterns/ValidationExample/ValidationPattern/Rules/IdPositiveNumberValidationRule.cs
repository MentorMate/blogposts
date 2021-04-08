using ValidationExample.ValidationPattern.Validation;

namespace ValidationExample.ValidationPattern.Rules
{
    /// <summary>Identifier are positive number.</summary>
    public class IdPositiveNumberValidationRule : IValidationRule<int>
    {
        /// <inheritdoc/>
        public ValidationError Error => new ValidationError("MY001", "Entity identifier need to be positive number.");

        /// <inheritdoc/>
        public bool Validate(int model) => model > 0;
    }
}

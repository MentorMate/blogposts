using ValidationExample.ValidationPattern.Validation;

namespace ValidationExample.ValidationPattern.Rules
{
    public class UsernameValidationRule : IValidationRule<string>
    {
        /// <inheritdoc/>
        public ValidationError Error => new ValidationError("MY002", "Username should be bigger than 3 chars.");

        /// <inheritdoc/>
        public bool Validate(string model) => model.Length > 3;
    }
}

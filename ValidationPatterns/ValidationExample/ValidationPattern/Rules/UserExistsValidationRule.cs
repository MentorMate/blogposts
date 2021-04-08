using System;

using ValidationExample.ValidationPattern.Validation;

namespace ValidationExample.ValidationPattern.Rules
{
    public class UserExistsValidationRule : IValidationRule<User>
    {
        private readonly Func<int, User> _getUserById;

        /// <summary>Initializes a new instance of the <see cref="UserExistsValidationRule"/> class.</summary>
        public UserExistsValidationRule(Func<int, User> getUserById)
        {
            _getUserById = getUserById;
        }

        /// <inheritdoc/>
        public ValidationError Error => new ValidationError("002", "User do not exist!");

        /// <inheritdoc/>
        public bool Validate(User model) => _getUserById(model.UserId) != null;
    }
}

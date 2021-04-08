using System;

using ValidationExample.ValidationPattern.Rules;
using ValidationExample.ValidationPattern.Validation;

namespace ValidationExample.ValidationPattern.Services
{
    public class UserService
    {
        private Func<int, User> _getUserById = (int userId) => userId == 10 ? new User() : null;

        public void Delete(int userId)
        {
            Console.WriteLine("Delete user " + userId.ToString());
            var result = new Validator<User>()
                .AddRule(new UserExistsValidationRule(_getUserById))
                .Validate(new User { UserId = userId });

            if (result.IsValid)
            {
                Console.WriteLine("Deleted");
            }

            Console.WriteLine($"Valid: {result.IsValid}; {result.Code} {result.Message}");
            Console.WriteLine();
        }

        public void Update(User user)
        {
            Console.WriteLine("Update user " + user.UserId.ToString());
            var result = new Validator<User>()
                .AddRule<IdPositiveNumberValidationRule, int>(it => it.UserId)
                .AddRule(new UserExistsValidationRule(_getUserById))
                .AddRules(it => it.Username, typeof(UsernameValidationRule))
                .Validate(user);

            if (result.IsValid)
            {
                Console.WriteLine("Updated");
            }

            Console.WriteLine($"Valid: {result.IsValid}; {result.Code} {result.Message}");
            Console.WriteLine();
        }
    }
}

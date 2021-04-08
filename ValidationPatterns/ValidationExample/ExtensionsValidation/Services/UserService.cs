using System;

using ValidationExample.ExtensionsValidation.Results;

namespace ValidationExample.ExtensionsValidation.Services
{
    public class UserService
	{
		private Func<int, User> _getUserById = (int userId) => userId == 10 ? new User() : null;

		public void Delete(int userId)
		{
			Console.WriteLine("Delete user " + userId.ToString());
			var result = Result
				.Validate(_getUserById(userId) != null, ResultCompleteTypes.InvalidArgument, "User do not exists")
				.Map(valid =>
				{
					Console.WriteLine("Deleted");
					return true;
				});

			Console.WriteLine(ToMsg(result));
			Console.WriteLine();
		}

		public void Update(User user)
		{
			Console.WriteLine("Update user " + user.UserId.ToString());
			var result = Result
				.Validate(user.UserId > 0, ResultCompleteTypes.InvalidArgument, "Number should be positive number.")
				.Validate(_getUserById(user.UserId) != null, ResultCompleteTypes.InvalidArgument, "User do not exists.")
				.Validate(user.Username.Length > 3, ResultCompleteTypes.InvalidArgument, "Name should be more than 3 chars.")
				.Map(valid =>
				{
					Console.WriteLine("Updated");
					return true;
				});

			Console.WriteLine(ToMsg(result));
			Console.WriteLine();
		}

		private static string ToMsg<T>(Result<T> result) =>
			$"Valid: {result.IsSuccessfulStatus()}; {result.Status} {string.Join(",", result.Messages ?? new string[0])}";
	}
}

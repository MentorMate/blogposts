using System;

using ExtensionsUserService = ValidationExample.ExtensionsValidation.Services.UserService;
using PatternUserService = ValidationExample.ValidationPattern.Services.UserService;

namespace ValidationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestValidationPattern();
            TestExtensionsPattern();
        }

        public static void TestValidationPattern()
        {
            Console.WriteLine("Validation Pattern");
            new PatternUserService().Delete(10);
            new PatternUserService().Delete(0);
            new PatternUserService().Update(new User { UserId = 10, Username = "User Name" });
            new PatternUserService().Update(new User { UserId = 10, Username = "Us" });
        }

        public static void TestExtensionsPattern()
        {
            Console.WriteLine("Extensions Pattern");
            new ExtensionsUserService().Delete(10);
            new ExtensionsUserService().Delete(0);
            new ExtensionsUserService().Update(new User { UserId = 10, Username = "User Name" }); ;
            new ExtensionsUserService().Update(new User { UserId = 10, Username = "Us" });
        }
    }
}

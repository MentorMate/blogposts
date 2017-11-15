using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    internal static class ControllerExtensions
    {
        public static T As<T>(this IActionResult result) where T : ActionResult
        {
            Assert.IsInstanceOfType(result, typeof(T), "The action result can not be parsed.");
            return result as T;
        }

        public static ObjectResult ShouldReturnValue(this ObjectResult result, object expected)
        {
            Assert.AreEqual(expected, result.Value);
            return result;
        }

        public static StatusCodeResult ShouldHaveStatusCode(this StatusCodeResult result, int? code)
        {
            Assert.AreEqual(result.StatusCode, code);
            return result;
        }

        public static ObjectResult ShouldHaveStatusCode(this ObjectResult result, int? code)
        {
            Assert.AreEqual(result.StatusCode, code);
            return result;
        }
    }
}

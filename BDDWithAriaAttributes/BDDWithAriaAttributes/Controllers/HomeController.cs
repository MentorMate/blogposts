using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnTheBoard.Web.Controllers
{
    /// <summary>The application default MVC controller.</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        [Authorize]
        /// <summary>The application default page..</summary>
        public IActionResult Index() =>
            View();
    }
}
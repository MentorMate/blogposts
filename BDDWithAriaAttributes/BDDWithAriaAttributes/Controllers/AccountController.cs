using System.Security.Claims;
using System.Threading.Tasks;
using BDDWithAriaAttributes.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace OnTheBoard.Web.Controllers
{
    /// <summary>The application account MVC controller.</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AccountController : Controller
    {
        /// <summary>The application default page..</summary>
        public IActionResult Login(string returnUrl = "") =>
            View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == "myname@mymail.com" &&
                    model.Password == "mypassword") 
                {
                    await HttpContext.SignInAsync(
                        new ClaimsPrincipal(
                            new ClaimsIdentity(new[] {
                                new Claim(ClaimTypes.Name, "MyName"),
                                new Claim(ClaimTypes.Email, "myname@mymail.com")
                            },
                            CookieAuthenticationDefaults.AuthenticationScheme)));

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Can't login! Wrong email or password.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
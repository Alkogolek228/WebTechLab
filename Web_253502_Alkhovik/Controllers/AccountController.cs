using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Services.Authentication;
using Web_253502_Alkhovik.Models;

namespace Web_253502_Alkhovik.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel user, [FromServices] IAuthService authService)
        {
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    return BadRequest("Данные пользователя не предоставлены.");
                }

                var result = await authService.RegisterUserAsync(user.Email, user.Password, user.Avatar);

                if (result.Result)
                {
                    var redirectUrl = Url.Action("Index", "Home");
                    return Redirect(redirectUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, [FromServices] IAuthService authService)
        {
            await HttpContext.ChallengeAsync(
                    OpenIdConnectDefaults.AuthenticationScheme,
                    new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") });

            return View(loginViewModel);
        }

        [HttpPost]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") });
        }

    }
}
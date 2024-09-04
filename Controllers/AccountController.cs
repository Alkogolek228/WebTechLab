using Microsoft.AspNetCore.Mvc;

namespace Web_253502_Alkhovik.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Logout()
        {
            return View();
        }
    }
}
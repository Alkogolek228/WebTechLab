using Microsoft.AspNetCore.Mvc;

namespace Web_253502_Alkhovik.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
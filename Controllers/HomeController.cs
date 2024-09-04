using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Models;

namespace Web_253502_Alkhovik.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

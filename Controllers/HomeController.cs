using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_253502_Alkhovik.Models;
using System.Collections.Generic;

namespace Web_253502_Alkhovik.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var listItems = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Item 1" },
                new ListDemo { Id = 2, Name = "Item 2" },
                new ListDemo { Id = 3, Name = "Item 3" }
            };

            var selectList = new SelectList(listItems, "Id", "Name");
            ViewBag.SelectList = selectList;

            return View();
        }
    }
}
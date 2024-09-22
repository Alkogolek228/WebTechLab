using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Models;
using Web_253502_Alkhovik.Helpers;

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

            var viewModel = new IndexViewModel
            {
                ListItems = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(listItems, "Id", "Name")
            };

            return View(viewModel);
        }
    }
}
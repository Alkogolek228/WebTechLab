using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Models;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Extensions;

namespace Web_253502_Alkhovik.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICarService _carService;

        public ProductController(ICarService carService, ICategoryService categoryService)
        {
            _carService = carService;
            _categoryService = categoryService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var productResponse = await _carService.GetProductListAsync(category, pageNo);
            if (!productResponse.Successfull)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            var allCategories = await _categoryService.GetCategoryListAsync();
            if (!allCategories.Successfull)
            {
                return NotFound(allCategories.ErrorMessage);
            }

            var currentCategory = category == null ? "Все" : allCategories.Data!.FirstOrDefault(c => c.NormalizedName == category)?.Name;
            ViewData["Categories"] = allCategories.Data;
            ViewData["CurrentCategory"] = currentCategory;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PagerAndCardsPartial", new
                {
                    CurrentCategory = category,
                    ReturnUrl = Request.Path + Request.QueryString.ToUriComponent(),
                    CurrentPage = productResponse.Data.CurrentPage,
                    TotalPages = productResponse.Data.TotalPages,
                    Products = productResponse.Data.Items,
                    Admin = false
                });
            }

            return View(new ProductListModel<Car>
            {
                Items = productResponse.Data.Items,
                CurrentPage = productResponse.Data.CurrentPage,
                TotalPages = productResponse.Data.TotalPages,
                CurrentCategory = category
            });
        }
    }
}
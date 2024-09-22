using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICarService _carService;
        public ProductController(ICarService carService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _carService = carService;
        }

        [Route("[controller]/{category?}")]
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var productResponse =
                await _carService.GetProductListAsync(category, pageNo);
            if (!productResponse.Successfull)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            var allCategories =
                await _categoryService.GetCategoryListAsync();
            if (!allCategories.Successfull)
            {
                return NotFound(allCategories.ErrorMessage);
            }

            var currentCategory = category == null ? "Все" : allCategories.Data.FirstOrDefault(c => c.NormalizedName.Equals(category)).Name;
			ViewData["Categories"] = allCategories.Data;
            ViewData["CurrentCategory"] = currentCategory;

			return View(productResponse.Data);
        }
    }
}
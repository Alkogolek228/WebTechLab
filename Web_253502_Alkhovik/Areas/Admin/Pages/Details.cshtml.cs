using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;

public class DetailsModel : PageModel
{
	private readonly ICarService _carService;
	private readonly ICategoryService _categoryService;
	public DetailsModel(ICategoryService categoryService, ICarService carService)
	{
		_categoryService = categoryService;
		_carService = carService;
	}
	public async Task<IActionResult> OnGetAsync(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var response = await _carService.GetProductByIdAsync(id.Value);

		if (!response.Successfull)
		{
			return NotFound();
		}

		var categoryResponse = await _categoryService.GetCategoryListAsync();
		if (!categoryResponse.Successfull)
		{
			return NotFound();
		}

		Car = response.Data!;
		CategoryName = categoryResponse.Data.FirstOrDefault(c => c.Id == Car.CategoryId).Name;

		return Page();
	}

	[BindProperty]
	public Car Car { get; set; } = default;

	[BindProperty]
	public string CategoryName { get; set; } = string.Empty;
}
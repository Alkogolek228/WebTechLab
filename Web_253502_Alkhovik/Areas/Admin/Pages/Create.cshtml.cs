using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class CreateModel : PageModel
{
    private readonly ICarService _carService;
	private readonly ICategoryService _categoryService;

	public CreateModel(ICarService carService, ICategoryService categoryService)
    {
        _carService = carService;
		_categoryService = categoryService;

		Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
    }
    public async Task<IActionResult> OnGetAsync()
    {
		return Page();
	}

	[BindProperty]
	public IFormFile? Image { get; set; }

	[BindProperty]
	public Car Car { get; set; } = default!;

	public SelectList Categories { get; set; } 
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var response = await _carService.CreateProductAsync(Car, Image);

		if (!response.Successfull)
		{
			return Page();
		}

		return RedirectToPage("./Index");
	}
}
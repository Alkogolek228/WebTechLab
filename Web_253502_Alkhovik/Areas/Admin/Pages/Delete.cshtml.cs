using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;

public class DeleteModel : PageModel
{
	private readonly ICarService _carService;
	private readonly ICategoryService _categoryService;
	public DeleteModel(ICategoryService categoryService, ICarService carService)
	{
		_categoryService = categoryService;
		_carService = carService;
	}
	[BindProperty]
	public Car Car { get; set; } = default!;

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

		Car = response.Data!;

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		try
		{
			await _carService.DeleteProductAsync(id.Value);
		}
		catch (Exception e)
		{
			return NotFound(e.Message);
		}

		return RedirectToPage("./Index");
	}
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;

public class CreateModel : PageModel
{
    private readonly ICarService _carService;
    private readonly ICategoryService _categoryService;

    public CreateModel(ICarService carService, ICategoryService categoryService)
    {
        _carService = carService;
        _categoryService = categoryService;
    }

    public SelectList Categories { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var categoryResponse = await _categoryService.GetCategoryListAsync();
        if (!categoryResponse.Successfull)
        {
            return NotFound();
        }

        Categories = new SelectList(categoryResponse.Data, "Id", "Name");
        return Page();
    }

    [BindProperty]
    public Car Car { get; set; } = default!;

    [BindProperty]
    public IFormFile? Image { get; set; }

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
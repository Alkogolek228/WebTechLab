using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;

public class EditModel : PageModel
{
    private readonly ICarService _carService;
    private readonly ICategoryService _categoryService;

    public EditModel(ICarService carService, ICategoryService categoryService)
    {
        _carService = carService;
        _categoryService = categoryService;
    }

    public SelectList Categories { get; set; }

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

        Categories = new SelectList(categoryResponse.Data, "Id", "Name");
        Car = response.Data!;
        CurrentImage = Car.Image;
        return Page();
    }

    [BindProperty]
    public IFormFile? Image { get; set; }

    [BindProperty]
    public string? CurrentImage { get; set; }

    [BindProperty]
    public Car Car { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _carService.UpdateProductAsync(Car.Id, Car, Image);
        }
        catch (Exception)
        {
            if (!await CarsExists(Car.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private async Task<bool> CarsExists(int id)
    {
        var response = await _carService.GetProductByIdAsync(id);
        return response.Successfull;
    }
}
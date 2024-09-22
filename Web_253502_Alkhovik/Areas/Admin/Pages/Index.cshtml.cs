using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;

public class IndexModel : PageModel
{
	private readonly ICarService _carService;
	public IndexModel(ICarService carService)
	{
		_carService = carService;

		var cars = _carService.GetProductListAsync(null).Result.Data;
		for (int i = 0; i < cars.TotalPages; i++)
		{
			Cars.AddRange(_carService.GetProductListAsync(null, i).Result.Data.Items);
		}
	}
	public void OnGet()
	{

	}

	[BindProperty]
	public List<Car> Cars { get; set; } = new();
}
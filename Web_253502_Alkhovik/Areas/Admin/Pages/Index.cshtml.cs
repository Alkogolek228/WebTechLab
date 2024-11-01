using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Extensions;

namespace Web_253502_Alkhovik.Areas.Admin.Pages;
[Authorize(Policy = "admin")]
public class IndexModel : PageModel
{
	private readonly ICarService _carService;
	public IndexModel(ICarService carService)
	{
		_carService = carService;
	}
	public async Task<IActionResult> OnGetAsync(int pageNo = 1)
	{
		var response = await _carService.GetProductListAsync(null, pageNo);
		if (response.Successfull)
		{
			Cars = response.Data.Items;
			TotalPages = response.Data.TotalPages;
			CurrentPage = pageNo;

			if (Request.IsAjaxRequest())
			{
				return Partial("_PagerAndCardsPartial", new
				{
					Admin = true,
					CurrentPage = CurrentPage,
					TotalPages = TotalPages,
					Cars = Cars
				});
			}

			return Page();
		}

		return RedirectToPage("/Error");
	}

	[BindProperty]
	public List<Car> Cars { get; set; } = new();

	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
}
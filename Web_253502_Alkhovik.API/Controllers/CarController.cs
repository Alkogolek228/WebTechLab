using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.API.Services.CarService;

namespace WEB_253Web_253502_Alkhovik505_Shishov.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    [Route("{category?}")]
    public async Task<IActionResult> GetCars(string? category, int pageNo = 1,int pageSize = 3)
    {
			return Ok(await _carService.GetProductListAsync(
                                        category,
                                        pageNo,
                                        pageSize));
	} 

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int? id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult Create()
    {
			throw new NotImplementedException();
	}

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int? id)
    {
			throw new NotImplementedException();
	}

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
			throw new NotImplementedException();
	}
}
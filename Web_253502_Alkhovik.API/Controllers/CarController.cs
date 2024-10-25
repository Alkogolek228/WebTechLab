using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.API.Services.CarService;

namespace Web_253502_Alkhovik.API.Controllers
{
[ApiController]
[Authorize(Policy = "admin")]
[Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [AllowAnonymous]
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
            var data = await _carService.GetProductByIdAsync(id.Value);

            if (!data.Successfull)
            {
                return Problem(data.ErrorMessage);
            }

            return Ok(data.Data);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Domain.Entities.Car car)
        {
            var response = await _carService.CreateProductAsync(car);

            if (!response.Successfull)
            {
                return Problem(response.ErrorMessage);
            }

            return Ok(response.Data);
        }

        [HttpPut("{id:int}")]
       
        public async Task<IActionResult> Edit(int? id)
        {
                throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        
        public async Task<IActionResult> Delete(int? id)
        {
            await _carService.DeleteProductAsync(id.Value);
            return NoContent();
        }
    }
}
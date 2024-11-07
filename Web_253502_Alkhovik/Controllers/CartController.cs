using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Services.CarService;

namespace Web_253502_Alkhovik.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICarService _carService;
        private readonly Cart _cart;
        public CartController(ICarService carService, Cart cart)
        {
            _carService = carService;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View(_cart);
        }

		[Route("[controller]/add/{id:int}")]
		public async Task<IActionResult> AddItem(int id, string returnUrl)
        {
            var data = await _carService.GetProductByIdAsync(id);
            if (data.Successfull)
            {
                _cart.AddToCart(data.Data);
            }

            return Redirect(returnUrl);
        }

        [Route("[controller]/remove/{id:int}")]
        public async Task<IActionResult> RemoveItem(int id, string returnUrl)
        {
            var data = await _carService.GetProductByIdAsync(id);
            if (data.Successfull)
            {
                _cart.RemoveItems(id);
            }

            return Redirect(returnUrl);
        }
    }
}
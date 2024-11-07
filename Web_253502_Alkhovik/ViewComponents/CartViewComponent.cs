using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Domain.Entities;

namespace Web_253502_Alkhovik.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly Cart _cart;
        public CartViewComponent(Cart cart) 
        {
            _cart = cart;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View(_cart));
        }
    }
}
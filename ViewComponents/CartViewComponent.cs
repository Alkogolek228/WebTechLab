using Microsoft.AspNetCore.Mvc;

namespace Web_253502_Alkhovik.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public CartViewComponent() 
        {
        
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}
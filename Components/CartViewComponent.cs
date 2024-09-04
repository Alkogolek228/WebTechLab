using Microsoft.AspNetCore.Mvc;

namespace Web_253502_Alkhovik.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Здесь вы можете получить информацию о корзине из базы данных или сервиса
            var cartInfo = new
            {
                TotalPrice = "00,0 руб",
                ItemCount = 0
            };

            return View(cartInfo);
        }
    }
}
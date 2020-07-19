using Microsoft.AspNetCore.Mvc;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        
        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke() => View(_cartService.TransformFromCart());
    }
}

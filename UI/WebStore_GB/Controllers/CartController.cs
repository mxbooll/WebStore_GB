using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Details() => View(new CartOrderViewModel
        {
            Cart = _cartService.TransformFromCart(),
            Order = new OrderViewModel()
        });

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderViewModel Model, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartOrderViewModel
                {
                    Cart = _cartService.TransformFromCart(),
                    Order = Model
                });

            var orderModel = new CreateOrderModel
            {
                Order = Model,
                Items = _cartService.TransformFromCart().Items
                  .Select(item => new OrderItemDTO
                  {
                      Id = item.Product.Id,
                      Price = item.Product.Price,
                      Quantity = item.Quantity
                  })
                  .ToList()
            };

            var order = await OrderService.CreateOrder(User.Identity.Name, orderModel);

            _cartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region WebAPI

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartAPI(int id)
        {
            _cartService.AddToCart(id);
            return Json(new { id, message = $"Товар id:{id} был добавлен в корзину" });
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _cartService.DecrementFromCart(id);
            return Ok();
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            _cartService.RemoveFromCart(id);
            return Ok();
        }

        public IActionResult RemoveAllAPI()
        {
            _cartService.RemoveAll();
            return Ok();
        }
        #endregion
    }
}
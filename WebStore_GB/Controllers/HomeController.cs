using Microsoft.AspNetCore.Mvc;

namespace WebStore_GB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello from Index action");
        }

        public IActionResult Another()
        {
            return Content("Hello from Another action");
        }
    }
}
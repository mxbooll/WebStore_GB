using Microsoft.AspNetCore.Mvc;

namespace WebStore_GB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Another()
        {
            return Content("Hello from Another action");
        }
    }
}
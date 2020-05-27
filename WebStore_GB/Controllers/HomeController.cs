using Microsoft.AspNetCore.Mvc;

namespace WebStore_GB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
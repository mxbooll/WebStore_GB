using Microsoft.AspNetCore.Mvc;
using WebStore_GB.Interfaces.TestApi;

namespace WebStore_GB.Controllers
{
    public class WebApiTestController : Controller
    {
        private readonly IValueService _valueService;

        public WebApiTestController(IValueService valueService)
        {
            _valueService = valueService;
        }

        public IActionResult Index()
        {
            var values = _valueService.Get();
            return View(values);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore_GB.Domain.Entities.Identity;

namespace WebStore_GB.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.ADMINISTRATOR)]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.ViewModels.Identity;

namespace WebStore_GB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register new user
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterUserViewModel Model)
        {
            return RedirectToAction("Index", "Home");
        }
        #endregion

        public IActionResult Login() => View();

        public IActionResult Logout() => View();

        public IActionResult AccessDenied() => View();
    }
}
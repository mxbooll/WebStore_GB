using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Domain.ViewModels.Identity;

namespace WebStore_GB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Register new user
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var user = new User
            {
                UserName = Model.UserName
            };

            using (_logger.BeginScope("Регистрация нового пользователя {0}", user.UserName))
            {
                _logger.LogInformation("Начинается процесс регистрации нового пользователя {0}", user.UserName);

                var registrationResult = await _userManager.CreateAsync(user, Model.Password);
                if (registrationResult.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                    var addUserRoleResult = await _userManager.AddToRoleAsync(user, Role.USER);
                    if (addUserRoleResult.Succeeded)
                    {
                        _logger.LogInformation("Пользователю успешно добавлена роль {0}", Role.USER);
                    }
                    else
                    {
                        _logger.LogError("Ошибка при добавлении пользователю роли {0}: {1}", Role.USER, string.Join(",", addUserRoleResult.Errors.Select(error => error.Description)));
                        throw new ApplicationException("Ошибка наделения нового пользователя ролью Пользователь");
                    }

                    await _signInManager.SignInAsync(user, false);
                    _logger.LogInformation("Пользователь {0} успешно вошел в систему", user.UserName);
                    return RedirectToAction("Index", "Home");
                }
                _logger.LogError("Ошибка при добавлении нового пользователя роли {0}: {1}",
                         user.UserName, string.Join(",", registrationResult.Errors.Select(error => error.Description)));
                foreach (var error in registrationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            //_Logger.Log(LogLevel.Critical, new EventId(0, "Test"), "QWE", null, (s, e) => s);
            return View(Model);
        }
        #endregion

        #region Login user
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _signInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false);

            if (login_result.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} успешно вошел в систему");
                if (Url.IsLocalUrl(Model.ReturnUrl))
                {
                    _logger.LogDebug("Выполняю перенаправление на {0}", Model.ReturnUrl);
                    return Redirect(Model.ReturnUrl);
                }
                _logger.LogDebug("Выполняю перенаправление на главную страницу");
                return RedirectToAction("Index", "Home");
            }

            _logger.LogWarning("Пользователь {0} произвел", Model.ReturnUrl);

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя, или пароль!");

            return View(Model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Пользователь {0} вышел из системы", userName)ж
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}
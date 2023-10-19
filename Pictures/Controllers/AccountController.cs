using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Pictures.Domain.ViewModels.Account;
using Pictures.Services.Interfaces;
using System.Security.Claims;

namespace Pictures.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) => 
            (_accountService) = (accountService);

        // Регистрация пользователя
        [HttpGet]
        public IActionResult Registration() => View();

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if(ModelState.IsValid) // закомментировал чтобы не сохранять в бд
            {
                var response = _accountService.Register(model);
                if (response.StatusCode is Domain.Enums.StatusCode.Success)
                {
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("GetPictures", "Picture");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        // Вход в учётную запись
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _accountService.Login(model);
                if (response.StatusCode is Domain.Enums.StatusCode.Success)
                {
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("GetPictures", "Picture");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        // Выход из учётной записи

        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

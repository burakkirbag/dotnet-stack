using Microsoft.AspNetCore.Mvc;
using stack.Helper;
using stack.Models.DTOs;
using stack.Services;
using System.Threading.Tasks;

namespace stack.Controllers
{
    public class AuthController : ManagementController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInput input)
        {
            var login = await _authenticationService.Login(input.Email, input.Password, "Admin");
            if (!login.Success)
            {
                SetWarningMessage(login.Message, login.Errors);
                return View();
            }

            CookieHelper.Set(HttpContext,
                StackEnvironments.CookieAuthOptions.Name,
                login.Data.AccessToken,
                StackEnvironments.CookieAuthOptions.ValidityDay);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            CookieHelper.Remove(HttpContext, StackEnvironments.CookieAuthOptions.Name);

            return RedirectToAction("Login", "Auth");
        }
    }
}

using CMS.Application.Account.ViewModels;
using CMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AccountController(IAccountService service) : Controller
    {
        private readonly IAccountService _service = service;


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");  // به صفحه اصلی هدایت کنید
            }

            return View();
        }

        [HttpPost("account/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");  // به صفحه اصلی هدایت کنید
            }
            var result = await _service.CreateUserAsync(user.Email, user.Password, user.UserName);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Register");
            }

            TempData["Success"] = result.Message;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");  // به صفحه اصلی هدایت کنید
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");  // به صفحه اصلی هدایت کنید
            }
            var result = await _service.LoginAsync(viewModel);
            if (result.Message == "ایمیل شما تأیید نشده است. لطفاً ایمیل خود را بررسی کنید.")
            {
                ModelState.AddModelError(String.Empty, result.Message!);
                return View(viewModel);
            }
            else if (!result.Success)
            {
                ModelState.AddModelError(String.Empty, result.Message!);
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            var result = await _service.ConfirmEmailAsync(userid, token);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View();
            }

            TempData["Success"] = result.Message;
            return View();

        }

        [HttpPost("account/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var result = await _service.LogOutAsync();

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Home");
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index", "Home");
        }
    }
}

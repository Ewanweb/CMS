using CMS.Application.ChekOut.ViewModel;
using CMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class CheckOutController(UserManager<IdentityUser> userManager, ICheckOutService service) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ICheckOutService _service = service;

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            var user = await _userManager.GetUserAsync(User);


            var result = await _service.ListOrders(user.Id);

            if (!result.Success)
            {
                ModelState.AddModelError(String.Empty, result.Message!);
                return View();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessOrder(CheckOutViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction("Login", "Account");

            var (orderId, result) = await _service.ProcessOrder(viewModel, user);

            if (!result.Success || orderId is 0 || orderId is null)
            {
                TempData["Error"] = result.Message;
                return View("Index");
            }

            return RedirectToAction("Index", new {orderId});
        }
    }
}

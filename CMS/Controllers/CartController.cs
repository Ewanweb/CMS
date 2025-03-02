using CMS.Application.Services;
using CMS.Application.SmallCartView.ViewModels;
using CMS.Domain.Admin.Cart;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using CMS.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CMS.Controllers
{
    public class CartController(IProductRepository repository, ICartService service) : Controller
    {
        private readonly IProductRepository _repository = repository;
        private readonly ICartService _service = service;

        public IActionResult Index()
        {
            var cartItem = _service.GetCartItems();
            return View(cartItem);
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            Product? product = await _repository.GetProductByIdAsync(productId);

            if (product is null)
                return NotFound();
            
            var result = _service.AddToCart(new SmallCartViewModel
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
                Image = product.Image
            });

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return Redirect(Request.Headers.Referer.ToString());
            }

            TempData["Success"] = "محصول با موفقیت اضافه شد!";

            return Redirect(Request.Headers.Referer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int productId)
        {

            var result = _service.RemoveFromCart(productId);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return Redirect(Request.Headers.Referer.ToString());
            }
            TempData["Success"] = "محصول با موفقیت حذف شد!";

            return Redirect(Request.Headers.Referer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {

            var result = _service.UpdateQuantity(productId, quantity);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return Redirect(Request.Headers.Referer.ToString());
            }
            TempData["Success"] = "محصول با موفقیت اضافه شد!";

            return Redirect(Request.Headers.Referer.ToString());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearCart()
        {

            var result = _service.ClearCart();

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return Redirect(Request.Headers.Referer.ToString());
            }
            TempData["Success"] = result.Message;

            return Redirect(Request.Headers.Referer.ToString());
        }
    }
}

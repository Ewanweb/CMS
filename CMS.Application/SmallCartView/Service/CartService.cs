using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CMS.Application.Common;
using CMS.Application.Services;
using CMS.Application.SmallCartView.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.SmallCartView.Service
{
    public class CartService(IHttpContextAccessor httpContext) : ICartService
    {
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private const string SessionKey = "Cart";

        private List<SmallCartViewModel> GetCart()
        {
            var session = _httpContext.HttpContext?.Session;

            if (session == null)
                return new List<SmallCartViewModel>();

            var cartJson = session.GetString(SessionKey);

            return string.IsNullOrEmpty(cartJson)
                ? new List<SmallCartViewModel>()
                : JsonSerializer.Deserialize<List<SmallCartViewModel>>(cartJson) ?? new List<SmallCartViewModel>();
        } 

        private void SaveCart(List<SmallCartViewModel> cart)
        {
            var session = _httpContext.HttpContext?.Session;

            if (session == null)
                throw new InvalidOperationException("Session is not available.");

            session.SetString(SessionKey, JsonSerializer.Serialize(cart));
        }
        public ServiceResult AddToCart(SmallCartViewModel item)
        {
            try
            {
                if (item == null)
                    return ServiceResult.Fail("آیتم نمی‌تواند خالی باشد.");

                if (item.Quantity <= 0)
                    return ServiceResult.Fail("تعداد باید بیشتر از صفر باشد.");

                var cart = GetCart();

                var existingItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (existingItem != null)
                    existingItem.Quantity += item.Quantity;
                else
                    cart.Add(item);

                SaveCart(cart);
                return ServiceResult.Ok("محصول با موفقیت به سبد خرید اضافه شد!");
            }
            catch (JsonException)
            {
                return ServiceResult.Fail("خطا در پردازش سبد خرید.");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Session"))
            {
                return ServiceResult.Fail("سبد خرید در دسترس نیست.");
            }
            catch (Exception)
            {
                return ServiceResult.Fail("خطای ناشناخته در افزودن محصول.");
            }
        }

        public List<SmallCartViewModel> GetCartItems()
        {
            try
            {
                return GetCart();
            }
            catch
            {
                return new List<SmallCartViewModel>();
            }
        }

        public decimal GetTotalPrice()
        {
            try
            {
                return GetCart().Sum(p => p.TotalPrice);
            }
            catch
            {
                return 0m;
            }
        }

        public ServiceResult RemoveFromCart(int productId)
        {
            try
            {
                var cart = GetCart();
                var item = cart.FirstOrDefault(p => p.ProductId == productId);

                if (item == null)
                    return ServiceResult.Fail("محصول در سبد خرید یافت نشد.");

                cart.RemoveAll(p => p.ProductId == productId);
                SaveCart(cart);

                return ServiceResult.Ok("محصول با موفقیت حذف شد!");
            }
            catch (JsonException)
            {
                return ServiceResult.Fail("خطا در پردازش سبد خرید.");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Session"))
            {
                return ServiceResult.Fail("سبد خرید در دسترس نیست.");
            }
            catch (Exception)
            {
                return ServiceResult.Fail("خطای ناشناخته در حذف محصول.");
            }

        }

        public ServiceResult UpdateQuantity(int productId, int quantity)
        {
            try
            {
                if (quantity <= 0)
                    return ServiceResult.Fail("تعداد باید بیشتر از صفر باشد.");

                var cart = GetCart();
                var item = cart.FirstOrDefault(p => p.ProductId == productId);

                if (item == null)
                    return ServiceResult.Fail("محصول در سبد خرید یافت نشد.");

                item.Quantity = quantity;
                SaveCart(cart);

                return ServiceResult.Ok("تعداد با موفقیت به‌روزرسانی شد!");
            }
            catch (JsonException)
            {
                return ServiceResult.Fail("خطا در پردازش سبد خرید.");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Session"))
            {
                return ServiceResult.Fail("سبد خرید در دسترس نیست.");
            }
            catch (Exception)
            {
                return ServiceResult.Fail("خطای ناشناخته در به‌روزرسانی تعداد.");
            }
        }

        public ServiceResult ClearCart()
        {
            _httpContext.HttpContext?.Session.Remove(SessionKey);

            if (_httpContext.HttpContext is null)
                return ServiceResult.Ok("سبد خرید پیدا نشد !");

            return ServiceResult.Ok("سبد خرید با موفقیت خالی شد !");
        }
    }
}

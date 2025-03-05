using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.ChekOut.ViewModel;
using CMS.Application.Common;
using CMS.Application.Services;
using CMS.Domain.Admin.Orders;
using CMS.Domain.Admin.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CMS.Application.ChekOut.Service
{
    public class CheckOutService(IOrderRepository repository, IOrderDetailRepository orderDetailRepository, ILogger<CheckOutService> logger, UserManager<IdentityUser> userManager) : ICheckOutService
    {
        private readonly IOrderRepository _repository = repository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;
        private readonly ILogger<CheckOutService> _logger = logger;

        public async Task<ServiceResult> ListOrders(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return ServiceResult.Fail("کاربر پیدا نشد");

            var orders = await _repository.GetUserOrders(user.Id);
            if (orders is null || !orders.Any())
                return ServiceResult.Fail("چیزی پیدا نشد");

            return ServiceResult.Ok(null);
        }

        public async Task<(int? orderId,ServiceResult)> ProcessOrder(CheckOutViewModel? model, IdentityUser? user)
        {
            try
            {
                // 1️⃣ بررسی مقدار `model`
                if (model == null || !model.Items.Any())
                    return (null,ServiceResult.Fail("اطلاعات سفارش نامعتبر است."));

                // 2️⃣ بررسی مقدار `user`
                if (user == null)
                    return (null, ServiceResult.Fail("کاربر نامعتبر است. لطفاً دوباره وارد شوید."));

                // 3️⃣ بررسی خالی بودن سبد خرید
                if (!model.Items.Any())
                    return (null, ServiceResult.Fail("سبد خرید شما خالی است."));

                // 4️⃣ ایجاد سفارش جدید
                var order = new Order()
                {
                    UserId = user.Id,
                    TotalPrice = model.Items.Sum(i => i.Price * i.Quantity),
                    Status = OrderStatus.pending,
                    CreatedTime = DateTime.Now
                };

                // 5️⃣ ثبت سفارش در دیتابیس
                await _repository.AddAsync(order);

                // 6️⃣ ثبت جزئیات سفارش
                foreach (var item in model.Items)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        ProductName = item.Name,
                        ProductImage = item.Image,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    };

                    await _orderDetailRepository.AddAsync(orderDetail);

                }

                // ✅ موفقیت‌آمیز
                return (order.Id, ServiceResult.Ok(null));
            }
            catch (Exception ex)
            {
                // 7️⃣ لاگ کردن خطا
                _logger.LogError(ex, "خطای غیرمنتظره در پردازش سفارش");

                return (null, ServiceResult.Fail("یک خطای غیرمنتظره رخ داده است. لطفاً بعداً دوباره تلاش کنید."));
            }
        }
    }
}

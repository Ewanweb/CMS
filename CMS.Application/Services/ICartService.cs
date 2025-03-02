using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common;
using CMS.Application.SmallCartView.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Services
{
    public interface ICartService
    {
        ServiceResult AddToCart(SmallCartViewModel item);
        ServiceResult RemoveFromCart(int productId);
        ServiceResult UpdateQuantity(int productId, int quantity);
        List<SmallCartViewModel> GetCartItems();
        decimal GetTotalPrice();
        ServiceResult ClearCart();
    }
}

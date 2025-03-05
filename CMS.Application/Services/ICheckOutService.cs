using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.ChekOut.ViewModel;
using CMS.Application.Common;
using CMS.Domain.Admin.Orders;
using CMS.Domain.Admin.Repository;
using Microsoft.AspNetCore.Identity;

namespace CMS.Application.Services
{
    public interface ICheckOutService
    {
        Task<(int? orderId, ServiceResult)> ProcessOrder(CheckOutViewModel model, IdentityUser user);
        Task<ServiceResult> ListOrders(string userId);
    }
}

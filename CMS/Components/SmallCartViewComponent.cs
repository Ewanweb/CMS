using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Application.SmallCartView.ViewModels;
using CMS.Domain.Admin.Cart;
using CMS.Infrastructure;
using CMS.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Components
{
    //public class SmallCartViewComponent : ViewComponent
    //{
    //    //public IViewComponentResult Invoke()
    //    //{
    //    //    List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

    //    //    SmallCartViewModel smallCartViewModel;

    //    //    if (!cart.Any())
    //    //    {
    //    //        smallCartViewModel = null;
    //    //    }


    //    //    return View(smallCartViewModel);
    //    //}
    //}
}
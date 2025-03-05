using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.SmallCartView.ViewModels;

namespace CMS.Application.ChekOut.ViewModel
{
    public class CheckOutViewModel
    {
        [Required]
        public List<SmallCartViewModel> Items { get; set; } = new List<SmallCartViewModel>();

        [Required(ErrorMessage = "وارد کردن نام کامل الزامی است.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "وارد کردن ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است.")]
        [Phone(ErrorMessage = "فرمت شماره تلفن معتبر نیست.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "وارد کردن آدرس الزامی است.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "کد پستی الزامی است.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد پستی باید ۱۰ رقم باشد.")]
        public string PostalCode { get; set; }

        public string PaymentMethod { get; set; } = "Online";

        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }
}

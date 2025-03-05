using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Account.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required, MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "حداقل 4 کاراکتر")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password", ErrorMessage = "کلمه عبور با تاییدیه کلمه عبور یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Account.ViewModels
{
    public class LoginViewModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}

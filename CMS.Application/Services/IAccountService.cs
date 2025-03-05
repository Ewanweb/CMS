using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Account.ViewModels;
using CMS.Application.Common;
using Microsoft.AspNetCore.Identity;

namespace CMS.Application.Services
{
    public interface IAccountService
    {
        Task<ServiceResult> SendEmailConfirm(IdentityUser? user);
        Task<ServiceResult> CreateUserAsync(string email, string password, string fullname);
        Task<ServiceResult> ConfirmEmailAsync(string userId, string token);
        Task<ServiceResult> LoginAsync(LoginViewModel viewModel);
        Task<ServiceResult> LogOutAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Account.ViewModels;
using CMS.Application.Common;
using CMS.Application.Common.Utils;
using CMS.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.WebEncoders;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CMS.Application.Account.Service
{
    public class AccountService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext, SignInManager<IdentityUser> signInManager, EmailSender emailSender) : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private readonly EmailSender _emailSender = emailSender;

        public async Task<ServiceResult> SendEmailConfirm(IdentityUser? user)
        {
            if (user is null)
                return ServiceResult.Fail("کاربر یافت نشد.");

            if (string.IsNullOrEmpty(user.Email))
                return ServiceResult.Fail("ایمیل کاربر نامعتبر است.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var confirmationLink = $"{_httpContext.HttpContext?.Request.Scheme}://{_httpContext.HttpContext?.Request.Host}/Account/ConfirmEmail?userId={user.Id}&token={encodedToken}";

            await _emailSender.SendEmailAsync(user.Email, "تایید ایمیل", $"برای تأیید ایمیل خود روی این لینک کلیک کنید: <a href='{confirmationLink}'>تأیید ایمیل</a>");

            return ServiceResult.Ok(null);

        }

        public async Task<ServiceResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return ServiceResult.Fail("کاربر پیدا نشد دوباره تلاش کنید و مطمئن شوید که اطلاعات را درست وارد کردید !");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var tokenType = _userManager.Options.Tokens.EmailConfirmationTokenProvider;

            var isTokenValid = await _userManager.VerifyUserTokenAsync(user, tokenType, "EmailConfirmation", decodedToken);

            if (!isTokenValid)
                return ServiceResult.Fail("توکن ارسال شده معتبر نیست");


            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                return ServiceResult.Fail(string.Join("\n", result.Errors.Select(e => e.Description)));

            await _signInManager.SignInAsync(user, true);

            return ServiceResult.Ok("ایمیل با موفقیت تائید شد !");
        }

        public async Task<ServiceResult> CreateUserAsync(string email, string password, string fullname)
        {
            var user = new IdentityUser()
            {
                Email = email,
                UserName = fullname
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return ServiceResult.Fail(string.Join("\n", result.Errors.Select(e => e.Description)));

            var emailSent = await SendEmailConfirm(user);

            if (!emailSent.Success)
                return ServiceResult.Fail(emailSent.Message!);

            return ServiceResult.Ok("ثبت نام با موفقیت انجام شد حالا ایمیل خود را فعال کنید!");
        }

        public async Task<ServiceResult> LoginAsync(LoginViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(viewModel.UserName);

            if (user is null)
                return ServiceResult.Fail("نام کاربری یا کلمه عبور اشتباه است");

            if (!user.EmailConfirmed)
            {
                await SendEmailConfirm(user);
                return ServiceResult.Fail("ایمیل شما تأیید نشده است. لطفاً ایمیل خود را بررسی کنید.");
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);

            if (!result.Succeeded)
                return ServiceResult.Fail("نام کاربری یا کلمه عبور اشتباه است");

            return ServiceResult.Ok(null);
        }

        public async Task<ServiceResult> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return ServiceResult.Ok("کاربر با موفقیت خارج شد !");
        }
    }
}

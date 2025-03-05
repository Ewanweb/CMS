using System.Threading.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using CMS.Domain.Admin.Repository;
using CMS.Infrastructure.Repositories;
using AutoMapper;
using CMS.Application.Account.Service;
using CMS.Application.ChekOut.Service;
using CMS.Application.Products.Service;
using CMS.Application.Common.Utils;
using CMS.Application.Pages.Services;
using CMS.Application.Services;
using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Products;
using CMS.Application.MapingProfiles;
using CMS.Application.SmallCartView.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CMS.Infrastructure
{
    public static class ServicesRegister
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {

            //Register DbContext
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped);

            //Auto Maper Configurations
            services.AddAutoMapper(typeof(MappingProfile));

            //Config Http Context Accessor
            services.AddHttpContextAccessor();

            //Config Cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // مسیر صفحه ورود
                options.LogoutPath = "/Account/Logout"; // مسیر خروج
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // زمان انقضای کوکی
                options.SlidingExpiration = true; // تمدید خودکار کوکی در صورت فعالیت
            });

            //Config Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });


            //Register Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrors>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.MaxFailedAccessAttempts = 5;   // بعد از ۵ تلاش ناموفق، حساب قفل شود
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // قفل ۱۵ دقیقه‌ای

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            });




            //Register Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductGalleryRepository, ProductGalleryRepository>();

            services.AddScoped<IPageRepository, PageRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(SlugGenerator<Page>), sp =>
                new SlugGenerator<Page>(
                    sp.GetRequiredService<IRepository<Page>>(),
                    p => p.Slug ?? throw new NullReferenceException()
                ));

            services.AddScoped(typeof(SlugGenerator<Product>), sp =>
                new SlugGenerator<Product>(
                    sp.GetRequiredService<IRepository<Product>>(),
                    p => p.Slug
                ));

            //Register Services
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IProductGalleryService, ProductGalleryService>();

            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ICheckOutService, CheckOutService>();

            services.AddScoped<PageService>();

            services.AddScoped<EmailSender>();


            return services;
        }
    }
}

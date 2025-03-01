using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using CMS.Domain.Admin.Repository;
using CMS.Infrastructure.Repositories;
using AutoMapper;
using CMS.Application.Products.Service;
using CMS.Application.Common.Utils;
using CMS.Application.Pages.Services;
using CMS.Application.Services;
using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Products;
using CMS.Application.MapingProfiles;

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

            //Register Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductGalleryRepository, ProductGalleryRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(SlugGenerator<Page>), sp =>
                new SlugGenerator<Page>(
                    sp.GetRequiredService<IRepository<Page>>(),
                    p => p.Slug
                ));

            services.AddScoped(typeof(SlugGenerator<Product>), sp =>
                new SlugGenerator<Product>(
                    sp.GetRequiredService<IRepository<Product>>(),
                    p => p.Slug
                ));

            //Register Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGalleryService, ProductGalleryService>();
            services.AddScoped<PageService>();
            return services;
        }
    }
}

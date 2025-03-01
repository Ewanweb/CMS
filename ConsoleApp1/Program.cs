using AutoMapper;
using CMS.Application.Common.Utils;
using CMS.Application.Products.Service;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using CMS.Infrastructure.Repositories;
using CMS.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

class Program
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;
    private readonly IProductRepository _repositor;
    private readonly SlugGenerator<Product> _slugGenerator;

    static async Task Main(string[] args)
    {
        // 1️⃣ مقداردهی DbContextOptions
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer("Data Source=Ewan;Initial Catalog=CMSEcommerce;TrustServerCertificate=True;Integrated Security=True")
            .Options;

        // 2️⃣ مقداردهی DataContext
        using var context = new DataContext(options);

        // 3️⃣ مقداردهی Repository و Service
        var productRepository = new ProductRepository(context);

        // 4️⃣ درخواست دریافت محصولات بر اساس دسته‌بندی
        Console.Write("Enter category slug: ");
        string slug = Console.ReadLine();

        var products = await productRepository.GetProductsByCategory(slug);

        // 5️⃣ نمایش خروجی
        Console.WriteLine("\nProducts:");
        foreach (var product in products)
        {
            Console.WriteLine($"- {product.Name} (ID: {product.Id})");
        }
    }
}
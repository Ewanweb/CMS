using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using CMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common;
using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products.Gallery;

namespace CMS.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var category = await _context.Categories.ToListAsync();
            if (!category.Any())
                throw new Exception("دسته بندی یافت نشد");

            return category;


        }

        public async Task<Product?> GetProductBySlugAsync(string slug)
        {
            Product? product = await _context.Products.Where(x => x.Slug == slug).FirstOrDefaultAsync();


            return product;
        }

        public async Task<IEnumerable<ProductGallery>?> GetProductGalleyAsync(int id)
        {
            IEnumerable<ProductGallery> product = await _context.ProductsGallery.Where(x => x.ProductId == id).ToListAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int categoryId)
        {

            var products = _context.Products
                .Where(x => categoryId == 0 || x.CategoryId == categoryId)
                .Include(p => p.Category)
                .OrderBy(x => x.Id);

            return products;

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                                          .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                throw new Exception("محصول یافت نشد");

            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct is null)
                throw new Exception("محصول یافت نشد");

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string? slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return new List<Product>();
            }

            var products = await _context.Categories
                .Where(c => c.Slug == slug)
                .Include(c => c.Products)
                .SelectMany(c => c.Products)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return products;
        }
    }
}

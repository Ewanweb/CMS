using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Products.Gallery;

namespace CMS.Domain.Admin.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int categoryId);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategory(string slug);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Product?> GetProductBySlugAsync(string slug);
        Task<IEnumerable<ProductGallery>?> GetProductGalleyAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products;

namespace CMS.Domain.Admin.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Product>> GetProductsCategoryAsync(int categoryId);
    }
}

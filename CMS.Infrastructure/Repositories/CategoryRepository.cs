using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public Task<IEnumerable<Product>> GetProductsCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}

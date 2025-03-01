using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly DataContext _context;

        public PageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Page>> GetAllAsync()
        {
            var page = await _context.Pages.OrderBy(p => p.Order).ToListAsync();
            if (page is null || page.Count < 1 || !page.Any())
                return Enumerable.Empty<Page>();
            return page;
        }

        public async Task<Page> GetByIdAsync(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
                throw new Exception("محصول پیدا نشد");

            return page;
        }

        public async Task<Page> GetBySlugAsync(string slug)
        {
            var slugs = await _context.Pages.FirstOrDefaultAsync(p => p.Slug == slug);



            return slugs;
        }

        public async Task AddAsync(Page page)
        {
            _context.Pages.Add(page);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Page page)
        {
            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var page = await GetByIdAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SlugIsExist(string slug)
        {
            await _context.Pages.AnyAsync(p => p.Slug == slug);

            return true;
        }
    }
}

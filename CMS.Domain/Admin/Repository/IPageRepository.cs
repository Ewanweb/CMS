using CMS.Domain.Admin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Admin.Repository
{
    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetAllAsync();
        Task<Page> GetByIdAsync(int id);
        Task<Page> GetBySlugAsync(string slug);
        Task<bool> SlugIsExist(string slug);
        Task AddAsync(Page page);
        Task UpdateAsync(Page page);
        Task DeleteAsync(int id);
    }
}

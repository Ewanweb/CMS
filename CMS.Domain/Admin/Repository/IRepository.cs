using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Admin.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> SlugIsExist(string slug);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
        void Save();
        void Update(T entity);
    }
}

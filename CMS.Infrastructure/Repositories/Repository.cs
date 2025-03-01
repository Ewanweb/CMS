using CMS.Domain.Admin.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CMS.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> SlugIsExist(string slug)
        {
            // بررسی وجود Slug در مدل
            var property = typeof(T).GetProperty("Slug");
            if (property == null)
                throw new InvalidOperationException($"Model {typeof(T).Name} does not contain a 'Slug' property.");

            return await _dbSet.AnyAsync(e => EF.Property<string>(e, "Slug") == slug);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var property = await _dbSet.FindAsync(id);

            if (property is null)
                throw new Exception("چیزی برای نمایش پیدا نشد");

            return property;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public T GetById(int id)
        {
            var property = _dbSet.Find(id); // روش همزمان برای دریافت داده
            
            return property;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges(); // ذخیره‌سازی همزمان
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}

using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using CMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products.Gallery;

namespace CMS.Infrastructure.Repositories
{
    public class ProductGalleryRepository : Repository<ProductGallery>, IProductGalleryRepository
    {
        private readonly DataContext _context;

        public ProductGalleryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductGallery>> GetAllGallery(int productId)
        {
            var galleries = await _context.ProductsGallery
                .Where(g => g.ProductId == productId)
                .ToListAsync();


            return galleries;
        }

        public async Task<bool> AddGalleryImagesAsync(List<ProductGallery> galleryImages)
        {
            if (galleryImages == null || galleryImages.Count == 0)
                throw new ArgumentException("لیست تصاویر نمی‌تواند خالی باشد.");
            
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.ProductsGallery.AddRangeAsync(galleryImages);

                await _context.SaveChangesAsync();

                if (galleryImages.All(g => g.Id == 0))
                    throw new Exception("داده‌ها در دیتابیس ذخیره نشدند!");

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                Console.WriteLine($"خطا در ذخیره‌سازی تصاویر: {ex.Message}"); // لاگ خطا

                return false;
            }
        }

        public async Task<ProductGallery> GetGalleryImageByIdAsync(int imageId)
        {
            var gallery = await _context.ProductsGallery
                .Where(g => g.Id == imageId)
                .FirstOrDefaultAsync();

            if (gallery is null || gallery.Id == 0 || imageId is 0)
                throw new Exception("تصویر یافت نشد");

            return gallery;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
                throw new Exception("محصولی یافت نشد");

            return product;

        }
    }
}

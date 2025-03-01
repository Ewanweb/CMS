using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common;
using CMS.Application.Services;
using CMS.Domain.Admin.Products.Gallery;
using CMS.Domain.Admin.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Products.Service
{
    public class ProductGalleryService : IProductGalleryService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductGalleryRepository _repository;
        private readonly IProductService _productService;

        public ProductGalleryService(IWebHostEnvironment webHostEnvironment, IProductGalleryRepository repository, IProductService productService)
        {
            _webHostEnvironment = webHostEnvironment;
            _repository = repository;
            _productService = productService;
        }

        public async Task<ServiceResult> AddProductGallery(int productId, List<IFormFile> images)
        {

            if (images.Count <= 0 || !images.Any())
                return ServiceResult.Fail("لطفاً حداقل یک تصویر انتخاب کنید.");

            var galleryImages = new List<ProductGallery>();

            var errors = new List<string>(); // لیست برای ذخیره خطاها

            foreach (var image in images)
            {
                string imageName = await _productService.HandleImageUpload(image, true);

                if (imageName == "فرمت فایل نامعتبر است." || imageName == "حجم فایل نباید بیشتر از ۲ مگابایت باشد.")
                {
                    errors.Add(imageName);
                    continue;
                }

                galleryImages.Add(new ProductGallery
                {
                    ProductId = productId,
                    Image = imageName
                });
            }

            if (galleryImages.Count <= 0 || !galleryImages.Any())
                return errors.Any() ? ServiceResult.Fail(errors.First()) : ServiceResult.Fail("خطا غیرمنتظره رخ داد !"); // اگر خطایی وجود داشته باشد، اولین خطا را برگردان


            var isSaved = await _repository.AddGalleryImagesAsync(galleryImages);

            if (!isSaved)
                return ServiceResult.Fail("مشکلی در ذخیره سازی تصویر به وجود امد");

            return ServiceResult.Ok("عملیات موفقیت امیز بود");
        }


        public async Task<ServiceResult> DeleteProductGallery(int imageId)
        {
            if (imageId <= 0)
                return ServiceResult.Fail("شناسه تصویر نا معتبر است");

            var galleryImage = await _repository.GetByIdAsync(imageId);

            if (galleryImage == null)
                return ServiceResult.Fail("تصویر یافت نشد");

            try
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Media/Products/Gallery",
                    galleryImage.Image);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                await _repository.DeleteAsync(galleryImage);

                return ServiceResult.Ok("عملیات موفقیت امیز بود");
            }
            catch (Exception e)
            {
                return ServiceResult.Fail($"خطا در حذف تصویر: {e.Message}");

            }

        }
    }
}

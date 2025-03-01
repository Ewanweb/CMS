using AutoMapper;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CMS.Application.Common;
using CMS.Application.Products.Dtos;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using CMS.Application.Common.Utils;
using static System.Net.WebRequestMethods;
using CMS.Domain.Admin.Products.Gallery;
using CMS.Application.Services;

namespace CMS.Application.Products.Service
{
    public class ProductService : IProductService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repositor;
        private readonly SlugGenerator<Product> _slugGenerator;

        public ProductService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IProductRepository repository, SlugGenerator<Product> slugGenerator)
        {
            _repositor = repository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _slugGenerator = slugGenerator;
        }

        public async Task<string> HandleImageUpload(IFormFile? imageUpload, bool isGallery = false) // پارامتر nullable شد
        {
            if (imageUpload == null)
            {
                return "noimage.png"; // مقدار پیش‌فرض در صورت نبود تصویر
            }

            string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };
            string extension = Path.GetExtension(imageUpload.FileName).ToLower();

            if (!permittedExtensions.Contains(extension))
            {
                return "فرمت فایل نامعتبر است."; // فرمت نامعتبر
            }

            if (imageUpload.Length > 2 * 1024 * 1024) // 2MB
            {
                return "حجم فایل نباید بیشتر از ۲ مگابایت باشد."; // حجم نامعتبر
            }

            // تعیین مسیر ذخیره‌سازی
            string folder = isGallery ? "media/products/gallery" : "media/products";
            string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            string imageName = $"{Guid.NewGuid()}_{Path.GetFileName(imageUpload.FileName)}";
            string filePath = Path.Combine(uploadsDir, imageName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await imageUpload.CopyToAsync(fs);
            }

            return imageName; // نام فایل ذخیره شده
        }


        public async Task<ServiceResult> CreateProduct(ProductDTO productDto)
        {
            if (productDto.CategoryId == 0)
            {
                return ServiceResult.Fail("لطفاً یک دسته‌بندی انتخاب کنید.");
            }

            productDto.Slug = await _slugGenerator.CheckAndGenerateSlug(productDto.Slug);

            string imageName = await HandleImageUpload(productDto.ImageUpload);

            if (imageName == "فرمت فایل نامعتبر است." || imageName == "حجم فایل نباید بیشتر از ۲ مگابایت باشد.")
            {
                return ServiceResult.Fail(imageName);
            }

            Product product = _mapper.Map<Product>(productDto);

            product.Image = imageName;

            await _repositor.AddProductAsync(product);

            return ServiceResult.Ok("محصول با موفقیت اضافه شد");
        }


        public async Task<ServiceResult> EditProduct(EditProductDto productDto)
        {
            if (productDto.CategoryId == 0)
            {
                return ServiceResult.Fail("لطفاً یک دسته‌بندی انتخاب کنید.");
            }

            var existingProduct = await _repositor.GetProductByIdAsync(productDto.Id);
            if (existingProduct == null)
            {
                return ServiceResult.Fail("محصول یافت نشد.");
            }

            productDto.Slug = await _slugGenerator.CheckAndGenerateSlug(productDto.Slug, productDto.Id);

            // مقدار پیش‌فرض تصویر را نگه داریم
            string imageName = productDto.Image;

            if (productDto.ImageUpload != null)
            {
                imageName = await HandleImageUpload(productDto.ImageUpload);

                if (imageName == "فرمت فایل نامعتبر است." || imageName == "حجم فایل نباید بیشتر از ۲ مگابایت باشد.")
                {
                    return ServiceResult.Fail(imageName);
                }
            }

            // حذف تصویر قبلی اگر تغییر کرده باشد
            if (!string.Equals(existingProduct.Image, "noimage.png") && productDto.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/Products");
                string oldImagePath = Path.Combine(uploadsDir, existingProduct.Image);

                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            Product product = _mapper.Map<Product>(productDto);
            product.Image = imageName;

            await _repositor.UpdateProductAsync(product);
            return ServiceResult.Ok("محصول با موفقیت ویرایش شد");
        }

        public async Task<ServiceResult> DeleteProduct(Product? product)
        {
            if (product == null)
                return ServiceResult.Fail("محصول پبدا نشد!");


            await _repositor.DeleteAsync(product);

            return ServiceResult.Ok("محصول با موفقیت حذف شد");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Application.Common;
using CMS.Application.Products.Dtos;
using CMS.Application.Services;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Repository;
using CMS.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using X.PagedList.Extensions;

namespace CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductRepository repository, IMapper mapper, IProductService productService)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _productService = productService;
        }

        // نمایش لیست محصولات
        public async Task<IActionResult> Index(int? page, int categoryId = 0, int pageSize = 10)
        {
            var pageNumber = page ?? 1;

            ViewBag.Categories = new SelectList(await _repository.GetAllCategoriesAsync(), "Id", "Name", categoryId.ToString());

            ViewBag.PageSize = pageSize;
            var products = await _repository.GetAllProductsAsync(categoryId);


            if (!products.Any())
            {
                ViewBag.Message = "هیچ محصولی یافت نشد.";
                return View();
            }

            var productsDto = _mapper.Map<List<ProductDTO>>(products);
            var paginated = productsDto.ToPagedList(pageNumber, pageSize);
            return View(paginated);
        }


        // نمایش فرم ایجاد محصول جدید
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _repository.GetAllCategoriesAsync(), "Id", "Name");

            return View();
        }

        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _repository.GetAllCategoriesAsync(), "Id", "Name");
                return View(productDto);
            }

            var result = await _productService.CreateProduct(productDto);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(productDto);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }


        //// دریافت اطلاعات یک محصول برای ویرایش
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _repository.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _repository.GetAllCategoriesAsync(), "Id", "Name", product.CategoryId);

            var productsDto = _mapper.Map<EditProductDto>(product);

            return View(productsDto);
        }

        //// ویرایش محصول
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductDto productDto)
        {
            Product product = await _repository.GetProductByIdAsync(productDto.Id);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _repository.GetAllCategoriesAsync(), "Id", "Name");
                return View(productDto);
            }

            var result = await _productService.EditProduct(productDto);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(productDto);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index", new {product.Id});
        }

        [HttpPost]
        [Route("Upload/ProductImages")] // Ensure this route matches your Dropzone.js form action
        public async Task<IActionResult> UploadImages(List<IFormFile> file) // Dropzone.js sends files under the name 'file' by default
        {
            if (file == null || file.Count == 0)
            {
                return BadRequest("هیچ فایلی برای آپلود دریافت نشد.");
            }

            List<string> uploadedFiles = new List<string>();
            foreach (var formFile in file)
            {
                string imageNameOrError = await _productService.HandleImageUpload(formFile);

                if (imageNameOrError.StartsWith("فرمت فایل نامعتبر است.") || imageNameOrError.StartsWith("حجم فایل نباید بیشتر از"))
                {
                    return BadRequest(imageNameOrError); // Return error message to frontend
                }
                uploadedFiles.Add($"/media/products/Gallery/{imageNameOrError}"); // Construct public URL path
            }

            return Ok(new { message = "تصاویر با موفقیت آپلود شدند.", files = uploadedFiles }); // Return success with file paths
        }


        //// حذف یک محصول
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id is 0)
            {
                TempData["Error"] = "محصول پیدا نشد!";
                return RedirectToAction("Index");
            }

            Product product = await _repository.GetProductByIdAsync(id);

            await _productService.DeleteProduct(product);

            TempData["Success"] = "محصول با موفقیت حذف شد!";
            
            return RedirectToAction("Index");
        }

        //public void Recorderproducts(int[] id)
        //{
        //    int count = 1;

        //    foreach(var productId in id)
        //    {
        //        var product = _context.products.Find(productId);

        //        product.Order = count;

        //        _context.SaveChanges();

        //        count++;
        //    }
        //}

        // محصول خطا
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

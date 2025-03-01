using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Application.Common;
using CMS.Application.Products.Dtos;
using CMS.Application.Services;
using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Products.Gallery;
using CMS.Domain.Admin.Repository;
using CMS.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using X.PagedList.Extensions;

namespace CMS.Controllers
{
    public class ProductController(ILogger<ProductController> logger, IProductRepository repository, IMapper mapper, IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly ILogger<ProductController> _logger = logger;
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        // نمایش لیست محصولات
        public async Task<IActionResult> Index(int? page, string slug = "", int pageSize = 16)
        {
            int pageNumber = page ?? 1;
            ViewBag.PageSize = pageSize;
            ViewBag.CategorySlug = slug;

            IEnumerable<Product> productList;

            if (slug is "")
            {
                productList = await _repository.GetAllProductsAsync(0);

            }
            else
            {
                productList = await _repository.GetProductsByCategory(slug);
            }

            if (!productList.Any())
            {
                ViewBag.Error = "هیچ محصولی یافت نشد.";
                return View(new List<ProductDTO>().ToPagedList(pageNumber, pageSize));
            }


            var productsDto = _mapper.Map<List<ProductDTO>>(productList);
            var paginatedProducts = productsDto.ToPagedList(pageNumber, pageSize);

            return View(paginatedProducts);
        }

        public async Task<IActionResult> Details(string slug = "")
        {
            Product? product = await _repository.GetProductBySlugAsync(slug);

            if (product is null)
                return RedirectToAction("Index");

            IEnumerable<ProductGallery>? pGallery = await _repository.GetProductGalleyAsync(product.Id);

            List<string> galleryPath = pGallery
                .Select(g => $"/media/products/gallery/{g.Image}") // فقط یک بار مسیر را اضافه کن
                .ToList() ?? new List<string>();

            ViewBag.GalleryPath = galleryPath;

            var productsDto = _mapper.Map<ProductDTO>(product);


            return View(productsDto);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

using CMS.Application.Services;
using CMS.Domain.Admin.Repository;
using Microsoft.AspNetCore.Mvc;
using PersianDate;
using static System.Net.Mime.MediaTypeNames;

namespace CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductGalleryController : Controller
    {
        private readonly IProductGalleryService _service;
        private readonly IProductGalleryRepository _repository;

        public ProductGalleryController(IProductGalleryService service, IProductGalleryRepository repository)
        {
            _service = service;
            _repository = repository;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.ProductId = id;
            var images = await _repository.GetAllGallery(id);
            if (!images.Any())
            {
                ViewBag.Error = "تصویری پیدا نشد";
            }
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImages(int productId, List<IFormFile> files)
        {
            var product = await _repository.GetProductByIdAsync(productId);

            if (product is null)
            {
                TempData["Error"] = "حداقل یک تصویر انتخاب کنید";
                return RedirectToAction("Index", new { id = productId });
            }


            if (files is null || files.Count <= 0)
            {
                TempData["Error"] = "حداقل یک تصویر انتخاب کنید";
                return RedirectToAction("Index", new {id = productId } );
            }

            var result = await _service.AddProductGallery(product.Id, files);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", new { id = productId });
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index", new { id = productId });

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "تصویر یافت نشد";
                return RedirectToAction("Index");
            }

            var galleryItem = await _repository.GetGalleryImageByIdAsync(id);

            var productId = galleryItem.ProductId;

            var result = await _service.DeleteProductGallery(id);

            if (!result.Success )
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", new { id = productId });
            }
;            return RedirectToAction("Index", new {id = productId });
        }
    }
}

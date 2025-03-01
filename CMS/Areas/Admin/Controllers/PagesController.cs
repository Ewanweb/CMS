using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Application.Pages.Dtos;
using CMS.Application.Pages.Services;
using CMS.Application.Products.Dtos;
using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;
        private readonly IRepository<Page> _repository;
        private readonly PageService _service;
        private readonly IMapper _mapper;

        public PagesController(ILogger<PagesController> logger, IRepository<Page> repository, PageService service, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        // نمایش لیست صفحات
        public async Task<IActionResult> Index()
        {
            IEnumerable<Page> pages = await _repository.GetAllAsync();

            var sortedPages = pages.OrderBy(p => p.Order).ToList();

            return View(sortedPages);
        }

        // نمایش فرم ایجاد صفحه جدید
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ایجاد صفحه جدید
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageDto pageDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(pageDto);
            }

            var result = await _service.CreatePage(pageDto);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(pageDto);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }

        // دریافت اطلاعات یک صفحه برای ویرایش 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var page = await _repository.GetByIdAsync(id);

            if (page is null) return NotFound();

            var pageDto = _mapper.Map<PageDto>(page);

           return View(pageDto);
        }

        //// ویرایش صفحه
        [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(PageDto pageDto)
       {
           if (!ModelState.IsValid)
           {
               TempData["Error"] = "لطفاً اطلاعات را به درستی وارد کنید.";
               return View(pageDto);
           }

           var result = await _service.EditPage(pageDto);


           if (!result.Success)
           {
               ModelState.AddModelError("", result.Message);
               return View(pageDto);
           }

           TempData["Success"] = result.Message;
           return RedirectToAction("Index");
        }

        // حذف یک صفحه
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeletePage(id);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                TempData["Error"] = "این صفحه وجود ندارد یا حذف آن مجاز نیست";

                return RedirectToAction("Index");
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RecorderPages(int[] sortedIds)
        {
            if (sortedIds == null || sortedIds.Length == 0)
            {
                return BadRequest("لیست صفحات معتبر نیست.");
            }

            int order = 1;
            foreach (var id in sortedIds)
            {
                var page = _repository.GetById(id);
                if (page != null)
                {
                    page.Order = order;
                    _repository.Update(page);
                    order++;
                }
            }

            _repository.Save(); // ذخیره در پایگاه داده

            return Ok();
        }


        // صفحه خطا
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

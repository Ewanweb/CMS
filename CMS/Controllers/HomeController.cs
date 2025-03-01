using CMS.Domain.Admin.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageRepository _repository;

        public HomeController(IPageRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string slug = "")
        {
            slug = slug.IsNullOrEmpty() ? "home" : slug;

            var page = await _repository.GetBySlugAsync(slug);

            if (page is null)
                return NotFound();

            return View(page);
        }
    }
}

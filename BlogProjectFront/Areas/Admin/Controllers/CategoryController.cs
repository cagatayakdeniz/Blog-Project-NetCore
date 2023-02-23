using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Filters;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ICategoryApiService _categoryApiService;
        public CategoryController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryApiService.GetAllAsync());
        }

        [JwtAuthorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Create(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [JwtAuthorize]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);
            return View(new CategoryUpdateModel
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
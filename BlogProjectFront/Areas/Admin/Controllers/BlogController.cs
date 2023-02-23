using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Filters;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize]
    public class BlogController: Controller
    {
        private IBlogApiService _blogApiService;
        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            if(ModelState.IsValid)
            {
                await _blogApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var blogList = await _blogApiService.GetDetailById(id);

            return View(new BlogUpdateModel
            {
                Id=blogList.Id,
                Title=blogList.Title,
                ShortDescription=blogList.ShortDescription,
                Description=blogList.Description,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateModel model)
        {
            if(ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _blogApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssignCategory(int id,[FromServices] ICategoryApiService categoryApiService)
        {
            var categories = await categoryApiService.GetAllAsync();
            var blogCategories = (await _blogApiService.GetCategoriesAsync(id)).Select(x=>x.Name);

            TempData["BlogId"] = id;

            List<AssignCategoryModel> list = new List<AssignCategoryModel>();

            foreach(var category in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = category.Id;
                model.CategoryName = category.Name;
                model.Exists = blogCategories.Contains(category.Name);

                list.Add(model);
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> list)
        {
            int id = (int)TempData["blogId"];
            foreach(var item in list)
            {
                if(item.Exists)
                {
                    CategoryBlogModel categoryBlogModel = new CategoryBlogModel();
                    categoryBlogModel.CategoryId = item.CategoryId;
                    categoryBlogModel.BlogId = id;
                    await _blogApiService.AddToCategoryAsync(categoryBlogModel);
                }
                else
                {
                    CategoryBlogModel categoryBlogModel = new CategoryBlogModel();
                    categoryBlogModel.CategoryId = item.CategoryId;
                    categoryBlogModel.BlogId = id;
                    await _blogApiService.DeleteFromCategoryAsync(categoryBlogModel);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
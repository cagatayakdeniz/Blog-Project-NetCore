using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Controllers
{
    public class HomeController: Controller
    {
        private IBlogApiService _blogApiService;
        public HomeController(IBlogApiService blogApiService)
        {
             _blogApiService=blogApiService;   
        }

        public async Task<IActionResult> Index(string s)
        {
            if(!string.IsNullOrWhiteSpace(s))
            {
                ViewBag.SearchString = s;
                return View(await _blogApiService.SearchAsync(s));
            }
            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> GetAllByCategoryId(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View(await _blogApiService.GetAllByCategoryId(categoryId));
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            ViewBag.Comments = await _blogApiService.GetCommentsAsync(id,null);
            return View(await _blogApiService.GetDetailById(id));
        }

        public async Task<IActionResult> AddComment(CommentAddModel commentAddModel)
        {
            await _blogApiService.AddCommentAsync(commentAddModel);
            return RedirectToAction("blogDetail",new {id=commentAddModel.BlogId});
        }
    }
}
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.ViewComponents
{
    public class CategoryList: ViewComponent
    {
        private ICategoryApiService _categoryApiService;
        public CategoryList(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_categoryApiService.GetAllWithBlogsCount().Result);
        }
    }
}
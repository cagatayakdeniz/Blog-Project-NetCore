using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IBlogService _blogService;
        public ImagesController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBlogeImageById(int id)
        {
            var blog = await _blogService.FindByIdAsync(id);
            if (string.IsNullOrEmpty(blog.ImagePath))
            {
                return BadRequest("Resim Yok.");
            }
            return File($"img/{blog.ImagePath}", "image/jpeg");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogProject.Business.Abstract;
using BlogProject.DTO.DTOs;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.CustomFilters;
using BlogProject.WebApi.Enums;
using BlogProject.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private IBlogService _blogService;
        private IMapper _mapper;
        private ICommentService _commentService;
        public BlogsController(IBlogService blogService, IMapper mapper, ICommentService commentService)
        {
            _commentService = commentService;
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<BlogListDto>>(await _blogService.GetAllSortedByPostedTimeAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<BlogListDto>(await _blogService.FindByIdAsync(id)));
        }

        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Create([FromForm] BlogAddModel blogAddModel)
        {
            string[] resimUzantilari = new string[]{"image/bmp","image/jpeg","image/gif","image/png"};
            string dosyaTuru = blogAddModel.Image.ContentType;
            foreach (string u in resimUzantilari)
            {
                if (dosyaTuru == u)
                {
                    var uploadModel = await UploadFileAsync(blogAddModel.Image, u);
                    if (uploadModel.UploadState == UploadState.Success)
                    {
                        blogAddModel.ImagePath = uploadModel.newName;
                        await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                        return Created("", blogAddModel);
                    }
                    else if (uploadModel.UploadState == UploadState.NotExist)
                    {
                        await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                        return Created("", blogAddModel);
                    }
                    else
                    {
                        return BadRequest(uploadModel.ErrorMessage);
                    }
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Update(int id, [FromForm] BlogUpdateModel blogUpdateModel)
        {
            if (id != blogUpdateModel.Id)
            {
                return BadRequest("Geçersiz Id değeri");
            }

            var uploadModel = await UploadFileAsync(blogUpdateModel.Image, "image/jpeg");
            var updatedBlog = await _blogService.FindByIdAsync(id);

            if (uploadModel.UploadState == UploadState.Success)
            {
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Title = blogUpdateModel.Title;
                updatedBlog.ImagePath = uploadModel.newName;

                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else if (uploadModel.UploadState == UploadState.NotExist)
            {
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Title = blogUpdateModel.Title;

                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }


        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.FindByIdAsync(id);
            await _blogService.DeleteAsync(blog);
            return NoContent();
        }

        [HttpPost("[action]")]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> AddToCategory(CategoryBlogDto categoryBlogDto)
        {
            await _blogService.AddToCategory(categoryBlogDto);
            return Created("", categoryBlogDto);
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> DeleteFromCategory([FromQuery]CategoryBlogDto categoryBlogDto)
        {
            await _blogService.DeleteFromCategory(categoryBlogDto);
            return NoContent();
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(int categoryId)
        {
            return Ok(await _blogService.GetAllByCategoryIdAsync(categoryId));
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> GetCategories(int id)
        {
            return Ok(_mapper.Map<List<CategoryListDto>>(await _blogService.GetCategoriesAsync(id)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastFive()
        {
            return Ok(_mapper.Map<List<Blog>>(await _blogService.GetLastFiveAsync()));
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> GetComments([FromRoute]int id, [FromQuery] int? parentId)
        {
            return Ok(_mapper.Map<List<CommentListDto>>(await _commentService.GetAllWithSubCommentsAsync(id, parentId)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string s)
        {
            var searchBlogs = await _blogService.SearchAsync(s);
            if(searchBlogs==null)
            {
                return NotFound("Blog bulunamadı");
            }
            return Ok(_mapper.Map<List<BlogListDto>>(searchBlogs));
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> AddComment(CommentAddDto commentAddDto)
        {
            await _commentService.AddAsync(_mapper.Map<Comment>(commentAddDto));
            return Created("", commentAddDto);
        }
    }
}
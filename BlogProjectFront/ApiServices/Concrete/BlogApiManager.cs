using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Extensions;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogProjectFront.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private HttpClient _httpClient;
        public BlogApiManager(HttpClient httlpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httlpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:52947/api/blogs/");
        }

        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>
                (await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetAllByCategoryId(int categoryId)
        {
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{categoryId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<BlogListModel> GetDetailById(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }
        public async Task AddAsync(BlogAddModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image != null)
            {
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BlogAddModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));
            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);
        }

        public async Task UpdateAsync(BlogUpdateModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image != null)
            {
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BlogAddModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;
            formData.Add(new StringContent(model.Id.ToString()),nameof(BlogUpdateModel.Id));

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));
            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", formData);
        }

        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.DeleteAsync($"{id}");
        }

        public async Task<List<CategoryListModel>> GetCategoriesAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}/GetCategories");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetLastFiveAsync()
        {
            var responseMessage = await _httpClient.GetAsync("GetLastFive");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task AddToCategoryAsync(CategoryBlogModel categoryBlogModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            var jsonData = JsonConvert.SerializeObject(categoryBlogModel);
            var stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            await _httpClient.PostAsync("AddToCategory",stringContent);
        }

        public async Task DeleteFromCategoryAsync(CategoryBlogModel categoryBlogModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            
            await _httpClient.DeleteAsync
            ($"DeleteFromCategory?{nameof(CategoryBlogModel.CategoryId)}={categoryBlogModel.CategoryId}&{nameof(CategoryBlogModel.BlogId)}={categoryBlogModel.BlogId}");
        }

        public async Task<List<BlogListModel>> SearchAsync(string s)
        {
            var responseMessage = await _httpClient.GetAsync($"Search?s={s}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<CommentListModel>> GetCommentsAsync(int blogId, int? parentCommentId)
        {
            var responseMessage = await _httpClient.GetAsync($"{blogId}/GetComments?parentId={parentCommentId}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        [HttpPost]
        public async Task AddCommentAsync(CommentAddModel commentAddModel)
        {
            var jsonData = JsonConvert.SerializeObject(commentAddModel);
            var content = new StringContent(jsonData,Encoding.UTF8,"application/json");

            await _httpClient.PostAsync("AddComment",content);
        }
    }
}
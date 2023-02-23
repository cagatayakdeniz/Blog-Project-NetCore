using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogProjectFront.ApiServices.Concrete
{
    public class CategoryApiManager : ICategoryApiService
    {
        private HttpClient _httpClient;
        private IHttpContextAccessor _httpContextAccessor;
        public CategoryApiManager(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("http://localhost:52947/api/categories/");
        }

        public async Task AddAsync(CategoryAddModel model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer",_httpContextAccessor.HttpContext.Session.GetString("token"));

            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            await _httpClient.PostAsync("",stringContent);
        }

        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer",_httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.DeleteAsync($"{id}");
        }

        public async Task<List<CategoryListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<CategoryWithBlogsCountModel>> GetAllWithBlogsCount()
        {
            var responseMessage = await _httpClient.GetAsync("GetWithBlogsCount");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryWithBlogsCountModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<CategoryListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryListModel>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task UpdateAsync(CategoryUpdateModel model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("Bearer",_httpContextAccessor.HttpContext.Session.GetString("token"));

            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            await _httpClient.PutAsync($"{model.Id}",stringContent);
        }
    }
}
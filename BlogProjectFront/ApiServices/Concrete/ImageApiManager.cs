using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;

namespace BlogProjectFront.ApiServices.Concrete
{
    public class ImageApiManager : IImageApiService
    {
        private HttpClient _httpClient;
        public ImageApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:52947/api/images/");
        }
        public async Task<string> GetBlogImageById(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"GetBlogeImageById/{id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            }
            return null;
        }
    }
}
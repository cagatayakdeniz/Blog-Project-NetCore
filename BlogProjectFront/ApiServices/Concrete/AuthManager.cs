using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogProjectFront.ApiServices.Concrete
{
    public class AuthManager : IAuthApiService
    {
        private HttpClient _httpClient;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress=new Uri("http://localhost:52947/api/Auth/");
        }

        public async Task<bool> SignInAsync(AppUserLoginModel appUserLoginModel)
        {
            var jsonData = JsonConvert.SerializeObject(appUserLoginModel);
            var stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMessage = await _httpClient.PostAsync("SignIn",stringContent);

            if(responseMessage.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<AccessTokenModel>(await responseMessage.Content.ReadAsStringAsync());
                _httpContextAccessor.HttpContext.Session.SetString("token",token.Token);
                return true;
            }

            return false;
        }
    }
}
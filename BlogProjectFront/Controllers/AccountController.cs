using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Controllers
{
    public class AccountController: Controller
    {
        private IAuthApiService _authApiService;
        public AccountController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginModel appUserLoginModel)
        {
            if(await _authApiService.SignInAsync(appUserLoginModel))
            {
                return RedirectToAction("Index","Blog", new {@area="Admin"});
            }
            return View();
        }
    }
}
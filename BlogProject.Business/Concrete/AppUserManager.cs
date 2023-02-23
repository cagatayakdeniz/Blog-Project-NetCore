using BlogProject.Business.Abstract;
using BlogProject.DataAccess.Abstract;
using BlogProject.DTO.DTOs;
using BlogProject.Entities.Concrete;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class AppUserManager: GenericManager<AppUser>, IAppUserService
    {
        private IGenericDal<AppUser> _genericDal;
        public AppUserManager(IGenericDal<AppUser> genericDal): base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto)
        {
            return await _genericDal.GetAsync(x => x.UserName == appUserLoginDto.UserName
                                                && x.Password == appUserLoginDto.Password);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _genericDal.GetAsync(x => x.UserName == userName);
        }
    }
}

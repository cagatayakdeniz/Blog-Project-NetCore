using BlogProject.DataAccess.Abstract;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EfCore.Repositories
{
    public class EfAppUserRepository: EfGenericRepository<AppUser>, IAppUserDal
    {
    }
}

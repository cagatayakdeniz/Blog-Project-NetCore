using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Utilities.Jwt
{
    public interface IJwtService
    {
        JwtToken GenerateJwt(AppUser user);
    }
}

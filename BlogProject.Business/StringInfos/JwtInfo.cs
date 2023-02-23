using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.StringInfos
{
    public class JwtInfo
    {
        public const string Issuer = "http://localhost:52947";
        public const string Audience = "http://localhost:5000";
        public const string SecurityKey = "cagataycagatay123";
        public const double TokenExpiration = 10;
    }
}

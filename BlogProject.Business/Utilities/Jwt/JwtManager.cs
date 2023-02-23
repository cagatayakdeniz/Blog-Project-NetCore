using BlogProject.Business.StringInfos;
using BlogProject.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogProject.Business.Utilities.Jwt
{
    public class JwtManager : IJwtService
    {
        public JwtToken GenerateJwt(AppUser user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtInfo.Issuer, audience: JwtInfo.Audience,
                notBefore: DateTime.Now, claims: SetClaims(user),signingCredentials:signingCredentials,
                expires: DateTime.Now.AddMinutes(JwtInfo.TokenExpiration));

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            
            JwtToken jwtToken = new JwtToken();
            jwtToken.Token = handler.WriteToken(token);
            return jwtToken;
        }

        private List<Claim> SetClaims(AppUser user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            return claims;
        }
    }
}

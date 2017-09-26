using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AutoRespect.Infrastructure.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Models;

namespace AutoRespect.AuthorizationServer.Business
{
    public class TokenIssuer : ITokenIssuer
    {
        public Task<Result<string>> Release(User user)
        {
            var identity = CreateClaims(user);

            var jwt = new JwtSecurityToken(                
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(AuthOptions.LifeTime),
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(AuthOptions.SecretKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Task.FromResult(CreateToken(encodedJwt));
        }

        private Result<string> CreateToken(string encodedJwt) => encodedJwt;

        private ClaimsIdentity CreateClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim (ClaimsIdentity.DefaultNameClaimType, user.Login.Value),
                new Claim ("Id", user.Id.ToString())
            };

            return new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );
        }
    }
}

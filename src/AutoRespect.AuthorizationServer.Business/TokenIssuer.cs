using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using System;
using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AutoRespect.AuthorizationServer.Business
{
    public class TokenIssuer : ITokenIssuer
    {
        public Task<Result<ErrorType, Token>> Release(User user)
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

        private Result<ErrorType, Token> CreateToken(string encodedJwt) => 
            new Token {
                Value = encodedJwt
            };

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

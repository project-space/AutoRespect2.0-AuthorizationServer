using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRespect.AuthorizationServer.Design.Models
{
    public static class AuthOptions
    {
        public static string Issuer = "";
        public static string Audience = "";
        public static TimeSpan LifeTime = new TimeSpan(0, 1, 0);
        public static SymmetricSecurityKey SecretKey = 
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("HIDE IN PRIVATE SETTINGS")); // TODO: 
    }
}

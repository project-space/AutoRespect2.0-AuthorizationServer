﻿using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using AutoRespect.Infrastructure.ErrorHandling;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess
{
    public interface IUserGetter
    {
        Task<Result<User>> Get(Login login);
    }
    public class UserNotFound : Error
    {
        public Login Login { get; private set; }

        public UserNotFound(Login login) : base("AA5E0914-E4E2-4F27-8592-90980F326D1F", $"User with login [{login.Value}]")
        {
            Login = login;
        }
    }
}

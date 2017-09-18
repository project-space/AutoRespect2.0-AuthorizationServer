using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using System;
using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;

namespace AutoRespect.AuthorizationServer.Business
{
    public class UserRegistrar : IUserRegistrar
    {
        private readonly IUserSaver userSaver;
        private readonly IUserGetter userGetter;
        private readonly ITokenIssuer tokenIssuer;

        public UserRegistrar(
            IUserSaver userSaver,
            IUserGetter userGetter,
            ITokenIssuer tokenIssuer)
        {
            this.userSaver = userSaver;
            this.userGetter = userGetter;
            this.tokenIssuer = tokenIssuer;
        }

        public async Task<Result<ErrorType, Token>> Register(UserCredentials credentials)
        {
            var login = UserLogin.Create (credentials.Login);
            var password = UserPassword.Create (credentials.Password);

            if (login.IsFailure) return login.Failure;
            if (password.IsFailure) return password.Failure; 

            var loginIsBussy = await LoginIsBussy(login.Success);
            if (loginIsBussy.IsFailure) return loginIsBussy.Failure;
            if (loginIsBussy.Success) return ErrorType.LoginIsBussy;

            var user = new User { Login = login.Success, Password = password.Success };
            var save = await userSaver.Save (user);

            if (save.IsFailure) return save.Failure;

            user.Id = save.Success;

            return await tokenIssuer.Release(user);
        }

        private async Task<Result<ErrorType, bool>> LoginIsBussy(UserLogin login)
        {
            var user = await userGetter.Get(login);
            if (user.IsFailure) 
            {
                if (user.Failure == ErrorType.UserNotFound)
                    return false;

                return user.Failure;
            }

            return true;
        }
    }
}

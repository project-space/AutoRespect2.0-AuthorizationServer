using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;

namespace AutoRespect.AuthorizationServer.Business
{
    public class Authenticator : IAuthenticator
    {
        private readonly IUserPasswordAuditor passwordAuditor;
        private readonly IUserGetter userGetter;
        private readonly ITokenIssuer tokenIssuer;

        public Authenticator(
            IUserPasswordAuditor passwordAuditor,
            IUserGetter userGetter,
            ITokenIssuer tokenIssuer)
        {
            this.passwordAuditor = passwordAuditor;
            this.userGetter = userGetter;
            this.tokenIssuer = tokenIssuer;
        }

        public async Task<Result<ErrorType, Token>> Authenticate(UserCredentials credentials)
        {
            var login = UserLogin.Create(credentials.Login);
            if (login.IsFailure) return login.Failure;

            var password = UserPassword.Create(credentials.Password);
            if (password.IsFailure) return password.Failure;

            var audit = await passwordAuditor.Audit(login.Success, password.Success);
            if (audit.IsFailure) return audit.Failure;

            var user = await userGetter.Get(login.Success);
            if (user.IsFailure) return user.Failure;

            return await tokenIssuer.Release(user.Success);
        }
    }
}

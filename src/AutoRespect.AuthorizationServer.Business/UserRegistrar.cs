using System.Linq;
using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using AutoRespect.Infrastructure.DI.Design;
using AutoRespect.Infrastructure.DI.Design.Attributes;
using AutoRespect.Infrastructure.ErrorHandling;
using AutoRespect.Infrastructure.OAuth.Jwt;

namespace AutoRespect.AuthorizationServer.Business
{
    [DI(LifeCycleType.Singleton)]
    public class UserRegistrar : IUserRegistrar
    {
        private readonly IUserSaver userSaver;
        private readonly IUserGetter userGetter;
        private readonly IJwtIssuer tokenIssuer;

        public UserRegistrar(
            IUserSaver userSaver,
            IUserGetter userGetter,
            IJwtIssuer tokenIssuer)
        {
            this.userSaver = userSaver;
            this.userGetter = userGetter;
            this.tokenIssuer = tokenIssuer;
        }

        public async Task<Result<string>> Register(Credentials credentials)
        {
            var loginIsBussy = await LoginIsBussy(credentials.Login);
            if (loginIsBussy.IsFailure) return loginIsBussy.Failures;
            if (loginIsBussy.Value) return new LoginAlreadyBussy(credentials.Login);

            var user = new User { Login = credentials.Login, Password = credentials.Password };
            var save = await userSaver.Save (user);

            if (save.IsFailure) return save.Failures;

            user.Id = save.Value;

            return tokenIssuer.Release(new JwtPayload {
                AccountId = user.Id
            });
        }

        //TODO: Move to LoginIsBussyChecker
        private async Task<Result<bool>> LoginIsBussy(Login login)
        {
            var user = await userGetter.Get(login);
            if (user.IsFailure) 
            {
                if (user.Failures.Any(e => e is UserNotFound))
                    return false;

                return 
                    user.Failures;
            }

            return true;
        }
    }
}

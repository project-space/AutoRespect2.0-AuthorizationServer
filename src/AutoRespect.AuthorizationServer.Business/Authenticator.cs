using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.DI.Design;
using AutoRespect.Infrastructure.DI.Design.Attributes;
using AutoRespect.Infrastructure.ErrorHandling;
using AutoRespect.Infrastructure.OAuth.Jwt;

namespace AutoRespect.AuthorizationServer.Business
{
    [DI(LifeCycleType.Singleton)]
    public class Authenticator : IAuthenticator
    {
        private readonly IUserPasswordAuditor passwordAuditor;
        private readonly IUserGetter userGetter;
        private readonly IJwtIssuer tokenIssuer;

        public Authenticator(
            IUserPasswordAuditor passwordAuditor,
            IUserGetter userGetter,
            IJwtIssuer tokenIssuer)
        {
            this.passwordAuditor = passwordAuditor;
            this.userGetter = userGetter;
            this.tokenIssuer = tokenIssuer;
        }

        public async Task<Result<string>> Authenticate(Credentials credentials)
        {
            var audit = await passwordAuditor.Audit(credentials);
            if (audit.IsFailure) return audit.Failures;

            var user = await userGetter.Get(credentials.Login.Value);
            if (user.IsFailure) return user.Failures;

            return tokenIssuer.Release(new JwtPayload
            {
                AccountId = user.Value.Id
            });
        }
    }
}

using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.DI.Design;
using AutoRespect.Infrastructure.DI.Design.Attributes;
using AutoRespect.Infrastructure.Errors.Design;
using AutoRespect.Infrastructure.OAuth.Jwt;

namespace AutoRespect.AuthorizationServer.Business
{
    [DI(LifeCycle.Singleton)]
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

        public async Task<R<string>> Authenticate(Credentials credentials)
        {
            var audit = await passwordAuditor.Audit(credentials);
            if (audit.IsFailure) return audit.Failures;

            var user = await userGetter.Get(credentials.Login.Value);
            if (user.IsFailure) return user.Failures;

            var claims = new JwtClaims
            {
                AccountId    = user.Value.Id,
                AccountLogin = user.Value.Login.Value
            };

            return tokenIssuer.Release(claims);
        }
    }
}

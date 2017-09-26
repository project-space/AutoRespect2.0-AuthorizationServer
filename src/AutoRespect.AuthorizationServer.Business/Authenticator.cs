using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.Infrastructure.ErrorHandling;

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

        public async Task<Result<string>> Authenticate(Credentials credentials)
        {
            var audit = await passwordAuditor.Audit(credentials);
            if (audit.IsFailure) return audit.Failures;

            var user = await userGetter.Get(credentials.Login.Value);
            if (user.IsFailure) return user.Failures;

            return await tokenIssuer.Release(user.Value);
        }
    }
}

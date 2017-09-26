using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.Infrastructure.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Business
{
    public class UserPasswordAuditor : IUserPasswordAuditor
    {
        private readonly IUserGetter userGetter;

        public UserPasswordAuditor(IUserGetter userGetter)
        {
            this.userGetter = userGetter;
        }

        public async Task<Result<bool>> Audit(Credentials credentials)
        {
            var user = await userGetter.Get(credentials.Login);
            if (user.IsFailure) return user.Failures;

            if (user.Value.Password.Value.Equals(credentials.Password.Value)) return true;
            else return new WrongLoginOrPassword();
        }
    }
}

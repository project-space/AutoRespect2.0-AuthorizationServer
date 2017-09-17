using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using System;
using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;

namespace AutoRespect.AuthorizationServer.Business
{
    public class UserPasswordAuditor : IUserPasswordAuditor
    {
        private readonly IUserGetter userGetter;

        public UserPasswordAuditor(IUserGetter userGetter)
        {
            this.userGetter = userGetter;
        }

        public async Task<Result<ErrorType, bool>> Audit(UserLogin login, UserPassword password)
        {
            var user = await userGetter.Get(login);
            if (user.IsFailure) return user.Failure;

            if (user.Success.Password.Value.Equals(password.Value)) return true;
            else return ErrorType.WrongLoginOrPassword;
        }
    }
}

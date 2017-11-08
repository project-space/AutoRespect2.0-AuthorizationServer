using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using AutoRespect.Infrastructure.DataAccess.Design;
using AutoRespect.Infrastructure.DI.Design;
using AutoRespect.Infrastructure.DI.Design.Attributes;
using AutoRespect.Infrastructure.Errors.Design;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    [DI(LifeCycle.Singleton)]
    public class UserGetter : IUserGetter
    {
        private readonly IIdentityServerDb db;

        public UserGetter(IIdentityServerDb db)
        {
            this.db = db;
        }

        public async Task<R<User>> Get(Login login)
        {
            var query = new Query(
                @"select * from  Account where Login = @Login",
                new { Login = login.Value }
            );

            var user = await db.QuerySingleOrDefault<User>(query);
            if (user == null) return new UserNotFound(login.Value);

            return user;
        }
    }
}
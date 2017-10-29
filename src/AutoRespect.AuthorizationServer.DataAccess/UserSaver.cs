using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.DataAccess.Design;
using AutoRespect.Infrastructure.DI.Design;
using AutoRespect.Infrastructure.DI.Design.Attributes;
using AutoRespect.Infrastructure.ErrorHandling;
using Dapper;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    [DI(LifeCycleType.Singleton)]
    public class UserSaver : IUserSaver
    {
        private readonly IIdentityServerDb db;

        public UserSaver(IIdentityServerDb db)
        {
            this.db = db;
        }

        public async Task<Result<int>> Save(User user)
        {
            const string sqlScript = @"
                if exists (select 1 from Account where Id = @Id) 
                begin
                    update
                        Account
                    set
                        Password = @Password,
                        Login = @Login
                    where
                        Id = @Id
                end
                else begin
                    insert into Account
                        (Login, Password)
                    values
                        (@Login, @Password)

                    set @Id = scope_identity()
                end

                select @Id";

            var query = new Query(sqlScript, new {
                Id       = user.Id,
                Login    = user.Login.Value,
                Password = user.Password.Value
            });

            return await db.Execute(query);
        }
    }
}

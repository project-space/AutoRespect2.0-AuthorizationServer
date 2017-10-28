using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.ErrorHandling;
using Dapper;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    public class UserSaver : IUserSaver
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AutoRespect.IdentityServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<int>(sqlScript, new { 
                    Id       = user.Id,
                    Login    = user.Login.Value,
                    Password = user.Password.Value 
                });
            }
        }
    }
}

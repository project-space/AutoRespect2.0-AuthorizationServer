using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using AutoRespect.Infrastructure.ErrorHandling;
using Dapper;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    public class UserGetter : IUserGetter
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AutoRespect.IdentityServer;Integrated Security=SSPI;";

        public async Task<Result<User>> Get(Login login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sqlScript = @"select * from  Account where Login = @Login";

                var user = await connection.QueryFirstOrDefaultAsync<User>(sqlScript, new { Login = login.Value });
                if (user == null) return new UserNotFound(login);

                return user;
            }
        }
    }
}
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using AutoRespect.Infrastructure.ErrorHandling;
using static AutoRespect.AuthorizationServer.DataAccess.Sql.Scripts;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    public class UserGetter : IUserGetter
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AutoRespect.AutorizationServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<Result<User>> Get(Login login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(UserGetter_Get, new { Login = login.Value });
                if (user == null) return new UserNotFound(login);

                return user;
            }
        }
    }
}

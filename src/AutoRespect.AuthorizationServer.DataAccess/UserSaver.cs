using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using AutoRespect.Infrastructure.ErrorHandling;
using static AutoRespect.AuthorizationServer.DataAccess.Sql.Scripts;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    public class UserSaver : IUserSaver
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AutoRespect.IdentityServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<Result<int>> Save(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<int>(
                    UserSaver_Save, 
                    new { 
                        Id = user.Id,
                        Login = user.Login.Value,
                        Password = user.Password.Value 
                    });
            }
        }
    }
}

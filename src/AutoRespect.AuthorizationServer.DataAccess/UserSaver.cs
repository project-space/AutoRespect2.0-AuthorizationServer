using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using static AutoRespect.AuthorizationServer.DataAccess.Sql.Scripts;

namespace AutoRespect.AuthorizationServer.DataAccess
{
    public class UserSaver : IUserSaver
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AutoRespect.AutorizationServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<Result<ErrorType, int>> Save(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<int>(
                    UserGetter_Get, 
                    new { 
                        Id = user.Id,
                        Login = user.Login.Value,
                        Password = user.Password.Value 
                    });
            }
        }
    }
}

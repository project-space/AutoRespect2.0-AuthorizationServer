using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.ErrorHandling;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess
{
    public interface IUserSaver
    {
        Task<Result<int>> Save(User user);
    }
}

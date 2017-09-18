using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess
{
    public interface IUserSaver
    {
        Task<Result<ErrorType, int>> Save(User user);
    }
}

using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess
{
    public interface IUserGetter
    {
        Task<Result<ErrorType, User>> Get(UserLogin login);
    }
}

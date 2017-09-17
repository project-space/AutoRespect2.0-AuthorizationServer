using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface ITokenIssuer
    {
        Task<Result<ErrorType, Token>> Release(User user);
    }
}

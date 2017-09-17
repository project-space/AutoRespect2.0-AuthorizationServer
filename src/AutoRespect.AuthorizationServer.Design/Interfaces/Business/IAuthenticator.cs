using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IAuthenticator
    {
        Task<Result<ErrorType, Token>> Authenticate(UserCredentials credentials);
    }
}

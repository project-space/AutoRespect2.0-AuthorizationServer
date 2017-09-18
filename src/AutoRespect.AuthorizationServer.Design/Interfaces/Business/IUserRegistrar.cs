using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Models;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IUserRegistrar
    {
        Task<Result<ErrorType, Token>> Register(UserCredentials credentials);
    }
}

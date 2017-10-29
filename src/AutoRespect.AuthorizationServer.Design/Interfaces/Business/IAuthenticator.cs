using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.ErrorHandling;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IAuthenticator
    {
        Task<Result<string>> Authenticate(Credentials credentials);
    }
}

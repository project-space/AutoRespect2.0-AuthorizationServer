using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.Errors.Design;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IAuthenticator
    {
        Task<R<string>> Authenticate(Credentials credentials);
    }
}

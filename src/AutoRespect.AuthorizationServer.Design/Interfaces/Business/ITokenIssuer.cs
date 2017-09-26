using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.ErrorHandling;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface ITokenIssuer
    {
        Task<Result<string>> Release(User user);
    }
}

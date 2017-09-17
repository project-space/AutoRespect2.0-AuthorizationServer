using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using AutoRespect.AuthorizationServer.Design.Primitives;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IUserPasswordAuditor
    {
        Task<Result<ErrorType, bool>> Audit(
            UserLogin login, 
            UserPassword password
        );
    }
}

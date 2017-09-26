using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.ErrorHandling;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IUserPasswordAuditor
    {
        Task<Result<bool>> Audit(Credentials credentials);
    }

    public class WrongLoginOrPassword : Error
    {
        public WrongLoginOrPassword() : base("ADBEC8D0-196D-4E99-80C2-55F33C1F8032", "Wrong login or password")
        {
        }
    }
}

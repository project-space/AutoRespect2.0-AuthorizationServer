using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.Errors.Design;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IUserPasswordAuditor
    {
        Task<R<bool>> Audit(Credentials credentials);
    }

    public class WrongLoginOrPassword : E
    {
        public WrongLoginOrPassword() : base("ADBEC8D0-196D-4E99-80C2-55F33C1F8032", "Wrong login or password")
        {
        }
    }
}

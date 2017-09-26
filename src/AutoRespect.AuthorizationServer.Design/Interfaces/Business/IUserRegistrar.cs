using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Primitives;
using AutoRespect.Infrastructure.ErrorHandling;
using System.Threading.Tasks;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.Business
{
    public interface IUserRegistrar
    {
        Task<Result<string>> Register(Credentials credentials);
    }

    public class LoginAlreadyBussy : Error
    {
        public LoginAlreadyBussy(Login login) : base("941F27AE-A778-40F6-83E8-9F12C5F6ADBB", $"Login [{login.Value}] already bussy")
        {
        }
    }
}

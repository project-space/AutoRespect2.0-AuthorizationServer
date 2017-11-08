using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.Infrastructure.Errors.Design;

namespace AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess
{
    public interface IUserSaver
    {
        Task<R<int>> Save(User user);
    }
}

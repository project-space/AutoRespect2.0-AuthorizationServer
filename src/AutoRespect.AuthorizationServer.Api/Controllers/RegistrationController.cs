using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.Infrastructure.ErrorHandling.AspNetCore;

namespace AutoRespect.AuthorizationServer.Api.Controllers
{
    [Route("api/v1/Registration")]
    public class RegistrationController : Controller
    {
        private readonly IUserRegistrar userRegistrar;

        public RegistrationController(IUserRegistrar userRegistrar)
        {
            this.userRegistrar = userRegistrar;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Credentials credentials) => (await userRegistrar
            .Register(credentials))
            .AsActionResult();            
    }
}
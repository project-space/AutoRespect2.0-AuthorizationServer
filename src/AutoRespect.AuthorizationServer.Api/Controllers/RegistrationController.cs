using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Post([FromBody] Credentials credentials)
        {
            var response = await userRegistrar.Register(credentials);

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Failures);
        }
    }
}
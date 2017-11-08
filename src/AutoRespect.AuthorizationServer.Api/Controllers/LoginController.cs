using System.Threading.Tasks;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Design.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoRespect.AuthorizationServer.Api.Controllers
{
    [Route("api/v1/Login")]
    public class LoginController : Controller
    {
        private readonly IAuthenticator authenticator;

        public LoginController(IAuthenticator authenticator) => this.authenticator = authenticator;

        /// <summary>
        /// Resource Owner Password Credentials Grant
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Credentials credentials)
        {
            var response = await authenticator.Authenticate(credentials);

            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return BadRequest(response.Failures);
        }
    }
}
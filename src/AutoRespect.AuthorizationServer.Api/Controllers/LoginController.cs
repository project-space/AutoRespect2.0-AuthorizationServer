using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using System;

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
            var result = await authenticator.Authenticate(credentials);
            if (result.IsFailure) return BadRequest(result.Failures);
            else return Ok(result.Value);
        }
    }
}
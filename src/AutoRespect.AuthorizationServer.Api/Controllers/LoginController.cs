using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoRespect.AuthorizationServer.Design.Models;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using System;
using AutoRespect.Infrastructure.ErrorHandling.AspNetCore;

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
        public async Task<IActionResult> Post([FromBody] Credentials credentials) => (await authenticator
            .Authenticate(credentials))
            .AsActionResult();
    }
}
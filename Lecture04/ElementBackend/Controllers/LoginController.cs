using System;
using ElementBackend.Models;
using ElementBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElementBackend.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : Controller
    {
        readonly ILogger<LoginController> _Logger;
        readonly LoginService _LoginService;

        public LoginController(
            ILogger<LoginController> logger,
            LoginService loginService)
        {
            _Logger = logger;
            _LoginService = loginService;
        }

        [HttpPost("Login")]
        public ActionResult<TokenModel> Login([FromBody] LoginModel model)
        {
            try
            {
                _Logger.LogInformation($"Login: {model.Login}...");

                var token = _LoginService.Login(model);

                _Logger.LogInformation($"Login OK: {model.Login}");

                return Ok(new TokenModel{Token = token});
            }
            catch (Exception)
            {
                _Logger.LogWarning($"Login Failed: {model.Login}");
                return NotFound(null);
            }
        }

    }
}

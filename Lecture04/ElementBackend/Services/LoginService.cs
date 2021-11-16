using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ElementBackend.Models;
using ElementLib;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ElementBackend.Services
{
    public class LoginService
    {
        readonly IConfiguration _Configuration;
        readonly ILogger<LoginService> _Logger;
        readonly UserRepository _UserRepository;

        public LoginService(
            ILogger<LoginService> logger,
            IConfiguration configuration,
            UserRepository userRepository)
        {
            _Configuration = configuration;
            _Logger = logger;
            _UserRepository = userRepository;
        }

        public string Login(LoginModel model)
        {
            return GenerateToken(model);
        }

        public string GenerateToken(LoginModel model)
        {
            var role = _UserRepository.VerifyUser(model.Login, model.Password);

            if (role == Role.None)
                throw new Exception("Invalid login");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["SECRET_KEY"]));

            var token = new JwtSecurityToken(
                _Configuration["APPLICATION_URL"],
                _Configuration["APPLICATION_URL"],
                new List<Claim>
                {
                    new("role", $"{role}"),
                    new("login", $"{model.Login}"),
                },
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

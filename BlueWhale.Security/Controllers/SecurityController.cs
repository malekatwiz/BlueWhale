using System;
using System.Collections.Generic;
using BlueWhale.Security.Models;
using BlueWhale.Security.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueWhale.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public SecurityController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password) || !ValidateUser(loginModel))
            {
                return Unauthorized();
            }

            return Ok(new { token = _tokenService.Generate(loginModel.Username, loginModel.Password) });
        }

        private static bool ValidateUser(LoginModel loginModel)
        {
            Users.TryGetValue(loginModel.Username, out var password);

            return string.Equals(password, loginModel.Password);
        }

        private static IReadOnlyDictionary<string, string> Users => new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"Malek", "MyPassword"}
        };
    }
}
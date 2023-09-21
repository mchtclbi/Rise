using Microsoft.AspNetCore.Mvc;
using Space.AuthAPI.Models.Request;
using Space.AuthAPI.Service.Interfaces;

namespace Space.Auht.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] CreateTokenRequest request) =>
            Ok(await _authService.CreateToken(request));
    }
}
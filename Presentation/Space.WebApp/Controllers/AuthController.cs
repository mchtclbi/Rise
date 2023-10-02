using Space.WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Space.WebApp.Models.Request;
using Space.WebApp.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Space.WebApp.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var serviceResponse = await _authService.Login(request);

            if (!serviceResponse.IsSuccess)
                return BadRequest();

            Request.HttpContext.Session.SetString(Constant.CookieKey, serviceResponse.Data.Token);

            SetCookie(Constant.CookieKey, serviceResponse.Data.Token, 30);
            SetHttpContextUser(GetClaim(serviceResponse.Data.Token));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Auth/Logout")]
        public IActionResult LogOut()
        {
            RemoveCookie(Constant.CookieKey);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var serviceResponse = await _userService.Register(request);

            if (!serviceResponse.IsSuccess)
                return BadRequest();

            return RedirectToAction("Index");
        }

        private Claim[] GetClaim(string token) => new[]
        {
            new Claim("Token", token),
        };

        private void SetHttpContextUser(Claim[] claims)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "login"));

            identity.AddClaims(claims);

            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true
            });

            HttpContext.User = principal;
        }
    }
}
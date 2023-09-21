using Microsoft.AspNetCore.Mvc;
using Space.UserAPI.Models.Request;
using Space.UserAPI.Service.Interfaces;

namespace Space.User.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("api/user")]
        public IActionResult AddUser([FromBody] AddUserRequest request) => Created("", _userService.Add(request));

        [HttpPost("api/user/confirm")]
        public async Task<IActionResult> UserConfirm([FromBody] UserConfirmRequest request) => Ok(await _userService.UserConfirm(request));
    }
}
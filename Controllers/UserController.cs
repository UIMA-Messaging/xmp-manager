using Microsoft.AspNetCore.Mvc;
using XmpManager.Services;
using XmpManager.Contracts;

namespace XmpManager.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly UserService service;

        public UserController(UserService service) 
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await service.RegisterUser(user);
            return Ok();
        }

        [HttpDelete("unregister/{username}")]
        public async Task<IActionResult> Unregister(string username)
        {
            await service.UnregisterUser(username);
            return Ok();
        }
    }
}

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
            await this.service.RegisterUser(user);
            return Ok();
        }

        [HttpDelete("unregister/{id}")]
        public async Task<IActionResult> Unregister(string id)
        {
            await this.service.UnregisterUser(id);
            return Ok();
        }
    }
}

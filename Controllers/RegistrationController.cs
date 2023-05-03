using Microsoft.AspNetCore.Mvc;
using XmpManager.Services;

namespace XmpManager.Controllers
{
    [Route("registeration")]
    public class RegistrationController : Controller
    {
        private readonly UserService service;

        public RegistrationController(UserService service) 
        {
            this.service = service;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register()
        {
            this.service.RegisterUser(null);
            return Ok();
        }

        [HttpPost]
        [Route("unregister")]
        public IActionResult Unregister()
        {
            throw new NotImplementedException();
        }
    }
}

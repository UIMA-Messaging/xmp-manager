using Microsoft.AspNetCore.Mvc;
using XmpManager.Contracts;
using XmpManager.Services;

namespace XmpManager.Controllers
{
    [Route("rooms")]
    public class RoomController : Controller
    {
        private readonly MucService service;

        public RoomController(MucService service) 
        {
            this.service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task CreateRoom([FromBody] Channel channel)
        {
            await service.CreateMuc(channel);
        }
    }
}

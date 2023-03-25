using Microsoft.AspNetCore.Mvc;
using XmpManager.Contracts;
using XmpManager.Service.Rooms;

namespace XmpManager.Controllers
{
    [Route("rooms")]
    public class RoomController : Controller
    {
        private readonly IMucService service;

        public RoomController(IMucService service) 
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

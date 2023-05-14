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

        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] Channel channel)
        {
            await service.CreateMuc(channel);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            await service.RemoveMuc(id);
            return Ok();
        }
    }
}

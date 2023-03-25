using XmpManager.Clients.Ejabberd;
using XmpManager.Contracts;
using XmpManager.EventBus;

namespace XmpManager.Service.Rooms
{
    public class MucService : IMucService
    {
        private readonly EjabberdClient client;

        public MucService()
        {
            client = new EjabberdClient();
        }

        public async Task CreateMuc(Channel channel)
        {
            await client.CreateMuc(channel.Name);
            await client.SendDirectInvitations(channel.Name);
        }
    }
}

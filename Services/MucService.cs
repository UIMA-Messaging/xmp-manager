using XmpManager.Clients;
using XmpManager.Contracts;

namespace XmpManager.Services
{
    public class MucService
    {
        private readonly EjabberdClient client;

        public MucService(EjabberdClient client)
        {
            this.client = client;
        }

        public async Task CreateMuc(Channel channel)
        {
            await client.CreateMuc(channel.Name);
            await client.SendDirectInvitations(channel.Name);
        }
    }
}

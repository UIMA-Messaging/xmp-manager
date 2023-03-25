using XmpManager.Clients.Ejabberd.Models;

namespace XmpManager.Clients.Ejabberd
{
    public class EjabberdClient
    {
        private readonly Uri baseUrl;
        private readonly string host;
        private readonly string service;
        private readonly HttpClient client;

        public EjabberdClient(string baseUrl = "https://localhost:5443", string host = "localhost", string service = "conference.localhost")
        {
            this.baseUrl = new Uri(baseUrl);
            this.host = host;
            client = new HttpClient();
            this.service = service;
        }

        public async Task CreateMuc(string name)
        {
            var url = new Uri(baseUrl, "/api/create_room_with_opts");
            var createMuc = new CreateMucWithOptions
            {
                Name = name,
                Host = host,
                Service = service,
                Options = new Options()
            };
            await client.PostAsync(url.ToString(), JsonContent.Create(createMuc));
        }

        public async Task SetAffiliance(string room, params string[] jids)
        {
            var url = new Uri(baseUrl, "/api/set_room_affiliation");
            var setAffiliation = new SetRoomAffiliation
            {
                Name = room,
                Service = service,
                Jid = string.Join(':', jids)
            };
            await client.PostAsync(url.ToString(), JsonContent.Create(setAffiliation));
        }

        public async Task SendDirectInvitations(string room, params string[] jids)
        {
            var url = new Uri(baseUrl, "/api/set_room_affiliation");
            var setAffiliation = new SendDirectInvitation
            {
                Name = room,
                Service = service,
                Reason = $"Chat with {jids.Length - 1} others",
                Users = string.Join(':', jids)
            };
            await client.PostAsync(url.ToString(), JsonContent.Create(setAffiliation));
        }

        public async Task RegisterUser(string username, string password)
        {
            var url = new Uri(baseUrl, "/api/register");
            var registerUser = new RegisterUser
            {
                User = username,
                Password = password,
                Host = host
            };
            await client.PostAsync(url.ToString(), JsonContent.Create(registerUser));
        }

        public async Task UnregisterUser(string username)
        {
            var url = new Uri(baseUrl, "/api/unregister");
            var unregisterUser = new RegisterUser
            {
                User = username,
                Host = host
            };
            await client.PostAsync(url.ToString(), JsonContent.Create(unregisterUser));
        }
    }
}

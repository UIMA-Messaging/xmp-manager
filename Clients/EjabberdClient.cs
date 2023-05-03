using Newtonsoft.Json;
using System.Net;

namespace XmpManager.Clients
{
    public class EjabberdClient
    {
        private readonly Uri baseUrl;
        private readonly string host;
        private readonly string service;
        private readonly HttpClient client;

        public EjabberdClient(string baseUrl = "https://localhost:5443", string host = "localhost", string service = "localhost", string adminUsername = "admin", string adminPassword = "admin")
        {
            this.baseUrl = new Uri(baseUrl);
            this.host = host;
            this.service = service;
            var clientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(adminUsername, adminPassword),
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            this.client = new HttpClient(clientHandler);
        }

        public async Task CreateMuc(string name)
        {
            var url = new Uri(baseUrl, "/api/create_room_with_opts");
            var createMuc = new
            {
                name = name,
                host = host,
                service = service,
                pptions = new
                {
                    name = "members_only"
                }
            };
            await client.PostAsync(url.ToString(), new StringContent(JsonConvert.SerializeObject(createMuc)));
        }

        public async Task SetAffiliance(string room, params string[] jids)
        {
            var url = new Uri(baseUrl, "/api/set_room_affiliation");
            var setAffiliation = new
            {
                name = room,
                service = service,
                jid = string.Join(':', jids)
            };
            await client.PostAsync(url.ToString(), new StringContent(JsonConvert.SerializeObject(setAffiliation)));
        }

        public async Task SendDirectInvitations(string room, params string[] jids)
        {
            var url = new Uri(baseUrl, "/api/set_room_affiliation");
            var setAffiliation = new
            {
                name = room,
                service = service,
                reason = $"Chat with {jids.Length - 1} others",
                users = string.Join(':', jids)
            };
            await client.PostAsync(url.ToString(), new StringContent(JsonConvert.SerializeObject(setAffiliation)));
        }

        public async Task RegisterUser(string username, string password)
        {
            var url = new Uri(baseUrl, "/api/register");
            var registerUser = new
            {
                user = username,
                password = password,
                host = host
            };
            await client.PostAsync(url.ToString(), new StringContent(JsonConvert.SerializeObject(registerUser)));
        }

        public async Task UnregisterUser(string username)
        {
            var url = new Uri(baseUrl, "/api/unregister");
            var unregisterUser = new
            {
                user = username,
                host = host
            };
            await client.PostAsync(url.ToString(), new StringContent(JsonConvert.SerializeObject(unregisterUser)));
        }
    }
}

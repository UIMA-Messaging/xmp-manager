using Newtonsoft.Json;
using System.Diagnostics;
using XmpManager.Clients;
using XmpManager.Contracts;
using XmpManager.RabbitMQ;

namespace XmpManager.Services
{
    public class UserService
    {
        private readonly EjabberdClient client;

        public UserService(IRabbitMQListener<User> userRegistrations, EjabberdClient client) 
        {
            this.client = client;
            userRegistrations.OnReceive += (_, user) => RegisterUser(user);
        }

        public async Task RegisterUser(User user)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(user));
            if (user != null)
            {
                await client.RegisterUser(user.Username, user.EphemeralPassword);
            }
        }

        public async Task UnregisterUser(User user)
        {
            await client.UnregisterUser(user.Username);
        }
    }
}

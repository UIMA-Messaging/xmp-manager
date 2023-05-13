using XmpManager.Clients;
using XmpManager.Contracts;
using XmpManager.RabbitMQ;

namespace XmpManager.Services
{
    public class UserService
    {
        private readonly EjabberdClient client;

        public UserService(EjabberdClient client, IRabbitMQListener<User> userRegistrations, IRabbitMQListener<User> userUnregistrations) 
        {
            this.client = client;
            userRegistrations.OnReceive += (_, user) => RegisterUser(user);
            userUnregistrations.OnReceive += (_, user) => UnregisterUser(user.Username);
        }

        public async Task RegisterUser(User user)
        {
            if (user != null)
            {
                await client.RegisterUser(user.Username, user.EphemeralPassword);
            }
        }

        public async Task UnregisterUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await client.UnregisterUser(id);
            }
        }
    }
}

using Newtonsoft.Json;
using System.Diagnostics;
using XmpManager.Clients.Ejabberd;
using XmpManager.Contracts;
using XmpManager.EventBus;

namespace XmpManager.Service.Users
{
    public class UserService : IUserService
    {
        private readonly EjabberdClient client;

        public UserService(IRabbitMQListener<User> userRegistrations, EjabberdClient client) 
        {
            this.client = client;

            userRegistrations.OnReceive += (_, user) => RegisterNewUser(user);
        }

        public async Task RegisterNewUser(User user)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(user));
            if (user != null)
            {
                await client.RegisterUser(user.Username, user.Id);
            }
        }

        public async Task UnregisterNewUser(User user)
        {
            await client.UnregisterUser(user.Username);
        }
    }
}

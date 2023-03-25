using Newtonsoft.Json;
using System.Diagnostics;
using XmpManager.Clients;
using XmpManager.Contracts;

namespace XmpManager.Service.Users
{
    public class UserService : IUserService
    {
        private readonly EjabberdClient client;

        public UserService() 
        {
            this.client = new EjabberdClient();
        }

        public async Task RegisterNewUser(User user)
        {
            Debug.WriteLine("Registering user...");
            Debug.WriteLine(JsonConvert.SerializeObject(user));
            client.RegisterUser(user.Username, user.Id);
        }

        public async Task UnregisterNewUser(User user)
        {
            await client.UnregisterUser(user.Username);
        }
    }
}

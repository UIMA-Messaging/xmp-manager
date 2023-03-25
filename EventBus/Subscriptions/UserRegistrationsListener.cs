using XmpManager.Contracts;
using XmpManager.EventBus.RabbitMQ;
using XmpManager.Service.Users;

namespace XmpManager.EventBus.Subscriptions
{
    public class UserRegistrationsListener
    {
        public UserRegistrationsListener(IUserService userService, IRabbitMQListener<User> rabbitMQListener) 
        {
            rabbitMQListener.OnReceive += (_, user) => userService.RegisterNewUser(user);
        }
    }
}

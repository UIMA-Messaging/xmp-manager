using XmpManager.Contracts;

namespace XmpManager.Service.Users
{
    public interface IUserService
    {
        public Task RegisterUser(User user);
        public Task UnregisterUser(User user);
    }
}
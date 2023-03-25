using XmpManager.Contracts;

namespace XmpManager.Service.Users
{
    public interface IUserService
    {
        public Task RegisterNewUser(User user);
        public Task UnregisterNewUser(User user);
    }
}
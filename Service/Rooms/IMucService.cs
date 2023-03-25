using XmpManager.Contracts;

namespace XmpManager.Service.Rooms
{
    public interface IMucService
    {
        public Task CreateMuc(Channel channel);
    }
}
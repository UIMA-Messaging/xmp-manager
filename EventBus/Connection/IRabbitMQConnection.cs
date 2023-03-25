using RabbitMQ.Client;

namespace XmpManager.EventBus.Connection
{
    public interface IRabbitMQConnection
    {
        public IConnection TryConnect();
    }
}
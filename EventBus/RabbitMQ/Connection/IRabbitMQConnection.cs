using RabbitMQ.Client;

namespace XmpManager.EventBus.RabbitMQ.Connection
{
    public interface IRabbitMQConnection
    {
        public IConnection TryConnect();
    }
}
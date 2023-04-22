using RabbitMQ.Client;

namespace XmpManager.RabbitMQ.Connection
{
    public interface IRabbitMQConnection
    {
        public IConnection TryConnect();
    }
}
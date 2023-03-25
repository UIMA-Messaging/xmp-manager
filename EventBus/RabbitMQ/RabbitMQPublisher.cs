using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;

namespace XmpManager.EventBus.RabbitMQ
{
    public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        private readonly IModel channel;
        private readonly string exchange;

        public RabbitMQPublisher(string exchange, IConnection connection)
        {
            channel = connection.CreateModel();
            this.exchange = exchange;

            channel.ExchangeDeclare(exchange, ExchangeType.Topic, durable: true);
        }

        public void Publish(T message, params string[] routingKeys)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            foreach (var key in routingKeys)
            {
                channel.BasicPublish(exchange, key, properties, body);
            }

            Debug.WriteLine($" [x] Sent '{message}' to '{routingKeys}'");
        }
    }
}

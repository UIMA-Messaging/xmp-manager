using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace XmpManager.EventBus.Subscriptions
{
    public class RabbitMQListener<T> : IRabbitMQListener<T>
    {
        public event EventHandler<T> OnReceive;
        private readonly IModel channel;
        private readonly string exchange;
        private readonly string queue;

        public RabbitMQListener(string exchange, string queue, IConnection connection)
        {
            channel = connection.CreateModel();
            this.exchange = exchange;
            this.queue = queue;

            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.ExchangeDeclare(exchange, ExchangeType.Topic, durable: true);
        }

        public void Subscribe(params string[] routingKeys)
        {
            foreach (var key in routingKeys)
            {
                channel.QueueBind(queue, exchange, key);
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += HandleMessage;

            channel.BasicConsume(queue, true, consumer: consumer);

            Debug.WriteLine($" [*] Listening on '{routingKeys}'...");
        }

        private void HandleMessage(object model, BasicDeliverEventArgs eventArgs)
        {
            var json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var message = JsonConvert.DeserializeObject<T>(json);

            Debug.WriteLine($" [x] Received '{message}' from '{eventArgs.RoutingKey}'");

            OnReceive?.Invoke(this, message);
        }
    }
}

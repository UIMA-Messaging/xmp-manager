namespace XmpManager.EventBus.Subscriptions
{
    public interface IRabbitMQListener<T>
    {
        public event EventHandler<T> OnReceive;

        public void Subscribe(params string[] routingKeys);
    }
}
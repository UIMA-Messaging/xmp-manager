namespace XmpManager.EventBus.RabbitMQ
{
    public interface IRabbitMQListener<T>
    {
        public event EventHandler<T> OnReceive;
    }
}
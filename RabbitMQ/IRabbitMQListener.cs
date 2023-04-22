namespace XmpManager.RabbitMQ
{
    public interface IRabbitMQListener<T>
    {
        public event EventHandler<T> OnReceive;
    }
}
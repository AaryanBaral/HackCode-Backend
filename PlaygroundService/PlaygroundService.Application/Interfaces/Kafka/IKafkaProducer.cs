namespace PlaygroundService.Application.Interfaces.Kafka
{
    public interface IKafkaProducer
    {
        Task ProduceAsync<T>(string topic, T message, string correlationId);
        public void Dispose();
    }
}
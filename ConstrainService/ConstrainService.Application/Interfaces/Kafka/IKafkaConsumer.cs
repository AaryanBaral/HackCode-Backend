namespace ConstrainService.Application.Interfaces.Kafka
{
    public interface IKafkaConsumer
    {
        void ConsumeAsync(CancellationToken cancellationToken);
    }
}
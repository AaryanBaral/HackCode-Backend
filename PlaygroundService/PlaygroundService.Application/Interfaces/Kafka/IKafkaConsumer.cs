using PlaygroundService.Application.DTOs.KafkaDto;

namespace PlaygroundService.Application.Interfaces.Kafka
{
    public interface IKafkaConsumer
    {
        void ConsumeAsync(CancellationToken cancellationToken);
        Task<GetLanguageResponse> WaitForLanguageResponseAsync(string correlationID);
    }
}
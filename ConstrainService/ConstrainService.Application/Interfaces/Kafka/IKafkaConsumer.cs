using ConstrainService.Application.DTOs.Kafka;

namespace ConstrainService.Application.Interfaces.Kafka
{
    public interface IKafkaConsumer
    {
        void ConsumeAsync(CancellationToken cancellationToken);
        Task<ValidateQuestionResponse> WaitForValidateQuestionIdResponseAsync(string correlationID);
    }
}
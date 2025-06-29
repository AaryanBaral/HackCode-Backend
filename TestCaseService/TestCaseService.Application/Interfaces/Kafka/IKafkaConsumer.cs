using ConstrainService.Application.DTOs.Kafka;

namespace TestCaseService.Application.Interfaces.Kafka
{
    public interface IKafkaConsumer
    {
        void ConsumeAsync(CancellationToken cancellationToken);
        public Task<ValidateQuestionResponse> WaitForValidateQuestionIdResponseAsync(string correlationID);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionService.Application.DTOs.KafkaDto;

namespace QuestionService.Application.Interfaces.Kafka
{
    public interface IKafkaConsumer
    {
        void ConsumeAsync(CancellationToken cancellationToken);
        Task<ValidateUserIDResponse> WaitForUserIDResponseAsync(string correlationID);
        Task<QuestionDeleteResponse> WaitForQuestionDeleteResponseAsync(string correlationID);
    }
}
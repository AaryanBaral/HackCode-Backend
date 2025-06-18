using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Application.DTOs.KafkaDto;

namespace UserService.Application.Interfaces.kafka
{
    public interface IKafkaConsumer
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
        Task IsUserIdValid(ValidateUserIdRequest request);
    }
}
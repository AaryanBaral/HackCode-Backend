using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageService.Application.Interfaces.Kafka
{
    public interface IKafkaConsumer
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
    }
}
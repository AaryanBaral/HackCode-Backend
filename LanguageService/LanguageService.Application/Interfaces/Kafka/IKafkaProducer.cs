using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageService.Application.Interfaces.Kafka
{
    public interface IKafkaProducer
    {
        Task ProduceAsync<T>(string topic, T message, string correlationId);
        public void Dispose();
    }
}
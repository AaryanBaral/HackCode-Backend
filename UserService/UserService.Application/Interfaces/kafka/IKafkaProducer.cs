using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Application.Interfaces.kafka
{
    public interface IKafkaProducer
    {
        Task ProduceAsync<T>(string topic, T message, string correlationId);
        public void Dispose();
    }
}
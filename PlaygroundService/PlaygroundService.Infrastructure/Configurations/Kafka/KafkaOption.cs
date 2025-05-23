using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundService.Infrastructure.Configurations.Kafka
{
    public class KafkaOption
    {
        public required string BootstrapServers { get; set; }
        public required string ProducerClientId { get; set; }
        public required string ConsumerGroupId { get; set; }
    }
}
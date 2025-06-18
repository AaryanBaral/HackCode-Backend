using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCaseService.Infrastructure.Configurations.Kafka
{
    public class KafkaOptions
    {
        public required string BootstrapServers { get; set; }
        public required string ProducerClientId { get; set; }
        public required string ConsumerGroupId { get; set; }
    }
}
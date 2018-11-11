using CM.Shared.Kernel.Application.Bus.Models;
using CM.Shared.Kernel.Application.Bus.Models.Events;
using CM.Shared.Kernel.Others.Kafka;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Others.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly KafkaProducerSettings KafkaProducerSettings;

        private readonly Producer<string, string> Producer;

        public KafkaProducer(KafkaSettings kafkaSettings, KafkaProducerSettings kafkaProducerSettings)
        {
            KafkaProducerSettings = kafkaProducerSettings;

            Producer = new Producer<string, string>(
                new Dictionary<string, object>()
                {
                    {
                    "bootstrap.servers", $"{kafkaSettings.Host}:9092"
                    },
                    { "default.topic.config", new Dictionary<string, object>()
                    {
                        { "message.timeout.ms", 5000 }
                    }
                },
                { "message.send.max.retries", 0 }
                },
                new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8));
        }

        public async Task Publish(IntegrationEvent integrationEvent)
        {
            string data = JsonConvert.SerializeObject(integrationEvent, Formatting.Indented);

            Message<string, string> message = await Producer
                .ProduceAsync(KafkaProducerSettings.Topic, integrationEvent.GetType().AssemblyQualifiedName, data);
        }

        public async Task Publish(IEnumerable<IntegrationEvent> integrationEvents, Header header)
        {
            foreach (var integrationEvent in integrationEvents)
            {
                integrationEvent.SetHeader(header);
                await Publish(integrationEvent);
            }
        }

        public void Dispose()
        {
            Producer.Dispose();
        }
    }
}
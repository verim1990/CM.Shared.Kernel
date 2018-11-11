using CM.Shared.Kernel.Application.Bus.Models;
using CM.Shared.Kernel.Application.Exceptions;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Others.Kafka
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IMediator Mediator;

        private readonly KafkaConsumerSettings KafkaConsumerSettings;

        private readonly KafkaSettings KafkaSettings;

        private readonly Consumer<string, string> Consumer;

        public KafkaConsumer(IMediator mediator, KafkaSettings kafkaSettings, KafkaConsumerSettings kafkaConsumerSettings)
        {
            Mediator = mediator;
            KafkaConsumerSettings = kafkaConsumerSettings;
            KafkaSettings = kafkaSettings;

            Consumer = new Consumer<string, string>(
                new Dictionary<string, object>
                {
                    { "group.id", KafkaConsumerSettings.GroupId },
                    { "enable.auto.commit", true },
                    { "auto.commit.interval.ms", 5000 },
                    { "statistics.interval.ms", 60000 },
                    { "bootstrap.servers", $"{kafkaSettings.Host}:9092" },
                    { "default.topic.config", new Dictionary<string, object>()
                        {
                            { "auto.offset.reset", "smallest" }
                        }
                    }
                }, new StringDeserializer(Encoding.UTF8), new StringDeserializer(Encoding.UTF8));
        }

        public async Task Listen(CancellationToken stoppingToken)
        {
            Consumer.Subscribe(KafkaConsumerSettings.Topics);

            while (!stoppingToken.IsCancellationRequested)
            {
                Message<string, string> msg;
                if (Consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
                {
                    try
                    {
                        Type eventType = Type.GetType(msg.Key);
                        IntegrationEvent integrationEvent = (IntegrationEvent)JsonConvert.DeserializeObject(msg.Value, eventType);
                        Mediator.Send(integrationEvent).Wait();
                    }
                    catch (DomainException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (TransactionConflictException ex)
                    {
                        Console.WriteLine(ex.IntegrationEvent.ToString());
                    }
                    catch (AppException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}

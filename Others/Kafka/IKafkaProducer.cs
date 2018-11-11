using CM.Shared.Kernel.Application.Bus.Models;
using CM.Shared.Kernel.Application.Bus.Models.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Others.Kafka
{
    public interface IKafkaProducer : IDisposable
    {
        Task Publish(IntegrationEvent integrationEvent);

        Task Publish(IEnumerable<IntegrationEvent> integrationEvents, Header header);
    }
}

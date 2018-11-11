using CM.Shared.Kernel.Application.Base;
using CM.Shared.Kernel.Application.Bus.Models;
using CM.Shared.Kernel.Application.Bus.Models.Commands;
using CM.Shared.Kernel.Application.Bus.Models.Events;
using CM.Shared.Kernel.Others.Kafka;
using MediatR;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Application.Bus
{
    public class Bus : IBus
    {
        private readonly IMediator Mediator;

        private readonly IKafkaProducer KafkaProducer;

        public Bus(IMediator mediator, IKafkaProducer kafkaProducer)
        {
            Mediator = mediator;
            KafkaProducer = kafkaProducer;
        }

        public async Task Send(Command command)
        {
            await Mediator.Send(command);
        }

        public async Task<T> Send<T>(CommandWithResponse<T> command) where T : CommandResponse
        {
            return await Mediator.Send(command);
        }

        public async Task<T> Send<T>(Query<T> query) where T : QueryResult
        {
            return await Mediator.Send(query);
        }

        public async Task Publish(Event @event) {
            await Mediator.Publish(@event);
        }

        public async Task Publish(DomainEvent @event)
        {
            await Mediator.Publish(@event);
        }

        public async Task PublishIntegrationEvents(AggregateRoot aggregateRoot, Header header)
        {
            await KafkaProducer.Publish(aggregateRoot.GetEvents(), header);
        }
    }
}

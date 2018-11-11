using CM.Shared.Kernel.Application.Base;
using CM.Shared.Kernel.Application.Bus.Models;
using CM.Shared.Kernel.Application.Bus.Models.Commands;
using CM.Shared.Kernel.Application.Bus.Models.Events;
using CM.Shared.Kernel.Others.Kafka;
using MediatR;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Application.Bus
{
    public interface IBus
    {
        Task Send(Command command);

        Task<T> Send<T>(CommandWithResponse<T> command) where T : CommandResponse;

        Task<T> Send<T>(Query<T> query) where T : QueryResult;

        Task Publish(Event @event);

        Task Publish(DomainEvent @event);

        Task PublishIntegrationEvents(AggregateRoot aggregateRoot, Header header);
    }
}

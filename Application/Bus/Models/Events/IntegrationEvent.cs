using CM.Shared.Kernel.Application.Bus.Models.Events;
using MediatR;
using System;

namespace CM.Shared.Kernel.Application.Bus.Models
{
    public class IntegrationEvent : IRequest
    {
        public Guid AggregateRootId { get; private set; }

        public int Version { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public Header Header { get; private set; }

        protected IntegrationEvent(Guid aggregateRootId, int version, DateTime createdDate, Header header)
        {
            AggregateRootId = aggregateRootId;
            Version = version;
            CreatedDate = createdDate;
            Header = header;
        }

        public static IntegrationEvent Create(Guid aggregateRootId, int version, DateTime createdDate)
        {
            return new IntegrationEvent(aggregateRootId, version, createdDate, null);
        }

        public void SetHeader(Header header)
        {
            Header = header;
        }
    }
}
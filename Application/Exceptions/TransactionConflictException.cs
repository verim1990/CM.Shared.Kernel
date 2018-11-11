using CM.Shared.Kernel.Application.Base;
using CM.Shared.Kernel.Application.Bus.Models;

namespace CM.Shared.Kernel.Application.Exceptions
{
    public class TransactionConflictException : AppException
    {
        public AggregateRoot AggregateRoot { get; private set; }

        public IntegrationEvent IntegrationEvent { get; private set; }

        public TransactionConflictException(AggregateRoot aggregateRoot, IntegrationEvent integrationEvent)
        {
            AggregateRoot = aggregateRoot;
            IntegrationEvent = integrationEvent;
        }
    }
}
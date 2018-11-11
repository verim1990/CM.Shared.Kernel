using CM.Shared.Kernel.Application.Bus.Models;
using CM.Shared.Kernel.Application.Bus.Models.Events;
using System;
using System.Collections.Generic;

namespace CM.Shared.Kernel.Application.Base
{
    public abstract class AggregateRoot : BaseEntity
    {
        private readonly Dictionary<Type, Action<object>> handlers = new Dictionary<Type, Action<object>>();
        private readonly List<IntegrationEvent> integrationEvents = new List<IntegrationEvent>();

        public int Version { get; private set; }

        public AggregateRoot()
        {
            Version = 0;
        }

        protected void Register<T>(Action<T> when)
        {
            handlers.Add(typeof(T), e => when((T)e));
        }

        protected void Raise(IntegrationEvent integrationEvent)
        {
            integrationEvents.Add(integrationEvent);
            handlers[integrationEvent.GetType()](integrationEvent);
            Version++;
        }

        public IReadOnlyCollection<IntegrationEvent> GetEvents()
        {
            return integrationEvents;
        }

        public void Apply(IntegrationEvent integrationEvent)
        {
            handlers[integrationEvent.GetType()](integrationEvent);
            Version++;
        }
    }
}
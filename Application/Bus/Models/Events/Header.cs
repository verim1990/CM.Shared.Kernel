using System;

namespace CM.Shared.Kernel.Application.Bus.Models.Events
{
    public class Header
    {
        public Guid CorrelationId { get; private set; }

        public Header(Guid correlationId, string userName)
        {
            CorrelationId = correlationId;
        }
    }
}
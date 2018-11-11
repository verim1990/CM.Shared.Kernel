using CM.Shared.Kernel.Application.Bus.Models.Events;
using MediatR;

namespace CM.Shared.Kernel.Application.Bus.Models.Commands
{
    public class Command : IRequest
    {
        public Header Header { get; set; }
    }

    public class CommandWithResponse<T> : IRequest<T> where T : CommandResponse
    {
        public Header Header { get; set; }
    }
}
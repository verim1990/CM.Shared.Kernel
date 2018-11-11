using MediatR;

namespace CM.Shared.Kernel.Application.Bus.Models
{
    public class Query<T> : IRequest<T> where T : QueryResult
    {
    }
}
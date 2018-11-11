using CM.Shared.Kernel.Application.Base;

namespace CM.Shared.Kernel.Application.Interfaces.Repository
{
    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T> where T : BaseEntity
    {
    }
}
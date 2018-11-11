using CM.Shared.Kernel.Application.Base;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Application.Interfaces.Repository
{
    public interface IWriteOnlyRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T entity, CancellationToken token = default(CancellationToken));

        Task UpdateAsync(T entity, CancellationToken token = default(CancellationToken));

        Task DeleteAsync(T entity, CancellationToken token = default(CancellationToken));

        Task SaveChangesAsync(CancellationToken token = default(CancellationToken));
    }
}
using CM.Shared.Kernel.Application.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Application.Interfaces.Repository
{
    public interface IReadOnlyRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);
    }
}
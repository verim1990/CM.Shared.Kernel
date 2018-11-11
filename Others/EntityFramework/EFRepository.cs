using CM.Shared.Kernel.Application.Base;
using CM.Shared.Kernel.Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Application.Repository
{
    public class EFRepository<T, TContext> : IRepository<T> where T : BaseEntity where TContext : DbContext
    {
        private readonly TContext Context;

        private DbSet<T> Entities { get; }

        public EFRepository(TContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var entity = await Entities.FindAsync(id);

            if (entity == null)
                return null;

            return entity;
        }

        public async Task InsertAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await Entities.AddAsync(entity, token);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            T exist = await Entities.FindAsync(entity.Id);
            
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(entity);
            }

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            T exist = await Entities.FindAsync(entity.Id);
            Entities.Remove(exist);
        }

        public async Task SaveChangesAsync(CancellationToken token = default(CancellationToken))
        {
            await Context.SaveChangesAsync(token);
        }
    }
}
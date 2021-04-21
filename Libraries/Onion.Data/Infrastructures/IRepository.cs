using Onion.Domain.Bases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Data.Infrastructures
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task InsertAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);

        Task UpdateEntityAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(int id);

        Task InsertManyAsync(IList<TEntity> entities);

        Task DeleteManyAsync(IList<TEntity> entities);

        Task DeleteManyAsync(IList<int> ids);

        Task UpdateManyAsync(IList<TEntity> entities);

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableWithNoTracking { get; }
    }
}
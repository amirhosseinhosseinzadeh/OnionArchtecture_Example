using Onion.Domain.Bases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Data.Infrastructures
{
    public interface IRepositiry<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);

        Task<TEntity> GetById(int id);

        Task UpdateEntity(TEntity entity);

        Task Delete(TEntity entity);

        Task Delete(int id);

        Task InsertMany(IList<TEntity> entities);

        Task DeleteMany(IList<TEntity> entities);

        Task DeleteMany(IList<int> ids);

        Task UpdateMany(IList<TEntity> entities);

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableWithNoTracking { get; }
    }
}
using Microsoft.EntityFrameworkCore;
using Onion.Libraries.Domain.Bases;

namespace Onion.Data
{
    public interface IBaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
    }
}

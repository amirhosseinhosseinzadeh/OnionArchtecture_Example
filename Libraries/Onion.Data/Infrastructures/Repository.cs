using Onion.Domain.Bases;
using Onnion.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Onion.Data.Infrastructures
{
    public class Repository<T> : IRepositiry<T> where T : BaseEntity
    {
        #region Fields

        private readonly ObjectContext _context;

        private DbSet<T> _entities;

        #endregion

        #region Ctor

        public Repository(ObjectContext objectContext)
        {
            this._context = objectContext;
        }

        #endregion

        #region Utilities

        void CheckForValue(int value)
        {
            if (value == 0)
                throw new Exception("Value can not be zero");
        }

        void CheckForValue(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value can not be null");
        }

        void CheckForValue(IList<T> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values), "Values can not be null");

            if (values.Any(v => v == null))
                throw new Exception("Some values are null");
        }

        void CheckForValue(IList<int> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values), "Values can not be null");

            if (values.Any(v => v == 0))
                throw new Exception("Some values are zero");
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }

        #endregion

        #region Methods

        public virtual async Task Delete(T entity)
        {
            CheckForValue(entity);

            try
            {
                Entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception thrown during removing data", ex);
            }
        }

        public virtual async Task Delete(int id)
        {
            CheckForValue(id);

            try
            {
                var entity = await GetById(id);
                Entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception thrown during removing data", ex);
            }
        }

        public virtual async Task<T> GetById(int id)
        {
            CheckForValue(id);

            return await Entities.FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            CheckForValue(entity);

            try
            {
                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An excption thrown during insert data", ex);
            }
        }

        public virtual async Task UpdateEntity(T entity)
        {
            CheckForValue(entity);

            try
            {
                Entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception thrown during updating data", ex);
            }
        }

        public virtual async Task InsertMany(IList<T> entities)
        {
            CheckForValue(entities);

            try
            {
                await Entities.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception thrown during insert records", ex);
            }
        }

        public virtual async Task DeleteMany(IList<T> entities)
        {
            CheckForValue(entities);

            try
            {
                Entities.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception thrown during removing some data", ex);
            }
        }

        public virtual async Task DeleteMany(IList<int> ids)
        {
            CheckForValue(ids);

            try
            {
                foreach (var id in ids)
                {
                    var entity = await GetById(id);

                    if (entity != null)
                        await Delete(entity);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception thrown during removing some data", ex);
            }
        }

        public virtual async Task UpdateMany(IList<T> entities)
        {
            CheckForValue(entities);
            try
            {
                Entities.UpdateRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An exception throw during updating records", ex);
            }
        }

        #endregion

        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableWithNoTracking => Entities.AsNoTracking();

    }
}

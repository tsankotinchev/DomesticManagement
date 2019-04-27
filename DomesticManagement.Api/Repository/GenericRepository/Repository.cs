using DomesticManagement.Common.Dto;
using DomesticManagement.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomesticManagement.Api.Repository.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DomesticManagementContext _context;
        protected readonly ILogger _logger;

        public Repository(DomesticManagementContext context, ILogger<Repository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            var res = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return res;
        }


        public virtual async Task<TEntity> GetAsync(object key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }


        public virtual async Task<TEntity> AddAsync(TEntity t)
        {
            await _context.Set<TEntity>().AddAsync(t);
            return t;

        }
        public virtual async Task<ICollection<TEntity>> AddManyAsync(ICollection<TEntity> t)
        {
            await _context.Set<TEntity>().AddRangeAsync(t);
            return t;
        }

        public virtual async Task<ICollection<TEntity>> UpdateManyAsync(ICollection<TEntity> t)
        {
            _context.Set<TEntity>().UpdateRange(t);
            return t;

        }

        public virtual async Task DeleteManyAsync(ICollection<TEntity> t)
        {
            _context.Set<TEntity>().RemoveRange(t);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, bool noTracking = true)
        {
            if (noTracking)
                return await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(match);
            else
                return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public async Task<TEntity> FindIncluding(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes == null)
            {
                return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
            }

            var data = _context.Set<TEntity>().AsQueryable();

            data = includes.Aggregate(data, (current, property) => current.Include(property));

            return await data.SingleOrDefaultAsync(match);

        }

        public virtual async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(match).ToListAsync();
        }


        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

        }


        public virtual async Task<TEntity> UpdateAsync(TEntity t, object key)
        {
            if (t == null)
                return null;

            TEntity exist = await _context.Set<TEntity>().FindAsync(key);

            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);

            }

            return exist;
        }


        public virtual async Task<TEntity> UpdateBoolValueAsync(TEntity t, object key, bool flag, string properyName)
        {
            if (t == null)
                return null;

            TEntity exist = await _context.Set<TEntity>().FindAsync(key);

            if (exist != null)
            {

                _context.Entry(exist).CurrentValues[properyName] = flag;

            }
            return exist;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync();
        }


        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> queryable = GetAll().AsNoTracking();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public virtual async Task<PagedResponseDto<TEntity>> GetPageAsync(int skip, int take, string sort, bool? filterIsActive, string searchTerm)
        {

            var queryable = _context.Set<TEntity>();

            return new PagedResponseDto<TEntity>
            {
                Count = _context.Set<TEntity>().Count(),
                Data = await _context.Set<TEntity>().OrderBy(sort).Skip(skip).Take(take).ToListAsync()
            };

        }

        public virtual bool CheckForDuplicate(string propertyName, string propertyValue, Guid? key)
        {
            IQueryable<string> stringQuerable;
            if (key != null && key != Guid.Empty)
            {
                stringQuerable = _context.Set<TEntity>().Where($"Id != \"{key}\"").Select($"{propertyName}") as IQueryable<string>;
            }
            else
            {
                stringQuerable = _context.Set<TEntity>().Select($"{propertyName}") as IQueryable<string>;
            }
            return stringQuerable.Any(x => x.Replace(" ", string.Empty) == propertyValue.Replace(" ", string.Empty));
        }

        public async Task<bool> UpdateForeignKeysAsync(IList<Guid> parameterCofigurationListId, string foreignKeyName, Guid foreignKeyValues)
        {

            if (parameterCofigurationListId == null)
                return false;

            foreach (var key in parameterCofigurationListId)
            {
                TEntity exist = await _context.Set<TEntity>().FindAsync(key);
                if (exist != null)
                {
                    if (foreignKeyValues != Guid.Empty)
                        _context.Entry(exist).CurrentValues[foreignKeyName] = foreignKeyValues;
                    else
                        _context.Entry(exist).CurrentValues[foreignKeyName] = null;
                }
                else
                    return false;

            }
            return true;
            //try
            //{
            //    await _context.SaveChangesAsync();
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError(CommonFunctions.BuildStringFromException(e));
            //    return false;
            //}
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

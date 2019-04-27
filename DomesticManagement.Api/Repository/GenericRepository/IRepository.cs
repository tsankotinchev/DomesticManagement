using DomesticManagement.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomesticManagement.Api.Repository.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T t);
        Task<ICollection<T>> AddManyAsync(ICollection<T> t);
        Task<ICollection<T>> UpdateManyAsync(ICollection<T> t);
        Task DeleteManyAsync(ICollection<T> t);
        Task<int> CountAsync();
        Task DeleteAsync(T entity);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match, bool noTracking = true);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(object key);
        Task<int> SaveAsync();
        Task<T> UpdateAsync(T t, object key);
        Task<PagedResponseDto<T>> GetPageAsync(int skip, int take, string sort, bool? filterIsActive, string searchTerm);
        Task<T> UpdateBoolValueAsync(T t, object key, bool flag, string properyName);
        bool CheckForDuplicate(string propertyName, string propertyValue, Guid? key = null);
        Task<bool> UpdateForeignKeysAsync(IList<Guid> parameterCofigurationListId, string foreignKeyName, Guid foreignKeyValues);
        void Dispose();
    }
}

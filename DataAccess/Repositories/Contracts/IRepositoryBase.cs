using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges, string? includeProperties, Expression<Func<T, bool>> filter);
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, bool trackChanges, string? includeProperties);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);  

    }
}

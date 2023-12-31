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
        void Add(T entity);
        IEnumerable<T> GetAll(bool trackChanges, string? includeProperties);
        T? GetByCondition(Expression<Func<T, bool>> condition, bool trackChanges, string? includeProperties);
        void Update(T entity);
        void Remove(T entity);  

    }
}

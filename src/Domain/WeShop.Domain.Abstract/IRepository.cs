using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeShop.Domain.Abstract
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetAsync(long id);

        T Insert(T entity);

        T Update(T entity);

        void Delete(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IUnitOfWork UnitOfWork { get; }
    }
}
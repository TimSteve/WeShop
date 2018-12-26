using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeShop.Domain.Abstract;
using WeShop.Infrasture.Data;

namespace WeShop.Infrasture.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly WeShopDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public BaseRepository(WeShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<T> GetAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return _context.Add(entity).Entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Attach(entity);
            return _context.Update(entity).Entity;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
    }
}
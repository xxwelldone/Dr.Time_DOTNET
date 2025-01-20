using System.Linq.Expressions;
using DoctorTime.API.Context;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorTime.API.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected PostgreSQL _context;

        public BaseRepository(PostgreSQL context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public Task<T?> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }
        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T? Update(T entity)
        {
             _context.Set<T>().Update(entity);
            return entity;
        }

        public T? Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}

using HorasRaras.Data.Context;
using HorasRaras.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HorasRaras.Data.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly HorasRarasContext _context;

        public BaseRepository(HorasRarasContext context)
        {
            _context = context;
        }

        public virtual async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> FindAsync(decimal id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).CountAsync();
        }

        public virtual async Task<int> CountAsync<K>(Expression<Func<T, IEnumerable<K>>> selectExpression)
        {
            return await _context.Set<T>().Select(selectExpression).Distinct().CountAsync();
        }

        public virtual async Task<int> CountAsync<K>(Expression<Func<T, bool>> expression, Expression<Func<T, IEnumerable<K>>> selectExpression)
        {
            return await _context.Set<T>().Where(expression).Select(selectExpression).Distinct().CountAsync();
        }

        public virtual async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public virtual async Task<List<T>> ListPaginationAsync<K>(Expression<Func<T, K>> sortExpression, int pagina, int quantidade)
        {
            return await _context.Set<T>().OrderBy(sortExpression).Skip(quantidade * (pagina - 1)).Take(quantidade).ToListAsync();
        }

        public virtual async Task<List<T>> ListPaginationAsync<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, int pagina, int quantidade)
        {
            return await _context.Set<T>().Where(expression).OrderBy(sortExpression).Skip(quantidade * (pagina - 1)).Take(quantidade).ToListAsync();
        }

        public virtual async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task EditAsync(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }
    }

}


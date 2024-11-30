using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServerApp.Context;
using System.Linq.Expressions;

namespace ServerApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SocialContext _context;
        private readonly DbSet<T> Table;

        public Repository(SocialContext context, DbSet<T> table)
        {
            _context = context;
            Table = table;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
           if( await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
           return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var value =  await Table.FindAsync(id);
            if (value == null)
            {
                return false;
            }
            _context.Remove(value);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? include = null)
        {
            IQueryable<T> values = Table;

            if (filter != null)
            {
                values = values.Where(filter);
            }

            if(include != null)
            {
                values = values.Include(include);
            }
            return await values.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
         return await Table.FirstOrDefaultAsync(filter);
         
        }

        public async Task<T> GetSingleByIdAsync(int id, Expression<Func<T, object>>? include = null)
        {
           
            if(include != null)
            {
                Table.Include(include);
            }

            return await Table.FindAsync(id);

            
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}

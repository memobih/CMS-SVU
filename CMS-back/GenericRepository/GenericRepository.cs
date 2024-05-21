using CMS_back.Data;
using CMS_back.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS_back.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CMSContext context;
        public GenericRepository(CMSContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }


        public async Task<T> FindFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
            //return context.Set<T>().ToList();
        }
        public async Task<T> GetById(string id, params string[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
        }
        public void Update(T entity)
        {
            context.Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
        }
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, params string[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>().Where(expression);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
            //return  context.Set<T>().Where(expression);
        }

    }
}
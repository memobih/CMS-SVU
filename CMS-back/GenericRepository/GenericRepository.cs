using CMS_back.Data;
using CMS_back.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return context.Set<T>().ToList();
        }
        public T GetById(string id)
        {

            return context.Set<T>().Find(id);
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
    }
}
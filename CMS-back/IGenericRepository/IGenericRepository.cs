﻿using System.Linq.Expressions;

namespace CMS_back.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void Update(T entity);
        void RemoveRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> expression);
    }
}

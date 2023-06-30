using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Database.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entity);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T entity);
        T Update(T entity);
        Task<T> GetById(int id);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);
        Task<int> SaveChanges();
    }
}

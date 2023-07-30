using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Database.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected MasterDbContext Context;

        public Repository(MasterDbContext context)
        {
            Context = context;
            SQLHelper.ConnectionString = Context.Database.GetDbConnection().ConnectionString;
        }

        public async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
            return entities;

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var data = await Context.Set<T>().ToListAsync();
            return data;
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public async Task<T> GetById(int id)
        {
            var data = await Context.Set<T>().FindAsync(id);
            return data;
        }

        public virtual async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await Context.Set<T>().Where(expression).FirstOrDefaultAsync();
        }
        public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await Context.Set<T>().Where(expression).ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        { 
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
             return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }
        public void Put(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified; 
        }

        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IDbSet<TEntity> dbSet = Context.Set<TEntity>();

            IEnumerable<TEntity> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        { 
            return Context.Set<TEntity>().Where(filter);
        }
        public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(query, parameters);
        }
    }
}
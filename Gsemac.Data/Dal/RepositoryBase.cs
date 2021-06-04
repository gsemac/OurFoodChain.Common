using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gsemac.Data.Dal {

    public abstract class RepositoryBase<TEntity, TDbContext> :
        IRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext {

        // Public members

        public TEntity Get(int id) {

            return Context.Set<TEntity>().Find(id);

        }
        public IEnumerable<TEntity> GetAll() {

            return Context.Set<TEntity>().ToList();

        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {

            return Context.Set<TEntity>().Where(predicate);

        }

        public void Add(TEntity entity) {

            Context.Set<TEntity>().Add(entity);

        }
        public void AddRange(IEnumerable<TEntity> entities) {

            Context.Set<TEntity>().AddRange(entities);

        }

        public void Remove(TEntity entity) {

            Context.Set<TEntity>().Remove(entity);

        }
        public void RemoveRange(IEnumerable<TEntity> entities) {

            Context.Set<TEntity>().RemoveRange(entities);

        }

        // Protected members

        protected TDbContext Context { get; }

        protected RepositoryBase(TDbContext context) {

            Context = context;

        }

    }

}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gsemac.Data.Dal {

    public abstract class RepositoryBase<TEntity, TDbContext> :
        IRepository<TEntity>
        where TEntity : class
        where TDbContext : IDbContext {

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
        public TEntity Add(TEntity entity) {

            return Context.Set<TEntity>().Add(entity).Entity;

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

        public async Task<TEntity> GetAsync(int id) {

            return await Context.Set<TEntity>().FindAsync(id);

        }
        public async Task<IEnumerable<TEntity>> GetAllAsync() {

            return await Context.Set<TEntity>().ToListAsync();

        }
        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) {

            return Task.FromResult(Find(predicate));

        }
        public async Task<TEntity> AddAsync(TEntity entity) {

            return (await Context.Set<TEntity>().AddAsync(entity)).Entity;

        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities) {

            await Context.Set<TEntity>().AddRangeAsync(entities);

        }
        public Task RemoveAsync(TEntity entity) {

            Remove(entity);

            return Task.CompletedTask;

        }
        public Task RemoveRangeAsync(IEnumerable<TEntity> entities) {

            RemoveRange(entities);

            return Task.CompletedTask;

        }

        // Protected members

        protected TDbContext Context { get; }

        protected RepositoryBase(TDbContext dbContext) {

            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            Context = dbContext;

        }

    }

}
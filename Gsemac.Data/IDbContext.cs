﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Gsemac.Data {

    public interface IDbContext {

        DbContextId ContextId { get; }
        IModel Model { get; }
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }

        public event EventHandler<SavingChangesEventArgs> SavingChanges;
        public event EventHandler<SavedChangesEventArgs> SavedChanges;
        public event EventHandler<SaveChangesFailedEventArgs> SaveChangesFailed;

        EntityEntry Add(object entity);
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        void AddRange(IEnumerable<object> entities);
        void AddRange(params object[] entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task AddRangeAsync(params object[] entities);
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Attach(object entity);
        void AttachRange(params object[] entities);
        void AttachRange(IEnumerable<object> entities);

        void Dispose();
        ValueTask DisposeAsync();

        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        object Find(Type entityType, params object[] keyValues);
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);

        IQueryable<TResult> FromExpression<TResult>(Expression<Func<IQueryable<TResult>>> expression);

        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange(params object[] entities);
        void RemoveRange(IEnumerable<object> entities);

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>(string name) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry Update(object entity);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

        void UpdateRange(params object[] entities);
        void UpdateRange(IEnumerable<object> entities);

    }

}
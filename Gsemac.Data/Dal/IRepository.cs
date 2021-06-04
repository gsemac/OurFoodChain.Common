using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gsemac.Data.Dal {

    // This interface is based on the one described in this YouTube video:
    // https://youtu.be/rtXpYpZdOzM ("Repository Pattern with C# and Entity Framework, Done Right | Mosh" by Programming with Mosh)

    public interface IRepository<TEntity> where TEntity : class {

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

    }

}
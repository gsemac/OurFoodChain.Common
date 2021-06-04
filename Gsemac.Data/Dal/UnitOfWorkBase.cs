using Microsoft.EntityFrameworkCore;
using System;

namespace Gsemac.Data.Dal {

    public abstract class UnitOfWorkBase<TDbContext> :
        IUnitOfWork
        where TDbContext : DbContext {

        // Public members

        public int SaveChanges() {

            return Context.SaveChanges();

        }

        public void Dispose() {

            Dispose(disposing: true);

            GC.SuppressFinalize(this);

        }

        // Protected members

        protected TDbContext Context { get; }

        protected UnitOfWorkBase(TDbContext context) {

            Context = context;

        }

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    Context.Dispose();

                }

                disposedValue = true;
            }
        }

        // Private members

        private bool disposedValue;

    }

}
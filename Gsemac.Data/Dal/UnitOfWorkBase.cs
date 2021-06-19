using System;
using System.Threading.Tasks;

namespace Gsemac.Data.Dal {

    public abstract class UnitOfWorkBase :
        IUnitOfWork {

        // Public members

        public int SaveChanges() {

            return Context.SaveChanges();

        }
        public Task<int> SaveChangesAsync() {

            return Context.SaveChangesAsync();

        }

        public void Dispose() {

            Dispose(disposing: true);

            GC.SuppressFinalize(this);

        }

        // Protected members

        protected IDbContext Context { get; }

        protected UnitOfWorkBase(IDbContext dbContext) {

            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            Context = dbContext;

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
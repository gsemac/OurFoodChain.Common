using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;

namespace OurFoodChain.Data.Dal {

    public sealed class OfcUnitOfWork<TDbContext> :
        UnitOfWorkBase<TDbContext> where TDbContext : DbContext, IOfcDbContext {

        // Public members

        public IWorldRepository Worlds { get; }

        public OfcUnitOfWork(TDbContext context) :
            base(context) {

            Worlds = new WorldRepository<TDbContext>(context);

        }

    }

}
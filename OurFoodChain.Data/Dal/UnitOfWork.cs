using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;

namespace OurFoodChain.Data.Dal {

    public sealed class UnitOfWork<TDbContext> :
        UnitOfWorkBase<TDbContext> where TDbContext : DbContext, IOurFoodChainDbContext {

        // Public members

        public IWorldRepository Worlds { get; }

        public UnitOfWork(TDbContext context) :
            base(context) {

            Worlds = new WorldRepository<TDbContext>(context);

        }

    }

}
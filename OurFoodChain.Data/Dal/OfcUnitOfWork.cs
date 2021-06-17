using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public sealed class OfcUnitOfWork :
        UnitOfWorkBase,
        IOfcUnitOfWork {

        // Public members

        public IWorldRepository Worlds { get; }

        public OfcUnitOfWork(IOfcDbContext context) :
            base(context) {

            Worlds = new WorldRepository(context);

        }

    }

}
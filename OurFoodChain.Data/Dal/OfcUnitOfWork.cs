using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public sealed class OfcUnitOfWork :
        UnitOfWorkBase,
        IOfcUnitOfWork {

        // Public members

        public ITaxonRepository Taxa { get; }
        public IWorldRepository Worlds { get; }

        public OfcUnitOfWork(IOfcDbContext context) :
            base(context) {

            Taxa = new TaxonRepository(context);
            Worlds = new WorldRepository(context);

        }

    }

}
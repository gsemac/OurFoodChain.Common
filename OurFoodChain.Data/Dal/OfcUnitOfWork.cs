using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public sealed class OfcUnitOfWork :
        UnitOfWorkBase,
        IOfcUnitOfWork {

        // Public members

        public ICladeRepository Clades { get; }
        public IWorldRepository Worlds { get; }

        public OfcUnitOfWork(IOfcDbContext context) :
            base(context) {

            Clades = new CladeRepository(context);
            Worlds = new WorldRepository(context);

        }

    }

}
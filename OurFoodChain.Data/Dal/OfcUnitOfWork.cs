using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public sealed class OfcUnitOfWork :
        UnitOfWorkBase,
        IOfcUnitOfWork {

        // Public members

        public ICladeRepository Clades { get; }
        public ISpeciesRepository Species { get; }
        public IWorldRepository Worlds { get; }

        public OfcUnitOfWork(IOfcDbContext context) :
            base(context) {

            Clades = new CladeRepository(context);
            Species = new SpeciesRepository(context);
            Worlds = new WorldRepository(context);

        }

    }

}
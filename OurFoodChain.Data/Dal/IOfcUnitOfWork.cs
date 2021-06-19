using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public interface IOfcUnitOfWork :
         IUnitOfWork {

        ICladeRepository Clades { get; }
        ISpeciesRepository Species { get; }
        IWorldRepository Worlds { get; }

    }

}
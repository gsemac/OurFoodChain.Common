using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public interface IOfcUnitOfWork :
         IUnitOfWork {

        ICladeRepository Clades { get; }
        IWorldRepository Worlds { get; }

    }

}
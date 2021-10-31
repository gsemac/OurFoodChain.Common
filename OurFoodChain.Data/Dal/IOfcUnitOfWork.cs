using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public interface IOfcUnitOfWork :
         IUnitOfWork {

        ITaxonRepository Taxa { get; }
        IWorldRepository Worlds { get; }

    }

}
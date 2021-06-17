using Gsemac.Data.Dal;

namespace OurFoodChain.Data.Dal {

    public interface IOfcUnitOfWork :
         IUnitOfWork {

        IWorldRepository Worlds { get; }

    }

}
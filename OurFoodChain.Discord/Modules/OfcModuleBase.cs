using OurFoodChain.Data;
using OurFoodChain.Data.Dal;

namespace OurFoodChain.Discord.Modules {

    public abstract class OfcModuleBase :
         Gsemac.Discord.Modules.ModuleBase {

        public OfcUnitOfWork<OfcDbContext> Db { get; set; }

    }

}
using Gsemac.Data.Dal;
using OurFoodChain.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public interface IWorldRepository :
        IRepository<World> {

        Task<World> GetWorldAsync(int id);
        Task<World> GetWorldAsync(ulong serverId);
        Task<IEnumerable<World>> GetWorldsAsync();

    }

}
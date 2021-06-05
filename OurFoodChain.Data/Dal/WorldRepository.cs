using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class WorldRepository<TDbContext> :
        RepositoryBase<World, TDbContext>,
        IWorldRepository where TDbContext : DbContext, IOurFoodChainDbContext {

        // Public members

        public WorldRepository(TDbContext context) :
            base(context) {
        }

        public async Task<World> GetWorldAsync(int id) {

            return await Context.Worlds.FindAsync(id);

        }
        public Task<World> GetWorldAsync(ulong serverId) {

            return Task.FromResult(Context.Worlds.Where(world => world.DiscordServerId == serverId).FirstOrDefault());

        }
        public async Task<IEnumerable<World>> GetWorldsAsync() {

            return await Context.Worlds.ToListAsync();

        }

    }

}
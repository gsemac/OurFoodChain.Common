using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class WorldRepository :
        RepositoryBase<World, IOfcDbContext>,
        IWorldRepository {

        // Public members

        public WorldRepository(IOfcDbContext dbContext) :
            base(dbContext) {
        }

        public async Task<World> GetWorldAsync(int id) {

            return await Context.Worlds.FindAsync(id);

        }
        public async Task<IEnumerable<World>> GetWorldsAsync() {

            return await Context.Worlds.ToListAsync();

        }

    }

}
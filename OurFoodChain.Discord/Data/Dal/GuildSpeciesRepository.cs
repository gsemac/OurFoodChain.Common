using Gsemac.Core;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Data.Dal {

    public class GuildSpeciesRepository :
        SpeciesRepository {

        // Public members

        public GuildSpeciesRepository(IOfcDbContext dbContext, AsyncLazy<World> world) :
            base(dbContext) {

            if (world is null)
                throw new ArgumentNullException(nameof(world));

            this.world = world;

        }

        public async Task<IEnumerable<Species>> GetSpeciesAsync() {

            return await Context.Species.FilterBy(await world)
                          .ToListAsync();

        }
        public Task<IEnumerable<Species>> GetSpeciesAsync(string name) {

            throw new NotImplementedException();

        }
        public async Task<IEnumerable<Species>> GetSpeciesAsync(string genus, string species) {

            return await Context.Species.FilterBy(await world)
                .FilterBy(genus, species)
                .ToListAsync();

        }

        // Private members

        private readonly AsyncLazy<World> world;

    }

}
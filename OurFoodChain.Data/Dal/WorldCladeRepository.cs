using Gsemac.Core;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class WorldCladeRepository :
        CladeRepository {

        // Public members

        public WorldCladeRepository(IOfcDbContext dbContext, AsyncLazy<World> world) :
            base(dbContext) {

            if (world is null)
                throw new ArgumentNullException(nameof(world));

            this.world = world;

        }

        public async Task<IEnumerable<Clade>> GetCladesAsync() {

            return await Context.Clades.FilterBy(await world).ToListAsync();

        }
        public async Task<IEnumerable<Clade>> GetCladesAsync(string name) {

            return await Context.Clades.FilterBy(await world)
                .FilterBy(name)
                .ToListAsync();

        }
        public async Task<IEnumerable<Clade>> GetCladesAsync(string name, Rank rank) {

            return await Context.Clades.FilterBy(await world)
                .FilterBy(name)
                .FilterBy(rank)
                .ToListAsync();

        }

        public async Task<Clade> AddCladeAsync(Clade clade) {

            clade.World = await world;

            return await AddAsync(clade);

        }

        // Private members

        private readonly AsyncLazy<World> world;

    }

}
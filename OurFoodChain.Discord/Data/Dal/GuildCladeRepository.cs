using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Data.Dal {

    public class GuildCladeRepository :
        CladeRepository {

        // Public members

        public GuildCladeRepository(IOfcDbContext dbContext, AsyncLazy<World> world) :
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
        public async Task<IEnumerable<Clade>> GetCladesAsync(string name, TaxonRankId rank) {

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
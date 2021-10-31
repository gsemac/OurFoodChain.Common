using Gsemac.Core;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class WorldTaxonRepository :
        TaxonRepository {

        // Public members

        public WorldTaxonRepository(IOfcDbContext dbContext, AsyncLazy<World> world) :
            base(dbContext) {

            if (world is null)
                throw new ArgumentNullException(nameof(world));

            this.world = world;

        }

        public async Task<IEnumerable<Taxon>> GetTaxaAsync() {

            return await Context.Taxa.FilterBy(await world).ToListAsync();

        }
        public async Task<IEnumerable<Taxon>> GetTaxaAsync(string name) {

            return await Context.Taxa.FilterBy(await world)
                .FilterBy(name)
                .ToListAsync();

        }
        public async Task<IEnumerable<Taxon>> GetTaxaAsync(string name, Rank rank) {

            return await Context.Taxa.FilterBy(await world)
                .FilterBy(name)
                .FilterBy(rank)
                .ToListAsync();

        }

        public async Task<Taxon> AddTaxonAsync(Taxon taxon) {

            taxon.World = await world;

            return await AddAsync(taxon);

        }

        // Private members

        private readonly AsyncLazy<World> world;

    }

}
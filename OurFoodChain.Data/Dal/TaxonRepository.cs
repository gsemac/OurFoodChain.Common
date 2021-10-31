using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class TaxonRepository :
        RepositoryBase<Taxon, IOfcDbContext>,
        ITaxonRepository {

        // Public members

        public TaxonRepository(IOfcDbContext dbContext) :
            base(dbContext) {
        }

        public Task<Taxon> GetTaxaAsync(int id) {

            return GetAsync(id);

        }
        public async Task<IEnumerable<Taxon>> GetTaxaAsync(World world) {

            return await Context.Taxa.FilterBy(world).ToListAsync();

        }
        public async Task<IEnumerable<Taxon>> GetTaxaAsync(World world, string name) {

            return await Context.Taxa.FilterBy(world)
                .FilterBy(name)
                .ToListAsync();

        }
        public async Task<IEnumerable<Taxon>> GetTaxaAsync(World world, string name, Rank rank) {

            return await Context.Taxa.FilterBy(world)
                .FilterBy(name)
                .FilterBy(rank)
                .ToListAsync();

        }

    }

}
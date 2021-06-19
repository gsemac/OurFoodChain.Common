using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class CladeRepository :
        RepositoryBase<Clade, IOfcDbContext>,
        ICladeRepository {

        // Public members

        public CladeRepository(IOfcDbContext dbContext) :
            base(dbContext) {
        }

        public Task<Clade> GetCladeAsync(int id) {

            return GetAsync(id);

        }
        public async Task<IEnumerable<Clade>> GetCladesAsync(World world) {

            return await Context.Clades.FilterBy(world).ToListAsync();

        }
        public async Task<IEnumerable<Clade>> GetCladesAsync(World world, string name) {

            return await Context.Clades.FilterBy(world)
                .FilterBy(name)
                .ToListAsync();

        }
        public async Task<IEnumerable<Clade>> GetCladesAsync(World world, string name, TaxonRankId rank) {

            return await Context.Clades.FilterBy(world)
                .FilterBy(name)
                .FilterBy(rank)
                .ToListAsync();

        }

    }

}
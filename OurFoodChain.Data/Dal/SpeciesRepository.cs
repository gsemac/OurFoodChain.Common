using Gsemac.Data.Dal;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Extensions;
using OurFoodChain.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public class SpeciesRepository :
        RepositoryBase<Species, IOfcDbContext>,
        ISpeciesRepository {

        // Public members

        public SpeciesRepository(IOfcDbContext dbContext) :
            base(dbContext) {
        }

        public Task<Species> GetSpeciesAsync(int id) {

            return GetAsync(id);

        }
        public async Task<IEnumerable<Species>> GetSpeciesAsync(World world) {

            return await Context.Species.FilterBy(world).ToListAsync();

        }
        public Task<IEnumerable<Species>> GetSpeciesAsync(World world, string name) {

            throw new NotImplementedException();

        }
        public async Task<IEnumerable<Species>> GetSpeciesAsync(World world, string genus, string species) {

            return await Context.Species.FilterBy(world)
                .FilterBy(genus, species)
                .ToListAsync();

        }

    }

}
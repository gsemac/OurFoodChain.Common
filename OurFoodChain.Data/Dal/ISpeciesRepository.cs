using Gsemac.Data.Dal;
using OurFoodChain.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public interface ISpeciesRepository :
        IRepository<Species> {

        Task<Species> GetSpeciesAsync(int id);
        Task<IEnumerable<Species>> GetSpeciesAsync(World world);
        Task<IEnumerable<Species>> GetSpeciesAsync(World world, string name);
        Task<IEnumerable<Species>> GetSpeciesAsync(World world, string genus, string species);

    }

}
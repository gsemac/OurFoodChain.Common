using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public interface ITaxonRepository {

        Task<Taxon> GetTaxaAsync(int id);
        Task<IEnumerable<Taxon>> GetTaxaAsync(World world);
        Task<IEnumerable<Taxon>> GetTaxaAsync(World world, string name);
        Task<IEnumerable<Taxon>> GetTaxaAsync(World world, string name, Rank rank);

    }

}
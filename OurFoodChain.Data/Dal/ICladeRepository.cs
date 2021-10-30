using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OurFoodChain.Data.Dal {

    public interface ICladeRepository {

        Task<Clade> GetCladeAsync(int id);
        Task<IEnumerable<Clade>> GetCladesAsync(World world);
        Task<IEnumerable<Clade>> GetCladesAsync(World world, string name);
        Task<IEnumerable<Clade>> GetCladesAsync(World world, string name, Rank rank);

    }

}
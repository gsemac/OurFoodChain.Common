using OurFoodChain.Data.Models;
using System.Linq;

namespace OurFoodChain.Data.Extensions {

    internal static class QueryableSpeciesExtensions {

        public static IQueryable<Species> FilterBy(this IQueryable<Species> queryable, World world) {

            return queryable.Where(s => s.WorldId == world.Id);

        }
        public static IQueryable<Species> FilterBy(this IQueryable<Species> queryable, string genus, string species) {

            return queryable.Where(s => s.Name == species && s.Genus.Name == genus);

        }

    }

}
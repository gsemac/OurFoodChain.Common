using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System.Linq;

namespace OurFoodChain.Data.Extensions {

    internal static class QueryableCladesExtensions {

        public static IQueryable<Clade> FilterBy(this IQueryable<Clade> queryable, World world) {

            return queryable.Where(c => c.WorldId == world.Id);

        }
        public static IQueryable<Clade> FilterBy(this IQueryable<Clade> queryable, string name) {

            return queryable.Where(c => c.Name == name.ToLowerInvariant());

        }
        public static IQueryable<Clade> FilterBy(this IQueryable<Clade> queryable, TaxonRankId rank) {

            return queryable.Where(c => c.Rank == rank);

        }

    }

}
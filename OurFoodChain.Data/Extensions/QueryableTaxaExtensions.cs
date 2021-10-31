using OurFoodChain.Data.Models;
using OurFoodChain.Taxonomy;
using System.Linq;

namespace OurFoodChain.Data.Extensions {

    internal static class QueryableTaxaExtensions {

        public static IQueryable<Taxon> FilterBy(this IQueryable<Taxon> queryable, World world) {

            return queryable.Where(c => c.WorldId == world.Id);

        }
        public static IQueryable<Taxon> FilterBy(this IQueryable<Taxon> queryable, string name) {

            return queryable.Where(c => c.Name == name.ToLowerInvariant());

        }
        public static IQueryable<Taxon> FilterBy(this IQueryable<Taxon> queryable, string genus, string species) {

            return queryable.Where(c => c.Name == species && c.Parent.Name == genus);

        }
        public static IQueryable<Taxon> FilterBy(this IQueryable<Taxon> queryable, Rank rank) {

            return queryable.Where(c => c.Rank == rank);

        }

    }

}
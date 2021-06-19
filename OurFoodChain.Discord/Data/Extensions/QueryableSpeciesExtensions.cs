using Discord;
using OurFoodChain.Data.Models;
using System.Linq;

namespace OurFoodChain.Discord.Data.Extensions {

    public static class QueryableSpeciesExtensions {

        public static IQueryable<Species> FilterBy(this IQueryable<Species> queryable, IGuild guild) {

            return queryable.Where(s => s.World.DiscordGuildId == guild.Id);

        }

    }

}
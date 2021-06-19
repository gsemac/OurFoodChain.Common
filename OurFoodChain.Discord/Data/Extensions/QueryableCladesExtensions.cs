using Discord;
using OurFoodChain.Data.Models;
using System.Linq;

namespace OurFoodChain.Discord.Data.Extensions {

    public static class QueryableCladesExtensions {

        public static IQueryable<Clade> FilterBy(this IQueryable<Clade> queryable, IGuild guild) {

            return queryable.Where(c => c.World.DiscordGuildId == guild.Id);

        }

    }

}
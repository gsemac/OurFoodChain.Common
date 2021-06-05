using Discord;
using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Extensions {

    public static class WorldRepositoryExtensions {

        // Public members

        public static Task<World> GetWorldAsync(this IWorldRepository repository, IGuild guild) {

            return Task.FromResult(repository.Find(world => world.DiscordGuildId == guild.Id).FirstOrDefault());

        }

        // Private members

        private static World CreateDefaultWorld(IGuild guild) {

            return new World() {
                Name = guild.Name,
                DiscordGuildId = guild.Id,
            };

        }

    }

}
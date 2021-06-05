using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Extensions {

    public static class WorldRepositoryExtensions {

        public static Task<World> GetWorldAsync(this IWorldRepository repository, ulong serverId) {

            return Task.FromResult(repository.Find(world => world.DiscordServerId == serverId).FirstOrDefault());

        }

    }

}
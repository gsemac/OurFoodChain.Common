using Discord;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Models;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Extensions {

    public static class OfcUnitOfWorkExtensions {

        // Public members

        public static async Task<World> EnsureWorldCreatedAsync<TDbContext>(this OfcUnitOfWork<TDbContext> unitOfWork, IGuild guild) where TDbContext : DbContext, IOfcDbContext {

            World world = await unitOfWork.Worlds.GetWorldAsync(guild);

            if (world is null) {

                world = await unitOfWork.Worlds.AddAsync(CreateDefaultWorld(guild));

                unitOfWork.SaveChanges();

            }

            return world;

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
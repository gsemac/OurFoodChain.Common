using Discord;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Data.Dal {

    public class GuildWorldRepository :
        WorldRepository {

        // Public members

        public GuildWorldRepository(IOfcDbContext dbContext, IGuild guild) :
            base(dbContext) {

            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            if (guild is null)
                throw new ArgumentNullException(nameof(guild));

            this.dbContext = dbContext;
            this.guild = guild;

        }

        public Task<World> GetWorldAsync() {

            return Task.FromResult(Find(world => world.DiscordGuildId == guild.Id).FirstOrDefault());

        }
        public async Task<World> GetOrCreateWorldAsync() {

            World world = await GetWorldAsync();

            if (world is null) {

                world = await AddAsync(CreateDefaultWorld());

                dbContext.SaveChanges();

            }

            return world;

        }

        // Private members

        private readonly IOfcDbContext dbContext;
        private readonly IGuild guild;

        // Private members

        private World CreateDefaultWorld() {

            return new World() {
                Name = guild.Name,
                DiscordGuildId = guild.Id,
            };

        }

    }

}
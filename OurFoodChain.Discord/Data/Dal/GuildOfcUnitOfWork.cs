using Discord;
using Gsemac.Core;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;
using OurFoodChain.Data.Models;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Data.Dal {

    public sealed class GuildOfcUnitOfWork :
        IOfcUnitOfWork {

        // Public members

        public WorldTaxonRepository Taxa => new(dbContext, world);
        public GuildWorldRepository Worlds => new(dbContext, guild);

        ITaxonRepository IOfcUnitOfWork.Taxa => Taxa;
        IWorldRepository IOfcUnitOfWork.Worlds => Worlds;

        public GuildOfcUnitOfWork(IOfcDbContext dbContext, IGuild guild) {

            this.dbContext = dbContext;
            this.guild = guild;
            this.world = new AsyncLazy<World>(async () => await Worlds.GetOrCreateWorldAsync());

        }

        public int SaveChanges() {

            return dbContext.SaveChanges();

        }
        public Task<int> SaveChangesAsync() {

            return dbContext.SaveChangesAsync();

        }

        public void Dispose() {

            dbContext.Dispose();

        }

        // Private members

        private readonly IOfcDbContext dbContext;
        private readonly IGuild guild;
        private readonly AsyncLazy<World> world;

    }

}
using Discord;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;

namespace OurFoodChain.Discord.Data.Dal {

    public sealed class GuildOfcUnitOfWork :
        IOfcUnitOfWork {

        // Public members

        public GuildWorldRepository Worlds => new GuildWorldRepository(dbContext, guild);

        IWorldRepository IOfcUnitOfWork.Worlds => Worlds;

        public GuildOfcUnitOfWork(IOfcDbContext dbContext, IGuild guild) {

            this.dbContext = dbContext;
            this.guild = guild;

        }

        public int SaveChanges() {

            return dbContext.SaveChanges();

        }

        public void Dispose() {

            dbContext.Dispose();

        }

        // Private members

        private readonly IGuild guild;
        private readonly IOfcDbContext dbContext;

    }

}
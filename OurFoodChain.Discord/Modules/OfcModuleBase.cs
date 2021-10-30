using OurFoodChain.Data;
using OurFoodChain.Discord.Data.Dal;
using System;

namespace OurFoodChain.Discord.Modules {

    public abstract class OfcModuleBase :
         Gsemac.Discord.Modules.ModuleBase {

        // Public members

        public new IOfcDiscordBotOptions Config { get; set; }

        public OfcModuleBase(IOfcDbContext dbContext) {

            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
            this.guildUnitOfWork = new Lazy<GuildOfcUnitOfWork>(CreateGuildOfcUnitOfWork);

        }

        public GuildOfcUnitOfWork Db => guildUnitOfWork.Value;

        // Private members

        private readonly IOfcDbContext dbContext;
        private readonly Lazy<GuildOfcUnitOfWork> guildUnitOfWork;

        private GuildOfcUnitOfWork CreateGuildOfcUnitOfWork() {

            return new GuildOfcUnitOfWork(dbContext, Context.Guild);

        }

    }

}
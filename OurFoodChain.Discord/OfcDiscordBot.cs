using Gsemac.Discord;
using Gsemac.IO.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OurFoodChain.Data;
using OurFoodChain.Data.Dal;
using System.Threading.Tasks;

namespace OurFoodChain.Discord {

    public sealed class OfcDiscordBot :
        DiscordBotBase {

        // Public members

        public OfcDiscordBot(IOfcDiscordBotOptions options) :
            this(options, new ConsoleLogger()) {
        }
        public OfcDiscordBot(IOfcDiscordBotOptions options, ILogger logger) :
            base(options, logger) {

            this.options = options;

        }

        // Protected members

        protected override Task ConfigureServicesAsync(IServiceCollection services) {

            services.AddDbContext<OfcDbContext>(builder => builder.UseSqlite($"Data Source={options.DatabaseFilePath};"))
                .AddScoped<OfcUnitOfWork<OfcDbContext>>();

            return base.ConfigureServicesAsync(services);

        }

        // Private members

        private readonly IOfcDiscordBotOptions options;

    }

}
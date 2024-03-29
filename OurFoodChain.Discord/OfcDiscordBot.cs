﻿using Discord.Commands;
using Gsemac.Discord;
using Gsemac.IO.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OurFoodChain.Data;
using System;
using System.Reflection;
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

            services.AddDbContext<IOfcDbContext, OfcDbContext>(builder => builder.UseSqlite($"Data Source={options.DatabaseFilePath};"))
                .AddSingleton<IDiscordBotOptions>(options)
                .AddSingleton(options);

            return base.ConfigureServicesAsync(services);

        }
        protected override Task ConfigureServicesAsync(IServiceProvider services) {

            services.GetRequiredService<IOfcDbContext>().Database.EnsureCreated();

            return base.ConfigureServicesAsync(services);

        }
        protected override async Task ConfigureCommandsAsync(CommandService commandService, IServiceProvider serviceProvider) {

            await commandService.AddModulesAsync(Assembly.GetExecutingAssembly(), serviceProvider);

            await base.ConfigureCommandsAsync(commandService, serviceProvider);

        }

        // Private members

        private readonly IOfcDiscordBotOptions options;

    }

}
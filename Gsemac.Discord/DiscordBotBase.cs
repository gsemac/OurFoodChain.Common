using Discord;
using Discord.Commands;
using Discord.Net.Providers.WS4Net;
using Discord.WebSocket;
using Gsemac.Discord.Documentation;
using Gsemac.Discord.Interactivity;
using Gsemac.Discord.Modules;
using Gsemac.IO.Logging;
using Gsemac.IO.Logging.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Gsemac.Discord {

    public abstract class DiscordBotBase :
        IDiscordBot {

        // Public members

        public async Task ConnectAsync() {

            if (disposedValue)
                throw new ObjectDisposedException(nameof(DiscordBotBase));

            if (Client is null)
                await StartAsync();

            string token = Options.Token;

            await Client.LoginAsync(TokenType.Bot, token);

            await Client.StartAsync();

            await Client.SetGameAsync(Options.Playing);

        }
        public async Task DisconnectAsync() {

            if (disposedValue)
                throw new ObjectDisposedException(nameof(DiscordBotBase));

            await Client.LogoutAsync();

        }

        public void Dispose() {

            Dispose(disposing: true);

            GC.SuppressFinalize(this);

        }

        // Protected members

        protected DiscordSocketClient Client { get; private set; }
        protected IDiscordBotOptions Options { get; }
        protected ILogger Logger { get; }

        protected DiscordBotBase(IDiscordBotOptions options) :
            this(options, new ConsoleLogger()) {
        }
        protected DiscordBotBase(IDiscordBotOptions options, ILogger logger) {

            Options = options;
            Logger = new NamedLogger(logger, GetType().Name);

        }

        protected virtual async Task ConfigureDiscordClientAsync(DiscordSocketConfig config) {

            config.LogLevel = LogSeverity.Info;

            Logger.Info("Configuring client");

            // Required on Windows 7

            if (Options.UseWS4Net)
                config.WebSocketProvider = WS4NetProvider.Instance;

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureServicesAsync(IServiceCollection services) {

            Logger.Info("Adding services");

            services.AddSingleton(Client)
                .AddSingleton<BaseSocketClient>(Client)
                .AddSingleton<CommandService>()
                .AddSingleton(Logger);

            services.TryAddSingleton(Options);
            services.TryAddSingleton<IInteractiveMessageServiceOptions, InteractiveMessageServiceOptions>();
            services.TryAddSingleton<IInteractiveMessageService, InteractiveCommandHandlerService>();
            services.TryAddSingleton<ICommandHandlerService>(provider => provider.GetRequiredService<IInteractiveMessageService>() as InteractiveCommandHandlerService);
            services.TryAddSingleton<ICommandMetadataServiceOptions, CommandMetadataServiceOptions>();
            services.TryAddSingleton<ICommandMetadataService, CommandMetadataService>();
            services.TryAddSingleton<IPaginatedMessageService, PaginatedMessageService>();

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureServicesAsync(IServiceProvider services) {

            Logger.Info("Configuring services");

            serviceProvider.GetRequiredService<ICommandHandlerService>();

            await ConfigureCommandsAsync(serviceProvider.GetRequiredService<CommandService>(), serviceProvider);

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureCommandsAsync(CommandService commandService, IServiceProvider serviceProvider) {

            Logger.Info("Configuring commands");

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

            if (serviceProvider.GetService<ICommandMetadataService>() is object)
                await commandService.AddModuleAsync<HelpModule>(serviceProvider);

        }

        protected virtual async Task OnLogAsync(ILogMessage message) {

            Logger.Log(message);

            await Task.CompletedTask;

        }
        protected virtual async Task OnReadyAsync() {

            foreach (IGuild guild in Client.Guilds)
                Logger.Info($"Joined {guild.Name} ({guild.Id})");

            await Task.CompletedTask;

        }

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    Client?.Dispose();
                    serviceProvider?.Dispose();

                }

                disposedValue = true;

            }
        }

        // Private members

        private bool disposedValue;
        private ServiceProvider serviceProvider;

        private async Task<DiscordSocketConfig> BuildDiscordSocketConfigAsync() {

            DiscordSocketConfig config = new DiscordSocketConfig();

            await ConfigureDiscordClientAsync(config);

            return config;

        }
        private async Task<DiscordSocketClient> BuildDiscordClientAsync() {

            return new DiscordSocketClient(await BuildDiscordSocketConfigAsync());

        }

        private async Task StartAsync() {

            Client = await BuildDiscordClientAsync();

            IServiceCollection services = new ServiceCollection();

            await ConfigureServicesAsync(services);

            serviceProvider = services.BuildServiceProvider();

            await ConfigureServicesAsync(serviceProvider);

            Client.Log += (message) => OnLogAsync(BotUtilities.ConvertLogMessage(message));
            Client.Ready += OnReadyAsync;

        }

    }

}
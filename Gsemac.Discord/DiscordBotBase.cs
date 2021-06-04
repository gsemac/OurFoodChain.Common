using Discord;
using Discord.Commands;
using Discord.Net.Providers.WS4Net;
using Discord.WebSocket;
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

            string token = Configuration.Token;

            await Client.LoginAsync(TokenType.Bot, token);

            await Client.StartAsync();

            await Client.SetGameAsync(Configuration.Playing);

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
        protected IDiscordBotOptions Configuration { get; }
        protected ILogger Logger { get; }

        protected DiscordBotBase(IDiscordBotOptions configuration, ILogger logger) {

            Configuration = configuration;
            Logger = new NamedLogger(logger, GetType().Name);

        }

        protected virtual async Task ConfigureDiscordClientAsync(DiscordSocketConfig config) {

            config.LogLevel = LogSeverity.Info;

            Logger.Info("Configuring client");

            // Required on Windows 7

            if (Configuration.UseWS4Net)
                config.WebSocketProvider = WS4NetProvider.Instance;

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureServicesAsync(IServiceCollection services) {

            Logger.Info("Configuring services");

            services.AddSingleton(Configuration)
                .AddSingleton(Client)
                .AddSingleton<BaseSocketClient>(Client)
                .AddSingleton<CommandService>()
                .AddSingleton(Logger);

            services.TryAddSingleton<IInteractiveMessageServiceOptions, InteractiveMessageServiceOptions>();
            services.TryAddSingleton<IInteractiveMessageService, InteractiveCommandHandlerService>();
            services.TryAddSingleton<ICommandHandlerService>(provider => provider.GetRequiredService<IInteractiveMessageService>() as InteractiveCommandHandlerService);
            services.TryAddSingleton<IDocumentationServiceOptions, DocumentationServiceOptions>();
            services.TryAddSingleton<IDocumentationService, DocumentationService>();
            services.TryAddSingleton<IPaginatedMessageService, PaginatedMessageService>();

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureCommandsAsync(CommandService commandService) {

            Logger.Info("Configuring commands");

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

            if (serviceProvider.GetService<IDocumentationService>() is object)
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

            serviceProvider.GetRequiredService<ICommandHandlerService>();

            await ConfigureCommandsAsync(serviceProvider.GetRequiredService<CommandService>());

            Client.Log += (message) => OnLogAsync(BotUtilities.ConvertLogMessage(message));
            Client.Ready += OnReadyAsync;

        }

    }

}
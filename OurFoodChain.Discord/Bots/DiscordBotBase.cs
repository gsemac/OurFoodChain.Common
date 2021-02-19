using Discord;
using Discord.Commands;
using Discord.Net.Providers.WS4Net;
using Discord.WebSocket;
using Gsemac.IO.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OurFoodChain.Discord.Bots.Modules;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public abstract class DiscordBotBase :
        IDiscordBot {

        // Public members

        public event LogEventHandler Log;

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

        protected LogEventHelper OnLog => new LogEventHelper(GetType().Name, Log);
        protected DiscordSocketClient Client { get; private set; }
        protected IDiscordBotConfiguration Configuration { get; }

        protected DiscordBotBase(IDiscordBotConfiguration configuration) {

            Configuration = configuration;

        }

        protected virtual async Task ConfigureDiscordClientAsync(DiscordSocketConfig config) {

            config.LogLevel = LogSeverity.Info;

            OnLog.Info("Configuring client");

            // Required on Windows 7

            if (Configuration.UseWS4Net)
                config.WebSocketProvider = WS4NetProvider.Instance;

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureServicesAsync(IServiceCollection services) {

            OnLog.Info("Configuring services");

            services.AddSingleton(Configuration)
                .AddSingleton(Client)
                .AddSingleton<BaseSocketClient>(Client)
                .AddSingleton<CommandService>();

            services.TryAddSingleton<ICommandHandlerService, CommandHandlerService>();
            services.TryAddSingleton<ICommandHelpServiceOptions, CommandHelpServiceOptions>();
            services.TryAddSingleton<ICommandHelpService, CommandHelpService>();

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureCommandsAsync(CommandService commandService) {

            OnLog.Info("Configuring commands");

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

            if (serviceProvider.GetService<ICommandHelpService>() is object)
                await commandService.AddModuleAsync<HelpModule>(serviceProvider);

        }

        protected virtual async Task OnLogAsync(ILogMessage message) {

            OnLog.OnLog(message);

            await Task.CompletedTask;

        }
        protected virtual async Task OnReadyAsync() {

            foreach (IGuild guild in Client.Guilds)
                OnLog.Info($"Joined {guild.Name} ({guild.Id})");

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
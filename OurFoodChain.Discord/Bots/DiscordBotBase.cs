using Discord;
using Discord.Net.Providers.WS4Net;
using Discord.WebSocket;
using Gsemac.IO.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
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

        protected virtual async Task ConfigureDiscordClientAsync(DiscordSocketConfig config) {

            config.LogLevel = global::Discord.LogSeverity.Info;

            // Required on Windows 7

            if (Configuration.UseWS4Net)
                config.WebSocketProvider = WS4NetProvider.Instance;

            await Task.CompletedTask;

        }
        protected virtual async Task ConfigureServicesAsync(IServiceCollection services) {

            services.AddSingleton(Configuration);

            await Task.CompletedTask;

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

            await ConnectAsync();

            Client.Log += (message) => OnLogAsync(DiscordUtilities.ConvertLogMessage(message));
            Client.Ready += OnReadyAsync;

        }

    }

}
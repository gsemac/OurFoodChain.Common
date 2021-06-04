using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OurFoodChain.Discord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public class InteractiveCommandHandlerService :
        CommandHandlerService,
        IInteractiveMessageService {

        // Public members

        public InteractiveCommandHandlerService(CommandService commandService, BaseSocketClient discordClient, IDocumentationService helpService, IServiceProvider serviceProvider, IDiscordBotOptions botConfiguration, IInteractiveMessageServiceOptions options) :
            base(commandService, discordClient, helpService, serviceProvider, botConfiguration) {

            this.botConfiguration = botConfiguration;
            this.options = options;

        }

        public async Task<IUserMessage> GetNextMessageAsync(ICommandContext context, IInteractionOptions options = null) {

            InteractiveMessageInfo interactiveMessage = new InteractiveMessageInfo(context, options ?? InteractionOptions.Default);

            return await WaitForReplyAsync(interactiveMessage);

        }

        // Protected members

        protected override async Task OnMessageReceivedAsync(IMessage message) {

            if ((botConfiguration.RespondToDMs || !message.IsDM()) && message is IUserMessage userMessage) {

                bool handled = false;

                if (options.IgnorePrefixInReplies || !message.Content.StartsWith(botConfiguration.Prefix, StringComparison.OrdinalIgnoreCase))
                    handled = await HandleInteractiveMessageAsync(userMessage);

                if (!handled)
                    await base.OnMessageReceivedAsync(message);

            }

        }

        // Private members

        private sealed class InteractiveMessageInfo :
            IDisposable {

            public ICommandContext Context { get; set; }
            public IUserMessage Response { get; set; }
            public IInteractionOptions Options { get; set; }
            public ManualResetEvent ResetEvent { get; set; } = new ManualResetEvent(false);
            public bool Cancelled { get; set; } = false;

            public InteractiveMessageInfo(ICommandContext context, IInteractionOptions options) {

                this.Context = context;
                this.Options = options;

            }

            public void Dispose() {

                ResetEvent?.Set();
                ResetEvent?.Dispose();

            }

        }

        private readonly IDiscordBotOptions botConfiguration;
        private readonly IInteractiveMessageServiceOptions options;
        private readonly IList<InteractiveMessageInfo> interactiveMessages = new List<InteractiveMessageInfo>();
        private readonly object interactiveMessagesMutex = new object();

        private async Task RegisterInteractiveMessageAsync(InteractiveMessageInfo interactiveMessage) {

            IEnumerable<InteractiveMessageInfo> interactiveMessages;

            lock (interactiveMessagesMutex)
                interactiveMessages = this.interactiveMessages.ToArray();

            if (options.MaxMessageCount > 0 && interactiveMessages.Count() >= options.MaxMessageCount) {

                // Remove the oldest message.

                InteractiveMessageInfo oldestInteractiveMessage = interactiveMessages
                    .OrderBy(m => m.Context.Message.Timestamp)
                    .FirstOrDefault();

                if (oldestInteractiveMessage is object)
                    await DeregisterInteractiveMessageAsync(oldestInteractiveMessage);
            }

            lock (interactiveMessagesMutex)
                this.interactiveMessages.Add(interactiveMessage);

        }
        private async Task DeregisterInteractiveMessageAsync(InteractiveMessageInfo interactiveMessage) {

            lock (interactiveMessagesMutex)
                interactiveMessages.Remove(interactiveMessage);

            interactiveMessage.Dispose();

            await Task.CompletedTask;

        }
        private async Task<bool> HandleInteractiveMessageAsync(IUserMessage message) {

            // Is this message in response to an interactive message?

            if (!IsUserMessage(message))
                return false;

            IEnumerable<InteractiveMessageInfo> interactiveMessages;

            lock (interactiveMessagesMutex)
                interactiveMessages = this.interactiveMessages.ToArray();

            InteractiveMessageInfo interactiveMessage = interactiveMessages
                .Where(m => !m.Options.RequireSourceUser || m.Context.Message.Author.Id == message.Author.Id)
                .Where(m => !m.Options.RequireSourceChannel || m.Context.Channel.Id == message.Channel.Id)
                .OrderBy(m => m.Context.Message.Timestamp)
                .FirstOrDefault();

            if (interactiveMessage is object) {

                string messageContent = (message.Content ?? "").Trim();

                interactiveMessage.Response = messageContent.Equals(options.CancellationKeyword, StringComparison.OrdinalIgnoreCase) ?
                    null :
                    message;

                interactiveMessage.ResetEvent?.Set();

                await DeregisterInteractiveMessageAsync(interactiveMessage);

            }

            return interactiveMessage is object;

        }

        private async Task<IUserMessage> WaitForReplyAsync(InteractiveMessageInfo interactiveMessage) {

            await RegisterInteractiveMessageAsync(interactiveMessage);

            // Block until we get a response.

            if (interactiveMessage.Options.Timeout.HasValue)
                interactiveMessage.ResetEvent.WaitOne(interactiveMessage.Options.Timeout.Value);
            else
                interactiveMessage.ResetEvent.WaitOne();

            return interactiveMessage.Response;

        }

    }

}
﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public class InteractiveCommandHandlerService :
        CommandHandlerService {

        // Public members

        public InteractiveCommandHandlerService(CommandService commandService, BaseSocketClient discordClient, ICommandHelpService helpService, IServiceProvider serviceProvider, IDiscordBotConfiguration botConfiguration, IInteractiveCommandHandlerServiceOptions options) :
            base(commandService, discordClient, helpService, serviceProvider, botConfiguration) {

            this.botConfiguration = botConfiguration;
            this.options = options;

        }

        public async Task<IMessage> GetNextMessageAsync(ICommandContext context, IInteractionOptions options = null) {

            InteractiveMessage interactiveMessage = new InteractiveMessage(context, options ?? InteractionOptions.Default);

            return await WaitForReplyAsync(interactiveMessage);

        }

        // Protected members

        protected override async Task OnMessageReceivedAsync(IMessage message) {

            if (botConfiguration.RespondToDMs || message.Channel is not IDMChannel) {

                bool handled = false;

                if (options.IgnoreCommandsInResponseMessages || !message.Content.StartsWith(botConfiguration.Prefix, StringComparison.OrdinalIgnoreCase))
                    handled = await HandleInteractiveMessageAsync(message);

                if (!handled)
                    await base.OnMessageReceivedAsync(message);

            }

        }

        // Private members

        private sealed class InteractiveMessage :
            IDisposable {

            public ICommandContext Context { get; set; }
            public IMessage Response { get; set; }
            public IInteractionOptions Options { get; set; }
            public ManualResetEvent ResetEvent { get; set; } = new ManualResetEvent(false);
            public bool Cancelled { get; set; } = false;

            public InteractiveMessage(ICommandContext context, IInteractionOptions options) {

                this.Context = context;
                this.Options = options;

            }

            public void Dispose() {

                ResetEvent?.Set();
                ResetEvent?.Dispose();

            }

        }

        private readonly IDiscordBotConfiguration botConfiguration;
        private readonly IInteractiveCommandHandlerServiceOptions options;
        private readonly IList<InteractiveMessage> interactiveMessages = new List<InteractiveMessage>();
        private readonly object interactiveMessagesMutex = new object();

        private async Task RegisterInteractiveMessageAsync(InteractiveMessage interactiveMessage) {

            IEnumerable<InteractiveMessage> interactiveMessages;

            lock (interactiveMessagesMutex)
                interactiveMessages = this.interactiveMessages.ToArray();

            if (options.MaxInteractiveMessages > 0 && interactiveMessages.Count() >= options.MaxInteractiveMessages) {

                // Remove the oldest message.

                InteractiveMessage oldestInteractiveMessage = interactiveMessages
                    .OrderBy(m => m.Context.Message.Timestamp)
                    .FirstOrDefault();

                if (oldestInteractiveMessage is object)
                    await DeregisterInteractiveMessageAsync(oldestInteractiveMessage);
            }

            lock (interactiveMessagesMutex)
                this.interactiveMessages.Add(interactiveMessage);

        }
        private async Task DeregisterInteractiveMessageAsync(InteractiveMessage interactiveMessage) {

            lock (interactiveMessagesMutex)
                interactiveMessages.Remove(interactiveMessage);

            interactiveMessage.Dispose();

            await Task.CompletedTask;

        }
        private async Task<bool> HandleInteractiveMessageAsync(IMessage message) {

            // Is this message in response to an interactive message?

            if (!IsUserMessage(message))
                return false;

            IEnumerable<InteractiveMessage> interactiveMessages;

            lock (interactiveMessagesMutex)
                interactiveMessages = this.interactiveMessages.ToArray();

            InteractiveMessage interactiveMessage = interactiveMessages
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

        private async Task<IMessage> WaitForReplyAsync(InteractiveMessage interactiveMessage) {

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
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gsemac.Discord.Interactivity {

    public class PaginatedMessageService :
        IPaginatedMessageService {

        // Public members

        public PaginatedMessageService(BaseSocketClient discordClient, IPaginatedMessageServiceOptions options = null) {

            this.options = options ?? PaginatedMessageServiceOptions.Default;

            discordClient.ReactionAdded += ReactionAddedAsync;
            discordClient.ReactionRemoved += ReactionAddedAsync;

        }

        public async Task<IUserMessage> SendPaginatedMessageAsync(ICommandContext context, IPaginatedMessage message, IPaginatedMessageOptions options = null) {

            options ??= PaginatedMessageOptions.Default;

            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (message is null)
                throw new ArgumentNullException(nameof(message));

            IPaginatedMessagePage currentPage = message.GetCurrentPage();

            IUserMessage sentMessage = await context.Channel.SendMessageAsync(currentPage.Content, isTTS: false, embed: currentPage.Embed);

            if (message.PageCount > 1) {

                IEnumerable<IEmote> emotes = new[] {
                    options.PreviousPageEmote,
                    options.NextPageEmote,
                    options.PreviousChapterEmote,
                    options.NextChapterEmote,
                }.Where(e => e is object);

                await sentMessage.AddReactionsAsync(emotes.ToArray());

            }

            await RegisterPaginatedMessageAsync(new PaginatedMessageInfo(context, sentMessage, message, options));

            return sentMessage;

        }

        // Private members

        private class PaginatedMessageInfo {

            public ICommandContext Context { get; }
            public IUserMessage Message { get; }
            public IPaginatedMessage PaginatedMessage { get; }
            public IPaginatedMessageOptions Options { get; }

            public PaginatedMessageInfo(ICommandContext context, IUserMessage message, IPaginatedMessage paginatedMessage, IPaginatedMessageOptions options) {

                Context = context;
                Message = message;
                PaginatedMessage = paginatedMessage;
                Options = options;

            }

        }

        private readonly IPaginatedMessageServiceOptions options;
        private readonly IDictionary<ulong, PaginatedMessageInfo> messages = new ConcurrentDictionary<ulong, PaginatedMessageInfo>();

        private async Task RegisterPaginatedMessageAsync(PaginatedMessageInfo paginatedMessageInfo) {

            // If we've reached the maximum number of messages, remove the oldest message.

            if (messages.Count >= options.MaxMessageCount)
                messages.Remove(messages.Keys.Min());

            messages.Add(paginatedMessageInfo.Message.Id, paginatedMessageInfo);

            await Task.CompletedTask;

        }

        public async Task ReactionAddedAsync(Cacheable<IUserMessage, ulong> cachedMessage, ISocketMessageChannel channel, SocketReaction reaction) {

            if (messages.TryGetValue(cachedMessage.Id, out PaginatedMessageInfo paginatedMessageInfo)) {

                bool allowInteraction = !paginatedMessageInfo.Options.RequireSourceUser || paginatedMessageInfo.Context.User.Id.Equals(reaction.UserId);

                if (allowInteraction) {

                    IPaginatedMessagePage currentPage = paginatedMessageInfo.PaginatedMessage.GetCurrentPage();

                    if (reaction.Emote.Equals(paginatedMessageInfo.Options.NextPageEmote)) {

                        paginatedMessageInfo.PaginatedMessage.PageIndex += 1;

                    }
                    else if (reaction.Emote.Equals(paginatedMessageInfo.Options.PreviousPageEmote)) {

                        paginatedMessageInfo.PaginatedMessage.PageIndex -= 1;

                    }
                    else if (reaction.Emote.Equals(paginatedMessageInfo.Options.NextChapterEmote)) {

                        paginatedMessageInfo.PaginatedMessage.ChapterIndex += 1;

                    }
                    else if (reaction.Emote.Equals(paginatedMessageInfo.Options.PreviousChapterEmote)) {

                        paginatedMessageInfo.PaginatedMessage.ChapterIndex -= 1;

                    }

                    IPaginatedMessagePage nextPage = paginatedMessageInfo.PaginatedMessage.GetCurrentPage();

                    if (nextPage is object && !nextPage.Equals(currentPage)) {

                        currentPage = nextPage;

                        IUserMessage message = await cachedMessage.GetOrDownloadAsync();

                        await message.ModifyAsync(m => {

                            m.Content = currentPage.Content;
                            m.Embed = currentPage.Embed;

                        });

                    }

                }

            }

        }

    }

}
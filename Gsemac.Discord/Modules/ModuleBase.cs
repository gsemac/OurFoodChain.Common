using Discord;
using Discord.Commands;
using Gsemac.Discord.Documentation;
using Gsemac.Discord.Interactivity;
using OurFoodChain.Discord.Extensions;
using System.Threading.Tasks;

namespace Gsemac.Discord.Modules {

    public abstract class ModuleBase :
        global::Discord.Commands.ModuleBase {

        // Public members

        public IDiscordBotOptions Config { get; set; }
        public CommandService CommandService { get; set; }
        public ICommandMetadataService DocumentationService { get; set; }
        public IInteractiveMessageService InteractiveMessageService { get; set; }
        public IInteractiveMessageServiceOptions InteractiveMessageServiceOptions { get; set; }
        public IPaginatedMessageService PaginatedMessageService { get; set; }

        public async Task<IUserMessage> GetNextMessageAsync(IInteractiveMessageOptions options = null) {

            if (InteractiveMessageService is object)
                return await InteractiveMessageService.GetNextMessageAsync(Context, options);

            return null;

        }
        public async Task<IUserMessage> GetReplyAsync(string message = "", bool isTTS = false, Embed embed = null, IInteractiveMessageOptions options = null) {

            message ??= "";
            options ??= InteractiveMessageOptions.Default;

            if (options.AllowCancellation && InteractiveMessageServiceOptions is object)
                message += $"\nReply with {InteractiveMessageServiceOptions.CancellationKeyword.ToLowerInvariant().ToCode()} to cancel the command.";

            message = message.Trim();

            if (!string.IsNullOrWhiteSpace(message) || embed is object)
                await Context.Channel.SendMessageAsync(text: message, isTTS: isTTS, embed: embed);

            if (InteractiveMessageService is object) {

                IUserMessage response = await InteractiveMessageService.GetNextMessageAsync(Context, options);

                if (response is null)
                    await ReplyAsync(embed: BotUtilities.BuildInfoEmbed("The command was cancelled.").Build());

                return response;

            }

            return null;

        }

        public async Task<IUserMessage> ReplyAsync(IPaginatedMessage paginatedMessage, IPaginatedMessageOptions options = null) {

            return await PaginatedMessageService.SendPaginatedMessageAsync(Context, paginatedMessage, options);

        }
        public async Task<IUserMessage> ReplyInfoAsync(string message) {

            return await ReplyAsync(embed: BotUtilities.BuildInfoEmbed(message).Build());

        }
        public async Task<IUserMessage> ReplyWarningAsync(string message) {

            return await ReplyAsync(embed: BotUtilities.BuildWarningEmbed(message).Build());

        }
        public async Task<IUserMessage> ReplyErrorAsync(string message) {

            return await ReplyAsync(embed: BotUtilities.BuildErrorEmbed(message).Build());

        }
        public async Task<IUserMessage> ReplySuccessAsync(string message) {

            return await ReplyAsync(embed: BotUtilities.BuildSuccessEmbed(message).Build());

        }

    }

}
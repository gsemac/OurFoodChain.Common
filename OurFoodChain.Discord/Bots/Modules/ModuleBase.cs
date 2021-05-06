using Discord;
using Discord.Commands;
using OurFoodChain.Discord.Extensions;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots.Modules {

    internal abstract class ModuleBase :
        global::Discord.Commands.ModuleBase {

        // Public members

        public IDiscordBotOptions Config { get; set; }
        public CommandService CommandService { get; set; }
        public IDocumentationService DocumentationService { get; set; }
        public IInteractiveMessageService InteractiveMessageService { get; set; }
        public IInteractiveMessageServiceOptions InteractiveMessageServiceOptions { get; set; }
        public IPaginatedMessageService PaginatedMessageService { get; set; }

        public async Task<IMessage> GetNextMessageAsync(IInteractionOptions options = null) {

            if (InteractiveMessageService is object)
                return await InteractiveMessageService.GetNextMessageAsync(Context, options);

            return null;

        }
        public async Task<IMessage> GetReplyAsync(string message = "", bool isTTS = false, Embed embed = null, IInteractionOptions options = null) {

            message ??= "";
            options ??= InteractionOptions.Default;

            if (options.AllowCancellation && InteractiveMessageServiceOptions is object)
                message += $"\nReply with {InteractiveMessageServiceOptions.CancellationKeyword.ToLowerInvariant().ToCode()} to cancel the command.";

            message = message.Trim();

            if (!string.IsNullOrWhiteSpace(message) || embed is object)
                await Context.Channel.SendMessageAsync(text: message, isTTS: isTTS, embed: embed);

            if (InteractiveMessageService is object) {

                IMessage response = await InteractiveMessageService.GetNextMessageAsync(Context, options);

                if (response is null)
                    await ReplyAsync(embed: BotUtilities.BuildInfoEmbed("The command was cancelled.").Build());

                return response;

            }

            return null;

        }

        public async Task<IMessage> ReplyAsync(IPaginatedMessage paginatedMessage, IPaginationOptions options = null) {

            return await PaginatedMessageService.SendPaginatedMessageAsync(Context, paginatedMessage, options);

        }

    }

}
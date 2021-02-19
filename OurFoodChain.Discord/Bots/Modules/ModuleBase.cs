using Discord;
using Discord.Commands;
using OurFoodChain.Discord.Extensions;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots.Modules {

    internal abstract class ModuleBase :
        global::Discord.Commands.ModuleBase {

        // Public members

        public IDiscordBotConfiguration Configuration { get; set; }
        public CommandService CommandService { get; set; }
        public ICommandHelpService HelpService { get; set; }
        public InteractiveCommandHandlerService InteractiveCommandHandlerService { get; set; }
        public IInteractiveCommandHandlerServiceOptions InteractiveCommandHandlerServiceOptions { get; set; }

        public async Task<IMessage> GetNextMessageAsync(IInteractionOptions options = null) {

            if (InteractiveCommandHandlerService is object)
                return await InteractiveCommandHandlerService.GetNextMessageAsync(Context, options);

            return null;

        }
        public async Task<IMessage> GetReplyAsync(string message = "", bool isTTS = false, Embed embed = null, IInteractionOptions options = null) {

            message ??= "";
            options ??= InteractionOptions.Default;

            if (options.AllowCancellation && InteractiveCommandHandlerServiceOptions is object)
                message += $"\nReply with {InteractiveCommandHandlerServiceOptions.CancellationKeyword.ToLowerInvariant().ToCode()} to cancel the command.";

            message = message.Trim();

            if (!string.IsNullOrWhiteSpace(message) || embed is object)
                await Context.Channel.SendMessageAsync(text: message, isTTS: isTTS, embed: embed);

            if (InteractiveCommandHandlerService is object) {

                IMessage response = await InteractiveCommandHandlerService.GetNextMessageAsync(Context, options);

                if (response is null)
                    await ReplyAsync(embed: BotUtilities.BuildInfoEmbed("The command was cancelled.").Build());

                return response;

            }

            return null;

        }

    }

}
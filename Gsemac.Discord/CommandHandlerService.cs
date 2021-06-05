using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Gsemac.IO.Logging;
using Gsemac.Text;
using Microsoft.Extensions.DependencyInjection;
using OurFoodChain.Discord.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gsemac.Discord {

    public class CommandHandlerService :
          ICommandHandlerService {

        // Public members

        public CommandHandlerService(CommandService commandService, BaseSocketClient discordClient, IDocumentationService helpService, IServiceProvider serviceProvider, IDiscordBotOptions botConfiguration, ILogger logger) {

            this.botConfiguration = botConfiguration;
            this.commandService = commandService;
            this.helpService = helpService;
            this.discordClient = discordClient;
            this.serviceProvider = serviceProvider;
            this.logger = new NamedLogger(logger, GetType().Name);

            commandService.Log += (e) => OnLogAsync(BotUtilities.ConvertLogMessage(e));
            commandService.CommandExecuted += OnCommandExecutedAsync;
            discordClient.MessageReceived += OnMessageReceivedAsync;

        }

        // Protected members

        protected async Task OnLogAsync(ILogMessage message) {

            logger.Log(message);

            await Task.CompletedTask;

        }

        protected virtual async Task OnMessageReceivedAsync(IMessage message) {

            if (botConfiguration.RespondToDMs || !message.IsDM()) {

                if (IsUserMessage(message) && IsCommandMessage(message))
                    await ExecuteCommandAsync(message);

            }

        }
        protected virtual async Task<IResult> ExecuteCommandAsync(IMessage message) {

            IUserMessage userMessage = message as IUserMessage;

            int argumentsIndex = GetCommmandArgumentsStartIndex(userMessage);
            ICommandContext context = new CommandContext(discordClient, userMessage);
            
            using(var scope = serviceProvider.CreateScope()) {

                IResult result = await commandService.ExecuteAsync(context, argumentsIndex, scope.ServiceProvider);

                return result;

            }

        }
        protected virtual async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result) {

            if (!string.IsNullOrEmpty(result?.ErrorReason))
                await OnCommandErrorAsync(command, context, result);

        }
        protected virtual async Task OnCommandErrorAsync(Optional<CommandInfo> command, ICommandContext context, IResult result) {

            string commandName = GetCommandNameFromMessage(context?.Message);

            if (result is null || string.IsNullOrEmpty(commandName))
                await ReplyGenericCommandErrorAsync(command, context, result);

            else if (result.Error == CommandError.BadArgCount) {

                // If help documentation exists for this command, display it.

                ICommandDocumentation commandHelpInfo = await helpService.GetCommandInfoAsync(commandName);

                if (commandHelpInfo != null) {

                    EmbedBuilder embedBuilder = BotUtilities.BuildErrorEmbed(result.ErrorReason)
                        .WithTitle($"Incorrect usage of \"{commandName.ToLowerInvariant()}\" command");

                    if (commandHelpInfo.Examples.Any()) {

                        string fieldTitle = "Example(s) of correct usage:";
                        string fieldValue = string.Join(Environment.NewLine, commandHelpInfo.Examples);

                        embedBuilder.AddField(fieldTitle, fieldValue);

                    }

                    await context.Channel.SendMessageAsync(string.Empty, false, embedBuilder.Build());

                }
                else
                    await ReplyGenericCommandErrorAsync(command, context, result);

            }
            else if (result.Error == CommandError.UnknownCommand) {

                string suggestedCommand = commandService.Commands.Where(command => IsCommandAvailableAsync(context, command).Result)
                        .SelectMany(command => command.Aliases)
                        .OrderBy(name => StringUtilities.ComputeLevenshteinDistance(name, commandName))
                        .FirstOrDefault();

                ICommandDocumentation commandHelpInfo = await helpService.GetCommandInfoAsync(suggestedCommand);

                await BotUtilities.ReplyErrorAsync(context.Channel, $"Unknown command. Did you mean {commandHelpInfo.Name.ToBold()}?");

            }
            else
                await ReplyGenericCommandErrorAsync(command, context, result);

        }

        protected bool IsCommandMessage(IMessage message) {

            if (message.Content?.Trim().Equals(botConfiguration.Prefix) ?? false)
                return false;

            if (message is IUserMessage userMessage)
                return GetCommmandArgumentsStartIndex(userMessage) != 0;
            else
                return false;

        }
        protected bool IsUserMessage(IMessage message) {

            return message is IUserMessage userMessage &&
                userMessage.Source == MessageSource.User &&
                userMessage.Author.Id != discordClient.CurrentUser.Id;

        }

        // Private members

        private readonly IDiscordBotOptions botConfiguration;
        private readonly CommandService commandService;
        private readonly IDocumentationService helpService;
        private readonly BaseSocketClient discordClient;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;

        private int GetCommmandArgumentsStartIndex(IMessage message) {

            int index = 0;

            if (message is IUserMessage userMessage) {

                userMessage.HasStringPrefix(botConfiguration.Prefix, ref index, StringComparison.InvariantCultureIgnoreCase);
                userMessage.HasMentionPrefix(discordClient.CurrentUser, ref index);

            }

            return index;

        }
        private string GetCommandNameFromMessage(IMessage message) {

            if (message is null)
                return string.Empty;

            string messageContent = message.Content.Substring(GetCommmandArgumentsStartIndex(message))?.Trim();

            string commandName = string.IsNullOrEmpty(messageContent) ?
                string.Empty :
                Regex.Match(messageContent, @"^(.+?)\b").Groups[1].Value;

            return commandName;

        }
        private async Task<bool> IsCommandAvailableAsync(ICommandContext context, CommandInfo commandInfo) {

            return (await commandInfo.CheckPreconditionsAsync(context, serviceProvider)).IsSuccess;

        }

        private static async Task ReplyGenericCommandErrorAsync(Optional<CommandInfo> command, ICommandContext context, IResult result) {

            if (result is null) {

                // Show a generic message if we don't have a result indicating what happened.

                if (command.IsSpecified)
                    await BotUtilities.ReplyErrorAsync(context.Channel, $"Something went wrong while executing the **{command.Value.Name}** command.");
                else
                    await BotUtilities.ReplyErrorAsync(context.Channel, $"Something went wrong while executing the command.");

            }
            else if (result is ExecuteResult executeResult && executeResult.Exception?.InnerException is object && !string.IsNullOrWhiteSpace(executeResult.Exception.InnerException.Message)) {

                await BotUtilities.ReplyErrorAsync(context.Channel, executeResult.Exception.InnerException.Message);

            }
            else {

                await BotUtilities.ReplyErrorAsync(context.Channel, result.ErrorReason);

            }

        }

    }

}
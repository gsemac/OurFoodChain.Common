using Discord;
using Discord.Commands;
using Gsemac.Reflection;
using Gsemac.Text.Extensions;
using OurFoodChain.Discord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots.Modules {

    internal class HelpModule :
        ModuleBase {

        // Public members

        [Command("help"), Alias("h")]
        public async Task Help([Remainder] string groupOrCommandName = "") {

            IEnumerable<CommandInfo> commands = CommandService.Commands.Where(command => command.CheckPreconditionsAsync(Context).Result.IsSuccess);

            groupOrCommandName = groupOrCommandName?.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(groupOrCommandName)) {

                // The user has either provided the name of a command group, or the name of an individual command.

                commands = commands.Where(command => command.Aliases.Any(alias => alias.Equals(groupOrCommandName, StringComparison.OrdinalIgnoreCase)));

            }

            // Pair each command with its help info and sort them according to their category.

            IEnumerable<ICommandHelpInfo> commandsInfo = commands.Select(command => HelpService.GetCommandHelpInfoAsync(command).Result);

            var commandsByCategory = commands.Zip(commandsInfo).
                GroupBy(pair => string.IsNullOrWhiteSpace(pair.Second.Category) ? DefaultCommandCategory : pair.Second.Category).
                OrderBy(group => group.Key);

            // Create a placeholder embed for the case where no commands were found.

            Embed helpEmbed = BotUtilities.BuildErrorEmbed($"{groupOrCommandName.ToCode()} is not recognized as a command or command group.").Build();

            if (commands.Any()) {

                if (commands.Count() > 1 || string.IsNullOrWhiteSpace(groupOrCommandName)) {

                    EmbedBuilder embedBuilder = new EmbedBuilder()
                        .WithTitle("Commands")
                        .WithDescription($"To learn more about a command, use `{Configuration.Prefix}help <command>` (e.g. `{Configuration.Prefix}help {commands.First().Name}`).")
                        .WithFooter(GetHelpEmbedFooter());

                    foreach (var commandGroup in commandsByCategory) {

                        string fieldTitle = commandGroup.Key.ToProper();
                        string fieldValue = string.Join("  ", commandGroup
                            .Select(pair => pair.Second.Name.ToLowerInvariant().ToCode())
                            .OrderBy(name => name));

                        embedBuilder.AddField(fieldTitle, fieldValue);

                    }

                    helpEmbed = embedBuilder.Build();

                }
                else {

                    EmbedBuilder embedBuilder = new EmbedBuilder()
                        .WithTitle($"Command: {commands.First().Name.ToLowerInvariant()}")
                        .WithFooter(GetHelpEmbedFooter());

                    if (!string.IsNullOrWhiteSpace(commandsInfo.First().Summary))
                        embedBuilder.AddField("Summary", commandsInfo.First().Summary);

                    if (commandsInfo.First().Examples.Any())
                        embedBuilder.AddField("Examples", string.Join(Environment.NewLine, commandsInfo.First().Examples));

                    if (!embedBuilder.Fields.Any())
                        embedBuilder.WithDescription("No documentation is available for this command.");

                    helpEmbed = embedBuilder.Build();

                }

            }

            await ReplyAsync(string.Empty, false, helpEmbed);

        }

        [Command("test", RunMode = RunMode.Async)]
        public async Task Test() {

            IMessage message = await GetReplyAsync();

            if (message is object)
                await ReplyAsync(message.Content);

        }

        // Private members

        private const string DefaultCommandCategory = "general";

        private static string GetHelpEmbedFooter() {

            return $"{EntryAssemblyInfo.GetProductName()} v{EntryAssemblyInfo.GetProductVersion()}";

        }

    }

}
using Discord;
using Discord.Commands;
using Gsemac.Collections.Extensions;
using Gsemac.Discord.Documentation;
using Gsemac.Reflection;
using Gsemac.Text.Extensions;
using OurFoodChain.Discord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gsemac.Discord.Modules {

    internal class HelpModule :
        ModuleBase {

        // Public members

        [Command("help"), Alias("h")]
        public async Task Help([Remainder] string commandName = "") {

            // Get matching commands and sort them into their respective categories.

            var commandGroups = (await DocumentationService.GetMetadataAsync(commandName, Context))
                .DistinctBy(metadata => metadata.FullName.ToLowerInvariant())
                .GroupBy(metadata => metadata.Category)
                .OrderBy(group => group.Key);

            if (commandGroups.Any()) {

                EmbedBuilder embedBuilder = new EmbedBuilder()
                    .WithTitle("Commands")
                    .WithFooter(GetHelpEmbedFooter());

                var firstGroup = commandGroups.First();
                var firstCommand = firstGroup.First();

                string distinctGroup = commandGroups.Count() == 1 && firstGroup.Count() > 1 ?
                    GetDistinctCommandGroup(firstGroup) :
                    string.Empty;

                if ((commandGroups.Count() == 1 && firstGroup.Count() == 1) || !string.IsNullOrWhiteSpace(distinctGroup)) {

                    // We have a single, specific command, or we have a distinct group of commands.

                    if (!string.IsNullOrWhiteSpace(distinctGroup))
                        firstCommand = firstGroup.Where(metadata => string.IsNullOrWhiteSpace(metadata.Group)).FirstOrDefault();

                    if (firstCommand is object) {

                        if (!string.IsNullOrWhiteSpace(firstCommand.Summary))
                            embedBuilder.AddField("Summary", firstCommand.Summary);

                        if (firstCommand.Aliases.Any())
                            embedBuilder.AddField("Aliases", string.Join(", ", firstCommand.Aliases));

                        if (firstCommand.Examples.Any())
                            embedBuilder.AddField("Examples", string.Join(Environment.NewLine, firstCommand.Examples));

                    }

                    if (string.IsNullOrWhiteSpace(distinctGroup)) {

                        // We have a single command (e.g. not a group).

                        embedBuilder.WithTitle($"Command: {firstCommand.FullName.ToLowerInvariant()}");

                        if (!embedBuilder.Fields.Any())
                            embedBuilder.WithDescription("No documentation is available for this command.");

                    }
                    else {

                        // We have a group or a command that supports verbs.

                        embedBuilder.WithTitle($"Command: {distinctGroup}");

                        // Select an example command that showcases the use of a verb (rather than the default command for the group).

                        ICommandMetadata exampleCommand = firstGroup.Where(metadata => !string.IsNullOrWhiteSpace(metadata.Group))
                            .First();

                        embedBuilder.WithDescription($"This command uses verbs, or subcommands.\nTo learn more, use `{Config.Prefix}help <command> <verb>` (e.g. `{Config.Prefix}help {exampleCommand.FullName}`).");

                        string fieldValue = string.Join("  ", firstGroup.Select(meta => meta.Name.ToCode())
                            .OrderBy(name => name));

                        embedBuilder.AddField("Verbs", fieldValue);

                    }

                }
                else {

                    // If we've got multiple categories of commands, list them all according to their category.
                    // Groups/commands with subcommands will be listed by their group name only.

                    foreach (var commandGroup in commandGroups) {

                        IEnumerable<string> commandsStrings = commandGroup.Select(meta => meta.FullName.ToLowerInvariant().Split(' ').First())
                            .OrderBy(name => name)
                            .Distinct(StringComparer.OrdinalIgnoreCase);

                        embedBuilder.WithDescription($"To learn more about a command, use `{Config.Prefix}help <command>` (e.g. `{Config.Prefix}help {commandsStrings.First()}`).");

                        string fieldTitle = commandGroup.Key;

                        if (string.IsNullOrWhiteSpace(fieldTitle))
                            fieldTitle = DefaultCommandCategory;
                        string fieldValue = string.Join("  ", commandsStrings.Select(str => str.ToCode()));

                        embedBuilder.AddField(fieldTitle.ToProper(), fieldValue);

                    }

                }

                await ReplyAsync(embed: embedBuilder.Build());

            }
            else if (string.IsNullOrWhiteSpace(commandName)) {

                await ReplyInfoAsync($"No commands are available.");

            }
            else {

                await ReplyErrorAsync($"{commandName.ToCode()} is not recognized as a command or command group.");

            }

        }

        // Private members

        private const string DefaultCommandCategory = "general";

        private static string GetDistinctCommandGroup(IEnumerable<ICommandMetadata> commandMetadata) {

            // All of the commands are of one group if they are all the same group, or there is a SINGLE outlier where its name is the group name.

            IEnumerable<string> groupNames = commandMetadata.Select(metadata => metadata.Group)
                .Where(group => !string.IsNullOrWhiteSpace(group))
                .Distinct(StringComparer.OrdinalIgnoreCase);

            if (groupNames.Count() != 1)
                return string.Empty;

            string groupName = groupNames.First();

            bool hasSingleGroup = commandMetadata.All(metadata => metadata.Group.Equals(groupName, StringComparison.OrdinalIgnoreCase) || metadata.FullName.Equals(groupName, StringComparison.OrdinalIgnoreCase));

            return hasSingleGroup ?
                groupName :
                string.Empty;

        }
        private static string GetHelpEmbedFooter() {

            return $"{AssemblyInfo.EntryAssembly.ProductName} v{AssemblyInfo.EntryAssembly.ProductVersion}";

        }

    }

}
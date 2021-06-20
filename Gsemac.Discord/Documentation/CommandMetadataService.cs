using Discord.Commands;
using Gsemac.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gsemac.Discord.Documentation {

    public class CommandMetadataService :
        ICommandMetadataService {

        // Public members

        public CommandMetadataService(CommandService commandService, IDiscordBotOptions botConfiguration, IServiceProvider serviceProvider, ICommandMetadataServiceOptions options) {

            this.commandService = commandService;
            this.botConfiguration = botConfiguration;
            this.serviceProvider = serviceProvider;
            this.options = options;

        }

        public Task<IEnumerable<ICommandMetadata>> GetMetadataAsync(ICommandContext context) {

            return GetMatchingCommandsAsync(string.Empty, context);

        }
        public Task<IEnumerable<ICommandMetadata>> GetMetadataAsync(string name, ICommandContext context) {

            return GetMatchingCommandsAsync(name, context);

        }
        public Task<ICommandMetadata> GetMetadataAsync(CommandInfo commandInfo, ICommandContext context) {

            if (commandInfo is null)
                throw new ArgumentNullException(nameof(commandInfo));

            CommandMetadata helpInfo = new CommandMetadata(commandInfo);

            if (helpInfo?.Examples is object && helpInfo.Examples.Any()) {

                helpInfo.Examples = helpInfo.Examples.Select(ex => FormatCommandExample(ex, commandInfo.Name)).ToList();

            }

            return Task.FromResult<ICommandMetadata>(helpInfo);

        }

        // Private members

        private readonly CommandService commandService;
        private readonly ICommandMetadataServiceOptions options;
        private readonly IDiscordBotOptions botConfiguration;
        private readonly IServiceProvider serviceProvider;

        private async Task<IEnumerable<ICommandMetadata>> GetMatchingCommandsAsync(string name, ICommandContext context) {

            return await commandService.Commands.Where(command => IsMatchingCommand(command, name))
                .ToAsyncEnumerable()
                .WhereAwait(async command => (await command.CheckPreconditionsAsync(context, serviceProvider)).IsSuccess)
                .SelectAwait(async command => await GetMetadataAsync(command, context))
                .OrderBy(metadata => metadata.FullName)
                .ToListAsync();

        }

        // Private members

        private string FormatCommandExample(string example, string commandName) {

            if (string.IsNullOrEmpty(example))
                return string.Empty;

            if (example.StartsWith(botConfiguration.Prefix))
                example = StringUtilities.After(example, botConfiguration.Prefix).Trim();

            if (example.StartsWith(commandName, StringComparison.OrdinalIgnoreCase))
                example = StringUtilities.After(example, commandName, StringComparison.OrdinalIgnoreCase).Trim();

            return $"{botConfiguration.Prefix}{commandName} {example}";

        }

        private static bool IsMatchingCommand(CommandInfo commandInfo, string commandName) {

            commandName = (commandName ?? "").Trim().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(commandName))
                return true;

            // Match commands with either a matching name, a matching alias, or a matching group name.

            return commandInfo.Aliases.Any(alias => alias.Equals(commandName, StringComparison.OrdinalIgnoreCase) || alias.Split(' ')[0].Equals(commandName, StringComparison.OrdinalIgnoreCase)) ||
                commandInfo.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase);

        }

    }

}
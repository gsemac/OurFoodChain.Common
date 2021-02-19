using Discord.Commands;
using Gsemac.Text;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public class CommandHelpService :
        ICommandHelpService {

        // Public members

        public CommandHelpService(CommandService commandService, IDiscordBotConfiguration botConfiguration, ICommandHelpServiceOptions options) {

            this.commandService = commandService;
            this.botConfiguration = botConfiguration;
            this.options = options;

        }

        public async Task<ICommandHelpInfo> GetCommandHelpInfoAsync(string commandName) {

            return await GetCommandHelpInfoAsync(GetCommandInfo(commandName));

        }
        public async Task<ICommandHelpInfo> GetCommandHelpInfoAsync(CommandInfo commandInfo) {

            ICommandHelpInfo helpInfo = GetCommandHelpInfo(commandInfo);

            if (helpInfo?.Examples is object && helpInfo.Examples.Any()) {

                helpInfo.Examples = helpInfo.Examples.Select(ex => FormatCommandExample(ex, commandInfo.Name));

            }

            return await Task.FromResult(helpInfo);

        }

        // Private members

        private readonly CommandService commandService;
        private readonly ICommandHelpServiceOptions options;
        private readonly IDiscordBotConfiguration botConfiguration;

        private CommandInfo GetCommandInfo(string commandName) {

            if (string.IsNullOrEmpty(commandName))
                return null;

            CommandInfo commandInfo = commandService.Commands.Where(command => command.Aliases.Any(name => name.Equals(commandName, StringComparison.OrdinalIgnoreCase)))
                .FirstOrDefault();

            return commandInfo;

        }
        private ICommandHelpInfo GetCommandHelpInfo(string commandName) {

            return GetCommandHelpInfo(GetCommandInfo(commandName));

        }
        private ICommandHelpInfo GetCommandHelpInfo(CommandInfo commandInfo) {

            if (commandInfo is null)
                return null;

            return new CommandHelpInfo(commandInfo);

        }

        private string FormatCommandExample(string example, string commandName) {

            if (string.IsNullOrEmpty(example))
                return string.Empty;

            if (example.StartsWith(botConfiguration.Prefix))
                example = StringUtilities.After(example, botConfiguration.Prefix).Trim();

            if (example.StartsWith(commandName, StringComparison.OrdinalIgnoreCase))
                example = StringUtilities.After(example, commandName, StringComparison.OrdinalIgnoreCase).Trim();

            return $"{botConfiguration.Prefix}{commandName} {example}";

        }

    }

}
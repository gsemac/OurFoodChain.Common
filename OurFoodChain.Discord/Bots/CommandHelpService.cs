using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public class CommandHelpService :
        ICommandHelpService {

        // Public members

        public CommandHelpService(global::Discord.Commands.CommandService commandService) :
            this(commandService, CommandHelpServiceOptions.Default) {
        }
        public CommandHelpService(global::Discord.Commands.CommandService commandService, ICommandHelpServiceOptions options) {

            this.commandService = commandService;
            this.options = options;

        }

        public async Task<ICommandHelpInfo> GetCommandHelpInfoAsync(string commandName) {

            return await Task.FromResult(GetCommandHelpInfo(commandName));

        }

        // Private members

        private readonly global::Discord.Commands.CommandService commandService;
        private readonly ICommandHelpServiceOptions options;

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

    }

}
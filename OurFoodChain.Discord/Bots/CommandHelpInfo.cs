using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OurFoodChain.Discord.Bots {

    public class CommandHelpInfo :
        ICommandHelpInfo {

        // Public members

        public string Name { get; set; }
        public string Summary { get; set; }
        public string Category { get; set; }
        public ICollection<string> Aliases { get; set; } = new List<string>();
        public ICollection<string> Examples { get; set; } = new List<string>();

        public CommandHelpInfo(string commandName) {

            Name = commandName;

        }
        public CommandHelpInfo(CommandInfo commandInfo) {

            Name = commandInfo.Name;
            Summary = commandInfo.Summary;
            Aliases = new List<string>(commandInfo.Aliases.Where(a => !a.Equals(commandInfo.Name, StringComparison.OrdinalIgnoreCase)));

        }

        IEnumerable<string> ICommandHelpInfo.Aliases {
            get => Aliases;
            set => Aliases = new List<string>(value);
        }
        IEnumerable<string> ICommandHelpInfo.Examples {
            get => Examples;
            set => Examples = new List<string>(value);
        }

    }

}
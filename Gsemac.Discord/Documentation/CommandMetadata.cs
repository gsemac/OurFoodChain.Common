using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsemac.Discord.Documentation {

    public class CommandMetadata :
        ICommandMetadata {

        // Public members

        public string FullName { get; set; }
        public string Summary { get; set; }
        public string Category { get; set; }
        public string Group => GetGroup();
        public ICollection<string> Aliases { get; set; } = new List<string>();
        public ICollection<string> Examples { get; set; } = new List<string>();
        public string Name => GetName();

        public CommandMetadata(string commandName) {

            FullName = commandName;

        }
        public CommandMetadata(CommandInfo commandInfo) {

            FullName = commandInfo.Aliases.First();
            Summary = commandInfo.Summary;
            Aliases = new List<string>(commandInfo.Aliases.Where(a => !a.Equals(commandInfo.Name, StringComparison.OrdinalIgnoreCase)));

        }

        IEnumerable<string> ICommandMetadata.Aliases {
            get => Aliases;
        }
        IEnumerable<string> ICommandMetadata.Examples {
            get => Examples;
        }

        // Private members

        private string GetGroup() {

            string[] nameParts = FullName.Split(' ');

            if (nameParts.Length > 1)
                return nameParts[0];

            return string.Empty;

        }
        private string GetName() {

            return string.IsNullOrEmpty(FullName) ? string.Empty : FullName.Split(' ').Last();

        }

    }

}
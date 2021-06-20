namespace Gsemac.Discord.Documentation {

    public class CommandMetadataServiceOptions :
        ICommandMetadataServiceOptions {

        // Public members

        public string DocumentationDirectoryPath { get; set; }

        public static CommandMetadataServiceOptions Default => new CommandMetadataServiceOptions();

    }

}
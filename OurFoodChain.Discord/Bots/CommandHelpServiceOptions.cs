namespace OurFoodChain.Discord.Bots {

    public class CommandHelpServiceOptions :
        ICommandHelpServiceOptions {

        // Public members

        public string HelpDirectoryPath { get; set; }

        public static CommandHelpServiceOptions Default => new CommandHelpServiceOptions();

    }

}
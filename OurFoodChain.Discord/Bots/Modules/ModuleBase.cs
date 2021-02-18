namespace OurFoodChain.Discord.Bots.Modules {

    internal abstract class ModuleBase :
        global::Discord.Commands.ModuleBase {

        public IDiscordBotConfiguration Configuration { get; set; }
        public global::Discord.Commands.CommandService CommandService { get; set; }
        public ICommandHelpService HelpService { get; set; }

    }

}
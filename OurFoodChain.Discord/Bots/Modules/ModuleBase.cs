namespace OurFoodChain.Discord.Bots.Modules {

    internal abstract class ModuleBase :
        global::Discord.Commands.ModuleBase {

        public ICommandHelpService HelpService { get; set; }

    }

}
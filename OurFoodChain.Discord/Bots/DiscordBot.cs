using Gsemac.IO.Logging;

namespace OurFoodChain.Discord.Bots {

    public class DiscordBot :
        DiscordBotBase {

        // Public members

        public DiscordBot(IDiscordBotOptions configuration) :
           this(configuration, new ConsoleLogger()) {
        }
        public DiscordBot(IDiscordBotOptions configuration, ILogger logger) :
            base(configuration, logger) {
        }

    }

}
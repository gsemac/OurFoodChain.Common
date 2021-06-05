using Gsemac.IO.Logging;

namespace Gsemac.Discord {

    public class DiscordBot :
        DiscordBotBase {

        // Public members

        public DiscordBot(IDiscordBotOptions options) :
           base(options) {
        }
        public DiscordBot(IDiscordBotOptions options, ILogger logger) :
            base(options, logger) {
        }

    }

}
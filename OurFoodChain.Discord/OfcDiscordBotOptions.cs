using Gsemac.Discord;

namespace OurFoodChain.Discord {

    public class OfcDiscordBotOptions :
        DiscordBotOptions,
        IOfcDiscordBotOptions {

        public string DatabaseFilePath { get; set; } = "data.sqlite";

    }

}
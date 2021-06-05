using Gsemac.Discord;

namespace OurFoodChain.Discord {

    public interface IOfcDiscordBotOptions :
        IDiscordBotOptions {

        string DatabaseFilePath { get; }

    }

}
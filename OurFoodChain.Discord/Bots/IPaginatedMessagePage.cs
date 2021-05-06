using Discord;

namespace OurFoodChain.Discord.Bots {

    public interface IPaginatedMessagePage {

        string Content { get; }
        Embed Embed { get; }

    }

}
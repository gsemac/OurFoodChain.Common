using Discord;

namespace OurFoodChain.Discord.Bots {

    public interface IPaginationOptions :
        IInteractionOptions {

        IEmote NextPageEmote { get; }
        IEmote PreviousPageEmote { get; }
        IEmote NextChapterEmote { get; }
        IEmote PreviousChapterEmote { get; }

    }

}
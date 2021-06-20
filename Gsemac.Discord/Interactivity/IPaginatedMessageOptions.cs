using Discord;

namespace Gsemac.Discord.Interactivity {

    public interface IPaginatedMessageOptions :
        IInteractiveMessageOptions {

        IEmote NextPageEmote { get; }
        IEmote PreviousPageEmote { get; }
        IEmote NextChapterEmote { get; }
        IEmote PreviousChapterEmote { get; }

    }

}
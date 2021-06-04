using Discord;

namespace Gsemac.Discord {

    public interface IPaginatedMessageOptions :
        IInteractiveMessageOptions {

        IEmote NextPageEmote { get; }
        IEmote PreviousPageEmote { get; }
        IEmote NextChapterEmote { get; }
        IEmote PreviousChapterEmote { get; }

    }

}
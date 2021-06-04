using Discord;

namespace Gsemac.Discord {

    public class PaginatedMessageOptions :
        InteractiveMessageOptions,
        IPaginatedMessageOptions {

        public IEmote NextPageEmote { get; set; } = new Emoji("▶");
        public IEmote PreviousPageEmote { get; set; } = new Emoji("◀");
        public IEmote NextChapterEmote { get; set; }
        public IEmote PreviousChapterEmote { get; set; }

        public static new PaginatedMessageOptions Default => new PaginatedMessageOptions();

    }

}
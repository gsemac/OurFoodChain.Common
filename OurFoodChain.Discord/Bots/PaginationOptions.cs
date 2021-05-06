using Discord;

namespace OurFoodChain.Discord.Bots {

    public class PaginationOptions :
        InteractionOptions,
        IPaginationOptions {

        public IEmote NextPageEmote { get; set; } = new Emoji("▶");
        public IEmote PreviousPageEmote { get; set; } = new Emoji("◀");
        public IEmote NextChapterEmote { get; set; }
        public IEmote PreviousChapterEmote { get; set; }

        public static new PaginationOptions Default => new PaginationOptions();

    }

}
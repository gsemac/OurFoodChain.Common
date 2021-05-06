using Discord;

namespace OurFoodChain.Discord.Bots {

    public class PaginatedMessagePage :
        IPaginatedMessagePage {

        public string Content { get; }
        public Embed Embed { get; }

        public PaginatedMessagePage(string content, Embed embed) {

            this.Content = content;
            this.Embed = embed;

        }

    }

}